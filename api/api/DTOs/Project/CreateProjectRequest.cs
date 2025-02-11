namespace api.DTOs.Project;

public class CreateProjectRequest
{
    public int RecruiterId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly DueDate { get; set; }
    public List<string> Technologies { get; set; } = new List<string>();
    public string Position { get; set; }
    public string WorkArrangement { get; set; }
}