using api.Data;
using api.DTOs.Project;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit.Tnef;

namespace api.Services;

public class ProjectService
{
    private readonly ApplicationContext _context;
    private readonly AuthService _authService;
    private readonly UserService _userService;

    public ProjectService(ApplicationContext context, AuthService authService, UserService userService)
    {
        _context = context;
        _authService = authService;
        _userService = userService;
    }

    public async Task<PaginatedProjectResponse> GetProjects(List<string>? companies, List<string>? positions, List<string>? technologies, int pageNumber, int userId = 0)
    {
        if (!_authService.AuthenticatedUser())
            throw new UnauthorizedAccessException();

        const int pageSize = 8;

        var projects = await _context.Projects
            .Where(x => x.Deleted == false)
            .Where(x => companies!.Count == 0 || companies.Contains(x.Company.Name))
            .Where(x => positions!.Count == 0 || positions.Contains(x.Position.Title))
            .Where(x => technologies!.Count == 0 || x.Technologies.Any(t => technologies.Contains(t.Name)))
            .Where(x => (userId == 0) || (x.RecruiterId == userId))
            .Include(p => p.Technologies)
            .Include(p => p.Position)
            .Include(p => p.WorkArrangement)
            .Include(p => p.Recruiter)
            .Include(p => p.Company)
            .ThenInclude(c => c!.Address)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        int projectCount = _context.Projects
            .Where(x => companies!.Count == 0 || companies.Contains(x.Company.Name))
            .Where(x => positions!.Count == 0 || positions.Contains(x.Position.Title))
            .Where(x => technologies!.Count == 0 || x.Technologies.Any(t => technologies.Contains(t.Name)))
            .Where(x => (userId == 0) || (x.RecruiterId == userId))
            .Count();

        List<ProjectDto> projectDtos = new();

        foreach (var project in projects)
        {
            projectDtos.Add(new ProjectDto(project));
        }

        PaginatedProjectResponse paginatedProjectResponse = new()
        {
            Page = pageNumber,
            TotalPages = (int)Math.Ceiling((projectCount / (double)pageSize)),
            Count = projectDtos.Count,
            Projects = projectDtos,
        };

        return paginatedProjectResponse;
    }

    public async Task<ProjectDto?> GetProjectById(int id)
    {
        if (!_authService.AuthenticatedUser())
            throw new UnauthorizedAccessException();

        Project? project = await _context.Projects
            .Include(p => p.Technologies)
            .Include(p => p.Position)
            .Include(p => p.WorkArrangement)
            .Include(p => p.Recruiter)
            .Include(p => p.Company)
            .ThenInclude(c => c!.Address)
            .SingleOrDefaultAsync(p => p.Id == id);

        List<string> techList = new();

        foreach (var tech in project!.Technologies)
        {
            techList.Add(tech.Name);
        }

        return new ProjectDto(project);
    }

    public ProjectDto PostProject(CreateProjectRequest projectRequest)
    {
        var recruiter = _context.Users.Include(u => u.Company).ThenInclude(c => c.Address).FirstOrDefault(u => u.Id == projectRequest.RecruiterId);

        if ((projectRequest.Name.Length == 0)
            || (projectRequest.Description.Length == 0)
            || (projectRequest.DueDate <= DateOnly.FromDateTime(DateTime.Now))
            || (recruiter == null)
            || (recruiter.Company == null)
            || recruiter.Company.Address == null)
        {
            throw new Exception();
        }

        Project project = new Project
        {
            Name = projectRequest.Name,
            Description = projectRequest.Description,
            DueDate = projectRequest.DueDate,
            Technologies = _context.Technologies.Where(t => projectRequest.Technologies.Contains(t.Name)).ToList(),
            Position = _context.Positions.Where(p => p.Title == projectRequest.Position).Single(),
            WorkArrangement = _context.WorkArrangements.Where(w => w.Name == projectRequest.WorkArrangement).Single(),
            Recruiter = recruiter,
            Company = recruiter.Company
        };

        _context.Projects.Add(project);
        _context.SaveChanges();

        return new ProjectDto(project);
    }

    public ProjectUser? SubmitProject(SubmitProjectRequest submitProjectRequest)
    {
        if (_authService.CurrentUserRole() != "Candidate") 
            throw new UnauthorizedAccessException();

        var user = _context.Users.SingleOrDefault(x => x.Id == submitProjectRequest.UserId);
        var project = _context.Projects.SingleOrDefault(x => x.Id == submitProjectRequest.ProjectId);

        if (_context.ProjectUsers.Any(pu => pu.ProjectId == submitProjectRequest.ProjectId && pu.UserId == submitProjectRequest.UserId))
        {
            return null;
        }

        user!.ProjectsAppliedTo.Add(project!);

        ProjectSolution projectSolution = new()
        {
            ProjectId = submitProjectRequest.ProjectId,
            UserId = submitProjectRequest.UserId,
            ProjectUrl = submitProjectRequest.ProjectUrl,
            SubmittedAt = DateOnly.FromDateTime(DateTime.UtcNow)
        };

        _context.ProjectSolutions.Add(projectSolution);

        _context.SaveChanges();

        return _context.ProjectUsers.Single(pu => pu.ProjectId == submitProjectRequest.ProjectId && pu.UserId == submitProjectRequest.UserId);
    }

    public List<SubmittedProjectDto> GetProjectsByUser(int userId)
    {
        if (!_authService.AuthenticatedUser())
            throw new UnauthorizedAccessException();

        List<int> submittedProjectsIds = _context.ProjectUsers
            .Where(pu => pu.UserId == userId)
            .Select(pu => pu.ProjectId)
            .ToList();

        List<Project> submittedProjects = _context.Projects
            .Where(x => submittedProjectsIds.Contains(x.Id))
            .Include(x => x.Position)
            .Include(x => x.Recruiter)
            .Include(x => x.Company)
            .ThenInclude(c => c!.Address)
            .Include(x => x.WorkArrangement)
            .Include(x => x.Technologies)
            .ToList();

        List<SubmittedProjectDto> submittedProjectDtos = new();

        foreach (var p in submittedProjects)
        {
            var projectSolutions = _context.ProjectSolutions.Single(ps => ps.ProjectId == p.Id && ps.UserId == userId)!;

            var project = new SubmittedProjectDto(p);

            project.ProjectUrl = projectSolutions.ProjectUrl;
            project.SubmittedAt = projectSolutions.SubmittedAt;

            submittedProjectDtos.Add(project);
        }

        return submittedProjectDtos;
    }

    public SubmittedProjectDto GetSubmittedProjectById(int projectId, int userId)
    {
        Project project = _context.Projects
            .Where(x => x.Id == projectId)
            .Include(x => x.Position)
            .Include(x => x.Recruiter)
            .Include(x => x.Company)
            .ThenInclude(c => c!.Address)
            .Include(x => x.WorkArrangement)
            .Include(x => x.Technologies)
            .First();

        var projectSolutions = _context.ProjectSolutions.Single(ps => ps.ProjectId == projectId && ps.UserId == userId)!;

        SubmittedProjectDto submittedProject = new(project);

        submittedProject.ProjectUrl = projectSolutions.ProjectUrl;
        submittedProject.SubmittedAt = projectSolutions.SubmittedAt;

        return submittedProject;
    }


    public ProjectSubmissionsResponse GetSubmissionsByProject(int projectId)
    {
        ProjectSubmissionsResponse submissions = new ProjectSubmissionsResponse();

        var ps = _context.ProjectSolutions
            .Where(ps => ps.ProjectId == projectId)
            .Join(_context.Users, ps => ps.UserId, u => u.Id, (projsol, user) => new {projsol, user})
            .ToList();

        var project = _context.Projects
            .FirstOrDefault(p => p.Id == projectId);

        submissions.RecruiterId = project.RecruiterId;
        submissions.ProjectId = project.Id;

        ps.ForEach(x =>
        {
            submissions.SubmittedProjects.Add(new SubmittedProjectMinimalDto(x.projsol, x.user.Username));
        });

        return submissions;
    }


    public bool DoesProjectBelongToUser(int projectId, int userId)
    {
        return _context.Projects.FirstOrDefault(p => (p.Id == projectId) && (p.RecruiterId == userId)) != null;
    }

    public async Task<List<ProjectDto>> GetProjectsByRecruiter(int recruiterId)
    {
        var projects = await _context.Projects
            .Where(x => x.RecruiterId == recruiterId && x.Deleted == false)
            .Include(p => p.Technologies)
            .Include(p => p.Position)
            .Include(p => p.WorkArrangement)
            .Include(p => p.Recruiter)
            .Include(p => p.Company)
            .ThenInclude(c => c!.Address)
            .ToListAsync();

        List<ProjectDto> projectDtos = new();

        foreach (var project in projects)
            projectDtos.Add(new ProjectDto(project));

        return projectDtos;
    }

    public Project DeleteProject(int projectId)
    {
        Project project = _context.Projects.FirstOrDefault(x => x.Id == projectId)!;
        User? currentUser = _authService.GetCurrentUser();

        if (currentUser?.Id != project.RecruiterId)
            throw new UnauthorizedAccessException();
        
        project.Deleted = true;

        _context.Projects.Update(project);
        _context.SaveChanges();

        return project;
    }
}