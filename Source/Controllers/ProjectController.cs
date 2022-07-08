using Microsoft.AspNetCore.Mvc;
using Telerik.Project.Management.Models;
using Telerik.Project.Management.Services;

namespace Telerik.Project.Management.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProjectInfo>> GetProjectsAsync()
        {
            return await this.projectService.GetProjectsAsync();
        }

        [HttpGet("{projectId}")]
        public async Task<ProjectInfo> GetProjectAsync(string projectId)
        {
            return await this.projectService.GetProjectAsync(projectId);
        }

        [HttpPut]
        public async Task<ProjectInfo> CreateProjectAsync(ProjectInfo projectInfo)
        {
            return await this.projectService.CreateProjectAsync(projectInfo);
        }

        [HttpPost]
        public async Task<ProjectInfo> UpdateProjectAsync(ProjectInfo projectInfo)
        {
            return await this.projectService.UpdateProjectAsync(projectInfo);
        }
    }
}
