using api.Models;

namespace api.DTOs.Project
{
    public class ProjectSubmissionsResponse
    {
        public int ProjectId { get; set; }
        public int RecruiterId { get; set; }
        public List<SubmittedProjectMinimalDto> SubmittedProjects { get; set; } = new List<SubmittedProjectMinimalDto>();
    }
}
