using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EventManagementWebApi.Services;
using EventManagementWebApi.Models;

namespace EventManagementWebApi.Controllers
{
    /// <summary>
    /// Step 2.1.09: Controller class for handling Event-related API requests
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public EventsController(ApplicationDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns all Events in the database ordered by Id in descending order
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetEvents()
        {
            var events = _context.Events.OrderByDescending(e => e.Id).ToList();
            return Ok(events);
        }

        /// <summary>
        /// Returns a specific Event by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetEvent(int id)
        {
            var eventItem = _context.Events.FirstOrDefault(e => e.Id == id);
            if (eventItem == null)
            {
                return NotFound();
            }
            return Ok(eventItem);
        }


        /// <summary>
        /// Step 2.1.11: Creates a new Event in the database
        /// </summary>
        /// <param name="eventdto"></param>
        /// <returns></returns>

        [HttpPost]
        public IActionResult CreateEvent([FromBody] EventDto eventdto)
        {
            if (eventdto == null)
            {
                return BadRequest();
            }
            var eventItem = new Event
            {
                Title = eventdto.Title,
                Description = eventdto.Description,
                StartDate = eventdto.StartDate,
                EndDate = eventdto.EndDate,
                AllDay = eventdto.AllDay,
                CreatedAt = DateTime.UtcNow
            };
            _context.Events.Add(eventItem);
            _context.SaveChanges();
            //return CreatedAtAction(nameof(GetEvent), new { id = eventItem.Id }, eventItem);
            return Ok(eventItem);
        }

        /// <summary>
        /// Step 2.1.12: Updates an existing Event in the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="eventdto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateEvent(int id, [FromBody] EventDto eventdto)
        {
            var eventItem = _context.Events.FirstOrDefault(e => e.Id == id);
            if (eventItem == null)
            {
                return NotFound();
            }

            eventItem.Title = eventdto.Title;
            eventItem.Description = eventdto.Description;
            eventItem.StartDate = eventdto.StartDate;
            eventItem.EndDate = eventdto.EndDate;
            eventItem.AllDay = eventdto.AllDay;
            eventItem.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();
            return Ok(eventItem);
        }


        /// <summary>
        ///  2.1.13: Deletes an existing Event from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            var eventItem = _context.Events.FirstOrDefault(e => e.Id == id);
            if (eventItem == null)
            {
                return NotFound();
            }

            _context.Events.Remove(eventItem);
            _context.SaveChanges();
            return Ok();
        }

    }
}
