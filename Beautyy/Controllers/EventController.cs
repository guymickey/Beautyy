using Beautyy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Beautyy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly CustomContext _context;

        public EventController(CustomContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            FormComponent events = new FormComponent();
            return Ok(events);
        }

        [HttpPost("create")]
        public ActionResult Create([FromBody] string component)
        {
            Event ev = JsonConvert.DeserializeObject<Event>(component);

            ev.Create(_context);
            _context.SaveChanges();
            return Ok(component);
        }
        [HttpPut]
        public ActionResult Update([FromBody] string ev)
        {
  
            Event even = JsonConvert.DeserializeObject<Event>(ev);

            even.Update(_context , even);
            _context.SaveChanges();
            return Ok(ev);
        }

        [HttpPost("CopyEvent")]
        public ActionResult CopyEvent(int id)
        {
            Event ev = new Event();

            ev.Cory(id, _context);

            if (ev.Id == 0)
            {
                return BadRequest();
            }

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            string json = JsonConvert.SerializeObject(ev, settings);

            return Ok(json);
        }

        [HttpPost("Copy")]
        public ActionResult CopyForm(int id)
        {
            Form form = new Form();

            form.Create(id, _context);
            
            if(form.Id == 0)
            {
                return BadRequest();
            }

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            string json = JsonConvert.SerializeObject(form, settings);

            return Ok(json);
        }

        [HttpGet("GetEvent/{id}")]
        public ActionResult GetById (int id )
        {
            Event ev = Event.GetById(id, _context);

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string json = JsonConvert.SerializeObject(ev, settings);

            return Ok(json);
        }

        [HttpGet("Index")]
        public ActionResult Index(string? search)
        {
            List<Event> ev = Event.GetAll(_context, search).OrderByDescending(i => i.IsFav).ToList();
            return Ok(ev);
        }

        [HttpDelete("Delete/{id}", Name = "DeleteEvent")]
        public ActionResult Delete(int id)
        {
            Event? ev = Event.GetById(id, _context);
            if (ev == null)
            {
                return NotFound();
            }
            ev.Delete(_context);
            _context.SaveChanges();

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string json = JsonConvert.SerializeObject(ev, settings);

            return Ok(json);
        }
    }
}
