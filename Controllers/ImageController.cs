using Microsoft.AspNetCore.Mvc;
using Shopdemo1.Models;
using Shopdemo1.Repository;

namespace Shopdemo1.Controllers
{
    [ApiController]
    [Route("api/image")]
    public class ImageController : Controller
    {
        private readonly IImageRepository imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        [HttpPost("add-image")]
        public async Task<IActionResult> AddImage([FromForm] IFormFile Images, [FromHeader] string productCode)
        {
            int count = imageRepository.CountImage(productCode) + 1;
            Image image = new Image();
            image.CreateDate = DateTime.Now;
            image.ProductCode = productCode;
            image.ImageName = "Image_" + productCode + "_" + count + ".png";
            imageRepository.SaveImage(Images, image.ImageName);
            imageRepository.Addimage(image);
            return Ok();
        }
        [HttpPost("update-image")]
        public IActionResult UpdateImage([FromForm] IFormFile Images, [FromHeader] string productName)
        {
            imageRepository.SaveImage(Images, productName);
            return Ok();
        }
        
        [HttpDelete("delete-image")]
        public IActionResult DeleteImage(int Id)
        {
            var image = imageRepository.GetById(Id);
            imageRepository.DeleteImage(image.ImageName);
            imageRepository.DeleteImage(Id);
            return Ok();
        }
    }
}
