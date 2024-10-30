using EventManagerMvc.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace EventManagerMvc.Controllers
{
    public class EventController : Controller
    {
        private EventContext db = new EventContext();

        // GET: Event
        public ActionResult Index()
        {
            return View(db.Events.ToList());
        }

        [HttpGet]
        public JsonResult GetEvents()
        {
            try
            {
                // Сначала выполните запрос и загрузите все события из базы данных
                var eventsFromDb = db.Events.ToList();

                // Затем преобразуйте данные в нужный формат
                var events = eventsFromDb.Select(e => new
                {
                    id = e.Id,
                    title = e.Name,
                    start = e.Date.ToString("yyyy-MM-ddTHH:mm:ss"), // Форматируем дату после выборки из БД
                    description = e.Description
                }).ToList();

                return Json(events, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }





        // GET: Event/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Date,Description")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@event);
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Date,Description")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
