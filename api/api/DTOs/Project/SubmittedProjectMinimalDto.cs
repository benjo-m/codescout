using api.Models;

namespace api.DTOs.Project
{
    public class SubmittedProjectMinimalDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string ProjectUrl { get; set; }

        public SubmittedProjectMinimalDto()
        {

        }

        public SubmittedProjectMinimalDto(ProjectSolution ps, string username)
        {
            Id = ps.Id;
            UserId = ps.UserId;
            ProjectUrl = ps.ProjectUrl;
            Username = username;

        }
    }
}
