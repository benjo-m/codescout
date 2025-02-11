namespace api.DTOs.Project;

public class SubmitProjectRequest
{
    public int UserId { get; set; }
    public int ProjectId { get; set; }
    public string ProjectUrl { get; set; }
}