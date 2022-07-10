using Microsoft.AspNetCore.Mvc;
using Telerik.Project.Management.Models;
using Telerik.Project.Management.Services;

namespace Telerik.Project.Management.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryService historyService;

        public HistoryController(IHistoryService historyService)
        {
            this.historyService = historyService;
        }

        [HttpGet("{parentId}")]
        public async Task<IEnumerable<ChangeSet>> GetChangesAsync(string parentId)
        {
            return await this.historyService.GetChangesAsync(parentId);
        }
    }
}
