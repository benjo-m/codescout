using api.Data;
using api.DTOs.Auth;
using api.Helpers;
using api.Models;
using Google.Apis.Util;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace api.Services;

public class AuthService
{
    private readonly ApplicationContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly EmailService _emailService;

    public AuthService(ApplicationContext context, IHttpContextAccessor httpContextAccessor, EmailService emailService)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _emailService = emailService;
    }


    public bool isValidId(int Id)
    {
        return Id > 0;
    }


    public async Task<SignInResponse?> SignIn(SignInRequest signInRequest)
    {
        User? user = _context.Users.Where(x => x.Username == signInRequest.Username).SingleOrDefault();

        if (user == null || !BCrypt.Net.BCrypt.Verify(signInRequest.Password, user.PasswordHash))
            return null;

        if (user.TwoFactorEnabled)
        {
            string code = new TokenGenerator().GenerateToken(6).ToUpper();

            Otp otp = new Otp
            {
                UserId = user.Id,
                Code = code
            };

            _context.Otps.Add(otp);
            _context.SaveChanges();

            await _emailService.SendEmail(user.Email, "CodeScout One-time password", $"Your OTP code is: {code}");

            return new SignInResponse
            {
                UserId = user.Id,
            };
        }

        return SaveAuthRecord(user);
    }

    private SignInResponse SaveAuthRecord(User user)
    {
        string token = new TokenGenerator().GenerateToken(15) + ':' + user.Id.ToString();

        AuthRecord authRecord = new()
        {
            User = user,
            Token = token,
            ExpirationDate = DateTime.UtcNow.AddDays(7),
        };

        _context.AuthRecords.Add(authRecord);
        _context.SaveChanges();

        SignInResponse signInResponse = new()
        {
            UserId = user.Id,
            Token = token,
            TokenExpirationDate = authRecord.ExpirationDate
        };

        return signInResponse;
    }


    public SignInResponse? VerifyOtp(TwoFactorRequest request)
    {
        User user = _context.Users.Single(x => x.Id == request.UserId);
        Otp? otp = _context.Otps.SingleOrDefault(x => x.Code == request.Otp && x.UserId == request.UserId);

        if (otp == null) return null;

        _context.Otps.Remove(otp);
        _context.SaveChanges();
        return SaveAuthRecord(user);
    }

    public void SignOut(SignOutRequest signOutRequest)
    {
        AuthRecord record = _context.AuthRecords.Where(x => x.Token == signOutRequest.Token).SingleOrDefault()!;

        _context.AuthRecords.Remove(record);
        _context.SaveChanges();
    }

    public bool AuthenticatedUser()
    {
        string? token = _httpContextAccessor?.HttpContext?.Request.Headers["auth-token"];

        return _context.AuthRecords.SingleOrDefault(x => x.Token == token) != null;
    }

    public User? GetCurrentUser()
    {
        string? tokenFromHeader = _httpContextAccessor?.HttpContext?.Request.Headers["auth-token"];

        AuthRecord? authRecord = _context.AuthRecords
            .Include(x => x.User)
            .ThenInclude(u => u!.Role)
            .SingleOrDefault(x => x.Token == tokenFromHeader);

        return authRecord?.User;
    }

    public string CurrentUserRole()
    {
        return GetCurrentUser().Role.Name;
    }

    public async Task RemoveExpiredTokensAsync(CancellationToken cancellationToken)
    {
        var expiredTokens = await _context.AuthRecords
            .Where(token => token.ExpirationDate < DateTime.UtcNow)
            .ToListAsync(cancellationToken);

        if (expiredTokens.Any())
        {
            _context.AuthRecords.RemoveRange(expiredTokens);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}