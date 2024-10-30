using EventManagerMvc.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Helpers;
using System.Web.Mvc;

namespace EventManagerMvc.Controllers
{
    public class EventController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(db.Events.ToList());
        }

        [HttpGet]
        public JsonResult GetEvents()
        {
            try
            {
                var eventsFromDb = db.Events.ToList();

                var events = eventsFromDb.Select(e => new
                {
                    id = e.Id,
                    title = e.Name,
                    start = e.Date.ToString("yyyy-MM-ddTHH:mm:ss"),
                    description = e.Description
                }).ToList();

                return Json(events, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event eventt = db.Events.Find(id);
            if (eventt == null)
            {
                return HttpNotFound();
            }
            return View(eventt);
        }

        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Date,Description")] Event eventt)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(eventt.UserId);
                eventt.User = user;
                E_mail(eventt);
                db.Events.Add(eventt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eventt);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event eventt = db.Events.Find(id);
            if (eventt == null)
            {
                return HttpNotFound();
            }
            return View(eventt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Date,Description")] Event eventt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eventt);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event eventt = db.Events.Find(id);
            if (eventt == null)
            {
                return HttpNotFound();
            }
            return View(eventt);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event eventt = db.Events.Find(id);
            db.Events.Remove(eventt);
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
        public void E_mail(Event eventt)
        {
            var user = db.Users.Find(eventt.UserId);
            eventt.User = user;
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "nepridumalnazvaniepocht@gmail.com";
                WebMail.Password = "rnlt mfvn ftjb usxu";
                WebMail.From = "nepridumalnazvaniepocht@gmail.com";
                WebMail.Send(user.Email, "Uus event!", eventt.Name + " " + eventt.Date + " " + eventt.Description);
                ViewBag.Message = "Kiri on saatnud!";
            }
            catch
            {
                ViewBag.Message = "Mul on kahju! Ei saa kirja saada!!!";
            }
        }
    }
}
