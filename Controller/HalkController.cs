using CaptchaMvc.HtmlHelpers;
using SahipsizHayvanlar1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SahipsizHayvanlar1.Controllers
{
    [Authorize]
    public class HalkController : Controller
    {
        // GET: Halk
        private SahipsizHayvanlar1Entities2 db = new SahipsizHayvanlar1Entities2();
       
        // GET: Halk

        [HttpGet]
        [OverrideAuthorization]

        public ActionResult Index()
        {
            List<Durum> liste = new List<Durum>();
            return View(liste);
        }

        [HttpPost]
        [OverrideAuthorization]

        // Post: Index
        public ActionResult Index(int ProtokolNo)
        {
            ViewBag.durum = false;
            var sorgu = from du in db.Durum
                        join ih in db.Ihbar on du.IhbarId equals ih.IhbarId
                        where ih.ProtokolNo == ProtokolNo  &&  du.DurumSilindi==false
                        select du;
               if (!this.IsCaptchaValid("Captcha is not valid"))
            {
                ViewBag.ErrorMessage = "Geçersiz Doğrulama Kodu!";
                List<Durum> liste = new List<Durum>();
                return View(liste);
               

            }
            else {
                return View(sorgu.ToList());
            }
       
        }

    }
}