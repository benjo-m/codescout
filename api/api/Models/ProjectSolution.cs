namespace api.Models;

public class ProjectSolution
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public int UserId { get; set; }
    public string ProjectUrl { get; set; }
    public DateOnly SubmittedAt { get; set; }
}