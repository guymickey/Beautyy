using Beautyy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpPut("Save")]
        public ActionResult Save ([FromBody] string category)
        {
            List<Category> cat = JsonConvert.DeserializeObject<List<Category>>(category);

            foreach (Category item in cat)
            {
                if (item.Id == 0)
                {
                    item.Create(_context);
                }
                else
                {
                    item.Update(_context, item);
                }
            }

            List<Category> old = _context.Categories.Where(c => !c.IsDelete).ToList();

            foreach (Category catdelete in old)
            {
                bool existsInNew = cat.Any(c => c.Id == catdelete.Id);
                if (!existsInNew)
                {
                    catdelete.Delete(_context);
                }
            }
            _context.SaveChanges();
            return Ok(category);
        }

        [HttpGet("Index")]
        public ActionResult GetAll()
        {
            List<Category> categories = Category.GetAll(_context);
            return Ok(categories);
        }


     
    }
}
