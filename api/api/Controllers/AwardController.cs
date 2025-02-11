using api.Data;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AwardController : ControllerBase
{
    private AwardService awardService;

    public AwardController(AwardService awardService)
    {
        this.awardService = awardService;
    }

    [HttpGet]
    public void CheckAwards(int userId)
    {
        awardService.CheckAwards(userId);
    }
}
