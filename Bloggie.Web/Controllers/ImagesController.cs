using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bloggie.Web.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : Controller
    {
        private readonly IImageRepository _IImageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this._IImageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var imageUrl = await _IImageRepository.UploadAsync(file);

            if(imageUrl == null)
            {
                return Problem("Something went wrong",null,(int)HttpStatusCode.InternalServerError);

            }
            return Json(new {link = imageUrl});

        }
        
    }
}
