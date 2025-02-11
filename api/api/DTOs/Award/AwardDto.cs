using api.Models;

namespace api.DTOs.Award;

public class AwardDto
{
    public string Name { get; set; }
    public string Description { get; set; }

    public AwardDto(Models.Award award)
    {
        Name = award.Name;
        Description = award.Description;
    }
}