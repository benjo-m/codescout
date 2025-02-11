using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class AwardService
{
    private ApplicationContext _context;

    public AwardService(ApplicationContext context)
    {
        _context = context;
    }

    public void CheckAwards(int userId)
    {
        List<string> unlockedAwards = _context.AwardUsers
            .Where(x => x.UserId == userId)
            .Include(x => x.Award)
            .Select(x => x.Award.Name)
            .ToList();
        
        User user = _context.Users.First(x => x.Id == userId);

        if (!unlockedAwards.Contains("First Submission") && CheckFirstSubmission(userId))
            user.Awards.Add(_context.Awards.First(x => x.Name == "First Submission"));

        if (!unlockedAwards.Contains("Solution Specialist") && CheckSolutionSpecialist(userId))
            user.Awards.Add(_context.Awards.First(x => x.Name == "Solution Specialist"));

        if (!unlockedAwards.Contains("Domain Expert") && CheckDomainExpert(userId))
            user.Awards.Add(_context.Awards.First(x => x.Name == "Domain Expert"));

        if (!unlockedAwards.Contains("Polyglot Developer") && CheckPolyglotDeveloper(userId))
            user.Awards.Add(_context.Awards.First(x => x.Name == "Polyglot Developer"));

        _context.Users.Update(user);
        _context.SaveChanges();
    }

    private bool CheckFirstSubmission(int userId)
    {
        return _context.ProjectUsers.Any(x => x.UserId == userId);
    }

    private bool CheckSolutionSpecialist(int userId)
    {
        return _context.ProjectUsers.Where(x => x.UserId == userId).Count() == 10;
    }

    private bool CheckPolyglotDeveloper(int userId)
    {
        var submittedProjects = _context.ProjectUsers
            .Where(pu => pu.UserId == userId)
            .Include(pu => pu.Project)
            .ThenInclude(p => p.Technologies)
            .ToList();

        List<string> technologies = new();

        foreach (var project in submittedProjects)
            foreach (var tech in project.Project.Technologies)
                technologies.Add(tech.Name);

        return technologies.Count() >= 20;
    }

    private bool CheckDomainExpert(int userId)
    {
        var submittedProjects = _context.ProjectUsers
            .Where(pu => pu.UserId == userId)
            .Include(pu => pu.Project)
            .ThenInclude(p => p.Technologies)
            .ToList();

        List<string> technologies = new();

        foreach (var project in submittedProjects)
            foreach (var tech in project.Project.Technologies)
                technologies.Add(tech.Name);

        Dictionary<string, int> dict = new();

        foreach (var tech in technologies)
        {
            if (dict.ContainsKey(tech))
                dict[tech]++;
            else
                dict[tech] = 1;

            if (dict[tech] == 10)
                return true;
        }

        return false;
    }
}
