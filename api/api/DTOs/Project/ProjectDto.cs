using api.DTOs.Company;
using api.Models;
using System.Text.Json.Serialization;

namespace api.DTOs.Project;

public class ProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly DueDate { get; set; }
    public List<string> Technologies { get; set; } = new List<string>();
    public string Position { get; set; }
    public string WorkArrangement { get; set; }
    public int RecruiterId { get; set; }
    public string Recruiter { get; set; }
    public CompanyDto Company { get; set; }

    [JsonConstructor]
    public ProjectDto()
    { }

    public ProjectDto(Models.Project project)
    {
        Id = project.Id;
        Name = project.Name;
        Description = project.Description;
        DueDate = project.DueDate;
        foreach (var item in project.Technologies)
        {
            Technologies.Add(item.Name);
        }
        Position = project.Position.Title;
        WorkArrangement = project.WorkArrangement.Name;
        RecruiterId = project.RecruiterId;
        Recruiter = project.Recruiter.Username;
        Company = new CompanyDto(project.Company);
    }
}