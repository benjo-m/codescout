using api.DTOs.Auth;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly EmailService _emailService;

    public AuthController(AuthService authService, EmailService emailService)
    {
        _authService = authService;
        _emailService = emailService;
    }

    [HttpPost("signin")]
    public Task<SignInResponse?> SignIn([FromBody] SignInRequest signInRequest)
    {
        return _authService.SignIn(signInRequest);
    }

    [HttpPost("2fa")]
    public SignInResponse? TwoFactorAuth(TwoFactorRequest request)
    {
        return _authService.VerifyOtp(request);
    }

    [HttpPost("signout")]
    public void SignOut([FromBody] SignOutRequest signOutRequest)
    {
        _authService.SignOut(signOutRequest);
    }
}