using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

[Table("Projects")]
public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly DueDate { get; set; }
    public List<Technology> Technologies { get; set; } = new();
    public int PositionId { get; set; }
    public Position Position { get; set; }
    public int WorkArrangementId { get; set; }
    public WorkArrangement WorkArrangement { get; set; }
    public int RecruiterId { get; set; }
    public User Recruiter { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public List<User> Candidates { get; set; }
    public bool Deleted { get; set; } = false;
}