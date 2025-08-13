using Beautyy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Beautyy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageFileController : ControllerBase
    {
        protected CustomContext _context;

        public ImageFileController (CustomContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult UploadFile([FromForm] Beautyy.Models.Dto.DtoFile file)
        {
            if (ModelState.IsValid)
            {
                ImageFile ima = new ImageFile();
                ima.FormFile = file.FormFile;
                ima.Create(_context, file.FormFile);
                _context.SaveChanges();

                return Ok(ima);

            }
            return BadRequest(ModelState);
        }
    }
}
