namespace api.DTOs.Project;

public class PaginatedProjectResponse
{
    public int Page { get; set; }
    public int? TotalPages { get; set; }
    public int Count { get; set; }
    public List<ProjectDto>? Projects { get; set; }
}