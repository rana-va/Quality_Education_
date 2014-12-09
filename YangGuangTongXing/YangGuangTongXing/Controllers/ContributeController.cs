using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YangGuangTongXing.Controllers
{
    public class ContributeController : Controller
    {
        //
        // GET: /Contribute/
        YangGuangTongXingEntities db = new YangGuangTongXingEntities();

        public ActionResult Index()
        {
            Announcement ann = db.Announcements.Where(a => a.Remark.Contains("捐助流程")).FirstOrDefault();
            return View(ann);
        }

    }
}
