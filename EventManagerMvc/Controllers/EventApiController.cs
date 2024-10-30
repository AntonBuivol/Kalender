using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EventManagerMvc.Models;

namespace EventManagerMvc.Controllers
{
    public class EventApiController : Controller
    {
        private EventContext db = new EventContext();

        // GET: EventApi/GetEvents
        [HttpGet]
        public JsonResult GetEvents()
        {
            var events = db.Events.Select(e => new
            {
                id = e.Id,
                title = e.Name,
                start = e.Date.ToString("yyyy-MM-ddTHH:mm:ss"),
                description = e.Description
            }).ToList();

            return Json(events, JsonRequestBehavior.AllowGet);
        }

        // POST: EventApi/CreateEvent
        [HttpPost]
        public JsonResult CreateEvent(Event newEvent)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(newEvent);
                db.SaveChanges();
                return Json(newEvent);
            }
            return Json(new { error = "Invalid event data" }, JsonRequestBehavior.AllowGet);
        }

        // PUT: EventApi/UpdateEvent/{id}
        [HttpPut]
        public JsonResult UpdateEvent(int id, Event updatedEvent)
        {
            var existingEvent = db.Events.Find(id);
            if (existingEvent == null)
            {
                return Json(new { error = "Event not found" }, JsonRequestBehavior.AllowGet);
            }

            if (ModelState.IsValid)
            {
                existingEvent.Name = updatedEvent.Name;
                existingEvent.Date = updatedEvent.Date;
                existingEvent.Description = updatedEvent.Description;
                db.SaveChanges();
                return Json(existingEvent);
            }
            return Json(new { error = "Invalid event data" }, JsonRequestBehavior.AllowGet);
        }

        // DELETE: EventApi/DeleteEvent/{id}
        [HttpDelete]
        public JsonResult DeleteEvent(int id)
        {
            var eventToDelete = db.Events.Find(id);
            if (eventToDelete == null)
            {
                return Json(new { error = "Event not found" }, JsonRequestBehavior.AllowGet);
            }

            db.Events.Remove(eventToDelete);
            db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}
