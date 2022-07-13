using Microsoft.AspNetCore.Mvc;
using Telerik.Project.Management.Models;
using Telerik.Project.Management.Services;

namespace Telerik.Project.Management.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpPost("{projectId}")]
        public async Task<ImageInfo> UploadImageAsync(string projectId)
        {
            var imageFiles = Request.Form.Files;
            var imageFile = imageFiles.GetFile("Image");

            if (imageFile is null)
            {
                return new ImageInfo();
            }

            return await this.imageService.SaveImageAsync(projectId, imageFile);
        }
    }
}
