using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using SahipsizHayvanlar1.Models;

namespace SahipsizHayvanlar1.Controllers
{
    [Authorize]
    public class PersonelsController : Controller
    {
        private SahipsizHayvanlar1Entities2 db = new SahipsizHayvanlar1Entities2();
        LogsController logsController = new LogsController();
     
        // GET: Personels
        [AllowAnonymous]
        public ActionResult Index()
        {
            var Yetkilendirme = Session["PersonelId"] as SahipsizHayvanlar1.Models.Personel;
            if (Session["PersonelId"] == null)
            {
                return Redirect("~/Login/Index");
            }
            else
            {

                if (Yetkilendirme.Yetki == "admin")
                {
                    var getList = db.Personel.Where(x => x.PersonelSilindi == false).ToList();
                    return View(getList.ToList());
                }
                else
                {
                    return RedirectToAction("Hata", "Personels");
                }
            }    
        }

        // GET: Personels/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (Session["PersonelId"] == null)
            {
                return Redirect("~/Login/Index");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Personel personel = db.Personel.Find(id);
                if (personel == null)
                {
                    return HttpNotFound();
                }
                return View(personel);
            }
        }
        // GET: Personels/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            var Yetkilendirme = Session["PersonelId"] as SahipsizHayvanlar1.Models.Personel;
            if (Session["PersonelId"] == null)
            {
                return Redirect("~/Login/Index");
            }
            else
            {
                if (Yetkilendirme.Yetki == "admin")
                {
                    List<SelectListItem> Yetkiler = new List<SelectListItem>();


                    Yetkiler.Add(new SelectListItem { Text = "ADMİN", Value = "admin" });
                    Yetkiler.Add(new SelectListItem { Text = "YÖNETİCİ", Value = "yönetici" });
                    Yetkiler.Add(new SelectListItem { Text = "USER", Value = "user" });

                    ViewBag.Yetki = Yetkiler;

                    return View();
                }
                else
                {
                    return RedirectToAction("Hata", "Personels");
                }
            }
        }
        // POST: Personels/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SahipsizHayvanlar1.Models.Personel personel)
        {
            var user = Session["PersonelId"] as SahipsizHayvanlar1.Models.Personel;

            MD5 md5Hash = MD5.Create();
            var hashsifre = new Hash().GetMd5Hash(md5Hash, personel.Sifre);

            if (Session["PersonelId"] == null)
            {
                return Redirect("~/Login/Index");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    personel.KulAdi = personel.KulAdi;
                    personel.Sifre = hashsifre;
                    db.Personel.Add(personel);
                    db.SaveChanges();
                    logsController.Loglama(user, "Eklendi", "Personel Tablosunda " + personel.PersonelId + " İd'li " + personel.PersonelAdi + " Kişisi eklendi");
                    return RedirectToAction("Index");
                }
                return View();
            }
        }

        // GET: Personels/Edit/5
        [AllowAnonymous]
        public ActionResult Edit(int? id)
        {
            var Yetkilendirme = Session["PersonelId"] as SahipsizHayvanlar1.Models.Personel;
            if (Session["PersonelId"] == null)
            {
                return Redirect("~/Login/Index");
            }
            else
            {
                if (Yetkilendirme.Yetki == "admin")
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Personel personel = db.Personel.Find(id);
                    if (personel == null)
                    {
                        return HttpNotFound();
                    }
                    List<SelectListItem> Yetkiler = new List<SelectListItem>();
                    Yetkiler.Add(new SelectListItem { Text = "ADMİN", Value = "admin", Selected = personel.Yetki == "admin" });
                    Yetkiler.Add(new SelectListItem { Text = "YÖNETİCİ", Value = "yönetici", Selected = personel.Yetki == "yönetici" });
                    Yetkiler.Add(new SelectListItem { Text = "USER", Value = "user", Selected = personel.Yetki == "user" });

                    ViewBag.Yetki = Yetkiler;
                    return View(personel);

                }
                else
                {
                    return RedirectToAction("Hata", "Personels");
                }
            }
        }

        // POST: Personels/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SahipsizHayvanlar1.Models.Personel personel)
        {
            var user = Session["PersonelId"] as SahipsizHayvanlar1.Models.Personel;
            if (ModelState.IsValid)
            {
                if (personel.Yetki == null)
                {
                    ViewBag.mesaj = "Yetki Seçiniz";
                    return View();
                }
                db.Entry(personel).State = EntityState.Modified;
                db.SaveChanges();
                logsController.Loglama(user, "Güncellendi", "Personel Tablosunda " + personel.PersonelId + " İd'li " + personel.PersonelAdi + " Kişisini Güncelledi");
                return RedirectToAction("Index");
            }
            return View(personel);
        }
        //Get:Personels/Home/5
        [AllowAnonymous]
        public ActionResult Home()
        {
            if (Session["PersonelId"] == null)
            {
                return Redirect("~/Login/Index");
            }
            else
            {
                return View();
            }
        }

        //Get:Personels/Hata/5
        [AllowAnonymous]
        public ActionResult Hata()
        {
            return View();
        }
        // GET: Personels/Delete/5
        [AllowAnonymous]
        public ActionResult Delete(int? id)
        {
            var Yetkilendirme = Session["PersonelId"] as SahipsizHayvanlar1.Models.Personel;
            if (Session["PersonelId"] == null)
            {
                return Redirect("~/Login/Index");
            }
            else
            {

                if (Yetkilendirme.Yetki == "admin")
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Personel personel = db.Personel.Find(id);
                    if (personel == null)
                    {
                        return HttpNotFound();
                    }

                    return View(personel);
                }
                else
                {
                    return RedirectToAction("Hata", "Personels");
                }

            }
        }

        // POST: Personels/Delete/5
        [AllowAnonymous]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = Session["PersonelId"] as SahipsizHayvanlar1.Models.Personel;

            if (Session["PersonelId"] == null)
            {
                return Redirect("~/Login/Index");
            }
            else
            {
                using (SahipsizHayvanlar1Entities2 db = new SahipsizHayvanlar1Entities2())

                {
                    Personel personel = new Personel();
                    personel = db.Personel.Where(x => x.PersonelId == id).FirstOrDefault();
                    personel.PersonelSilindi = true;
                    db.SaveChanges();
                    logsController.Loglama(user, "Silindi", "Personel Tablosunda " + personel.PersonelId + " İd'li " + personel.PersonelAdi + " Kişisi Silindi");

                }
                return RedirectToAction("Index");
            }
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
