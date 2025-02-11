using api.Data;
using api.DTOs.User;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace api.Services;

public class UserService
{
    private readonly ApplicationContext _context;
    private readonly AuthService _authService;

    public UserService(ApplicationContext context, AuthService authService)
    {
        _context = context;
        _authService = authService;
    }



    public bool IsRecruiter(int userId)
    {
        var user = _context.Users.Include(u => u.Company).FirstOrDefault(u => u.Id == userId);

        if((user != null) && (user.Company != null))
        {
            return true;
        }

        return false;
    }


    public async Task<List<UserDto>> GetAllUsers()
    {
        if (!_authService.AuthenticatedUser())
           throw new UnauthorizedAccessException();

        var users = await _context.Users
            .Include(x => x.Role)
            .Include(x => x.Company)
            .ThenInclude(c => c.Address)
            .ToListAsync();

        var userDtos = new List<UserDto>();

        foreach (var user in users)
        {
            userDtos.Add(new UserDto(user));
        }

        return userDtos;
    }

    public async Task<UserDto?> GetUserById(int id)
    {
        if (!_authService.AuthenticatedUser())
           throw new UnauthorizedAccessException();

        User? user = await _context.Users
            .Where(x => x.Id == id)
            .Include(x => x.Role)
            .Include(x => x.Awards)
            .Include(x => x.Socials)
            .Include(x => x.Company)
            .ThenInclude(c => c.Address)
            .SingleOrDefaultAsync();

        UserDto userDto = new(user);

        return userDto;
    }

    private bool isValidUser(UserCreateRequest registerRequest)
    {
        if (_context.Users.Any(x => x.Username == registerRequest.Username)) return false;

        if (_context.Users.Any(x => x.Email == registerRequest.Email)) return false;

        if (registerRequest.Role.ToLower() != "recruiter" && registerRequest.Role.ToLower() != "candidate") return false;

        return true;
    }

    public User? RegisterUser(UserCreateRequest registerRequest)
    {
        if (!isValidUser(registerRequest))
        {
            return null;
        }

        Company? company = null;

        if (registerRequest.Role == "recruiter")
        {
            company = _context.Companies.Single(x => x.Name == registerRequest.Company);
        }

        User user = new()
        {
            Username = registerRequest.Username,
            Email = registerRequest.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password),
            Role = _context.Roles.Where(x => x.Name == registerRequest.Role).Single(),
            Company = company,
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return user;
    }

    public UserStats GetUserStats(int userId)
    {
        if (!_authService.AuthenticatedUser())
           throw new UnauthorizedAccessException();

        var submittedProjects = _context.ProjectUsers
            .Where(pu => pu.UserId == userId)
            .Include(pu => pu.Project)
            .ThenInclude(p => p.Technologies)
            .ToList();

        List<string> technologies = new();

        foreach (var project in submittedProjects)
            foreach (var tech in project.Project.Technologies)
                technologies.Add(tech.Name);

        var top3TechnologiesList = technologies
            .GroupBy(s => s)
            .OrderByDescending(g => g.Count())
            .Take(3)
            .Select(g => new { String = g.Key, Count = g.Count() });

        Dictionary<string, int> top3TechnologiesDict = new();

        foreach (var item in top3TechnologiesList)
            top3TechnologiesDict.Add(item.String, item.Count);

        UserStats userStats = new()
        {
            ProjectsSubmitted = submittedProjects.Count,
            AwardsUnlocked = _context.AwardUsers.Where(au => au.UserId == userId).Count(),
            TechnologiesWorkedWith = technologies.Count(),
            TopTechnologies = top3TechnologiesDict,
        };

        return userStats;
    }

    public void AddAward(int userId, int awardId)
    {
        var user = _context.Users.Single(x => x.Id == userId);
        var award = _context.Awards.Single(x => x.Id == awardId);

        user.Awards?.Add(award);

        _context.SaveChanges();
    }

    public User EditProfile(UserUpdateRequest request)
    {
        if (_authService.GetCurrentUser()?.Id != request.UserId)
            throw new UnauthorizedAccessException();

        User user = _context.Users
            .Include(x => x.Socials)
            .FirstOrDefault(x => x.Id == request.UserId)!;

        user.Username = request.Username;
        user.Email = request.Email;
        user.Biography = request.Biography;
        user.TwoFactorEnabled = request.TwoFactorEnabled;
        user.Socials.X = request.X.IsNullOrEmpty() ? null : request.X;
        user.Socials.StackOverflow = request.StackOverflow.IsNullOrEmpty() ?  null : request.StackOverflow;
        user.Socials.GitHub = request.GitHub.IsNullOrEmpty() ? null : request.GitHub;
        user.Socials.LinkedIn = request.LinkedIn.IsNullOrEmpty() ? null : request.LinkedIn;
        user.Socials.Medium = request.Medium.IsNullOrEmpty() ? null : request.Medium;

        _context.Users.Update(user);
        _context.SaveChanges();

        return user;
    }
}