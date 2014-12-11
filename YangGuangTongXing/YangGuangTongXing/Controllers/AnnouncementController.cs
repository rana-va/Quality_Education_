using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YangGuangTongXing;

namespace YangGuangTongXing.Controllers
{
    public class AnnouncementController : Controller
    {
        private YangGuangTongXingEntities db = new YangGuangTongXingEntities();

        //
        // GET: /Announcement/

        public ViewResult Index()
        {
            var announcements = db.Announcements.Include(a => a.UpdatedUser);
            return View(announcements.ToList());
        }

        //
        // GET: /Announcement/Details/5

        public ViewResult Details(int id)
        {
            Announcement announcement = db.Announcements.Find(id);
            return View(announcement);
        }

        //
        // GET: /Announcement/Create

        public ActionResult Create()
        {
            ViewBag.UpdatedBy = new SelectList(db.Users, "UserId", "UserName");
            return View();
        }

        //
        // POST: /Announcement/Create

        [HttpPost]
        public ActionResult Create(Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                db.Announcements.Add(announcement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UpdatedBy = new SelectList(db.Users, "UserId", "UserName", announcement.UpdatedBy);
            return View(announcement);
        }

        //
        // GET: /Announcement/Edit/5

        [Authorize(Roles = "editor")]
        public ActionResult Edit(int id)
        {
            Announcement announcement = db.Announcements.Find(id);
            ViewBag.UpdatedBy = new SelectList(db.Users, "UserId", "UserName", announcement.UpdatedBy);
            return View(announcement);
        }

        //
        // POST: /Announcement/Edit/5

        [HttpPost]
        public ActionResult Edit(Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(announcement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UpdatedBy = new SelectList(db.Users, "UserId", "UserName", announcement.UpdatedBy);
            return View(announcement);
        }

        [Authorize(Roles = "editor")]
        [HttpPost]
        public bool SaveAnnouncement(int id, string content)
        {
            Announcement announcement = db.Announcements.Find(id);
            announcement.Content = HttpUtility.UrlDecode(content);
            string userName = User.Identity.Name;
            announcement.UpdatedUser = db.Users.SingleOrDefault(u => u.UserName == userName);
            try
            {
                db.Entry(announcement).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            } 
            return true;
        }



        //
        // GET: /Announcement/Delete/5

        public ActionResult Delete(int id)
        {
            Announcement announcement = db.Announcements.Find(id);
            return View(announcement);
        }

        //
        // POST: /Announcement/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Announcement announcement = db.Announcements.Find(id);
            db.Announcements.Remove(announcement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}