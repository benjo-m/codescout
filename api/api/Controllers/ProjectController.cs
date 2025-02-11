using api.DTOs.Project;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly ProjectService _projectService;
    private readonly AuthService _authService;
    private readonly UserService _userService;
    private readonly AwardService _awardService;


    public ProjectController(ProjectService projectService, AuthService authService, UserService userService, AwardService awardService)
    {
        _projectService = projectService;
        _authService = authService;
        _userService = userService;
        _awardService = awardService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedProjectResponse>> GetProjects(
        [FromQuery] List<string>? companies,
        [FromQuery] List<string>? positions,
        [FromQuery] List<string>? technologies,
        [FromQuery] int? userId,
        [FromQuery] int page = 1)
    {
        return await _projectService.GetProjects(companies, positions, technologies, page, (userId == null) ? 0 : userId.Value);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDto>> GetProjectById(int id)
    {
        ProjectDto? project = await _projectService.GetProjectById(id);
        if (project == null)
            return NotFound();
        return Ok(project);
    }

    [HttpPost]
    public ActionResult<ProjectDto> PostProject([FromBody] CreateProjectRequest projectRequest)
    {
        try
        {
            if (_authService.isValidId(projectRequest.RecruiterId) && _authService.AuthenticatedUser() && _userService.IsRecruiter(projectRequest.RecruiterId))
            {
                return _projectService.PostProject(projectRequest);
            }
            else
            {
                return Unauthorized();
            }
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }


    [HttpGet("DoesProjectBelongToUser")]
    public ActionResult<bool> DoesProjectBelongToUser(int projectId, int userId)
    {
        try
        {
            if (!(_authService.isValidId(userId) && _authService.isValidId(projectId))) BadRequest(false);
            else if (!_authService.AuthenticatedUser()) Unauthorized(false);

            return Ok(_projectService.DoesProjectBelongToUser(projectId, userId));
        }
        catch (Exception)
        {
            return BadRequest(false);
        }
    }


    [HttpPost("submit-project")]
    public ActionResult<ProjectUser> SubmitProject([FromBody] SubmitProjectRequest submitProjectRequest)
    {
        var projectUser = _projectService.SubmitProject(submitProjectRequest);

        if (projectUser == null)
        {
            return Conflict(new
            {
                error = new
                {
                    code = "already_applied",
                    message = "The user has already applied to this project."
                }
            });
        }

        _awardService.CheckAwards(submitProjectRequest.UserId);

        return Ok(projectUser);
    }

    [HttpGet("user/{userId}")]
    public ActionResult<List<SubmittedProjectDto>> GetProjectsByUser(int userId)
    {
        return _projectService.GetProjectsByUser(userId);
    }

    [HttpGet("user/submitted-projects")]
    public ActionResult<SubmittedProjectDto> GetSubmittedProjectById([FromQuery] int projectId, int userId)
    {
        try
        {
            if (!(_authService.isValidId(userId) && _authService.isValidId(projectId))) return BadRequest();
            else if (!_authService.AuthenticatedUser()) Unauthorized();

            return _projectService.GetSubmittedProjectById(projectId, userId);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }


    [HttpGet("GetSubmissionsByProject")]
    public ActionResult<ProjectSubmissionsResponse> GetSubmissionsByProject(int projectId, int recruiterId)
    {
        try
        {
            if (!(_authService.isValidId(recruiterId) && _authService.isValidId(projectId))) return BadRequest();
            else if (!(_authService.AuthenticatedUser() && _projectService.DoesProjectBelongToUser(projectId, recruiterId))) return Unauthorized();

            return _projectService.GetSubmissionsByProject(projectId);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }


    [HttpGet("recruiter/{recruiterId}")]
    public async Task<List<ProjectDto>> GetProjectsByRecruiter([FromRoute] int recruiterId)
    {
        return await _projectService.GetProjectsByRecruiter(recruiterId);
    }

    [HttpDelete("{projectId}")]
    public ActionResult<Project> DeleteProject(int projectId)
    {
        return _projectService.DeleteProject(projectId);
    }
}