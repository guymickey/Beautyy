using Beautyy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Beautyy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CustomContext _context;

        public CategoryController(CustomContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult Create ([FromBody] string category)
        {
            List<Category> cat = JsonConvert.DeserializeObject<List<Category>>(category);

            foreach (Category item in cat)
            {
                item.Create(_context);
            }
            _context.SaveChanges();
            return Ok(category);
        }
    }
}
