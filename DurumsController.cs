using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SahipsizHayvanlar1.Models;
using static SahipsizHayvanlar1.Models.dbmodel;

namespace SahipsizHayvanlar1.Controllers
{
    [Authorize]
    public class DurumsController : Controller
    {
        private SahipsizHayvanlar1Entities2 db = new SahipsizHayvanlar1Entities2();
        LogsController logsController = new LogsController();


        void doldur(int id)
        {
           var drm = db.Durum.ToList();
           var aa = drm.Where(o => o.IhbarId == id).FirstOrDefault();
           ViewBag.bb = aa;
        }


        // GET: Durums
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (Session["PersonelId"] == null)
            {
                 return Redirect("~/Login/Index");
            }
            else
            {
                var getList = db.Durum.Where(x => x.DurumSilindi == false).ToList();
                return View(getList.ToList());
            }
        }

        void dropdown()
        {
            var getProtokolNo = db.Durum.ToList();
            SelectList list = new SelectList(getProtokolNo, "IhbarId", "ProtokolNo");
            ViewBag.ProtokolNoList2 = list;
        }
        // GET: Durums/Create
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
                if (Yetkilendirme.Yetki == "admin" || Yetkilendirme.Yetki == "yönetici")
                {
                    dropdown();
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Personels");
                }

            }
        }


        // POST: Durums/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SahipsizHayvanlar1.Models.Durum durum)
        {
            var user = Session["PersonelId"] as SahipsizHayvanlar1.Models.Personel;

            if (Session["PersonelId"] == null)
            {
                 return Redirect("~/Login/Index");
            }
            else
            {
                dropdown();
                if (ModelState.IsValid)
                {
                    db.Durum.Add(durum);
                    //IhbarDurum model = new IhbarDurum();
                    //model.durum = durum;

                    db.SaveChanges();
                    logsController.Loglama(user, "Eklendi", "Durum Tablosuna " + durum.AcıklamaId + " İd'li " + durum.IhbarId + " İhbar Id ye Sahip Durum Eklendi");
                    return RedirectToAction("Index", "Ihbars");
                }

                return View();
            }
        }



        // GET: Durums/Edit/5
        void DurumEditDropdown()
        {
            var getProtokolNo = db.Ihbar.ToList();
            SelectList list = new SelectList(getProtokolNo, "IhbarId", "ProtokolNo");
            ViewBag.ProtokolNoList = list;
        }

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
                if (Yetkilendirme.Yetki == "admin" || Yetkilendirme.Yetki == "yönetici" || Yetkilendirme.Yetki == "user")
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Durum durum = db.Durum.Find(id);
                    if (durum == null)
                    {
                        return HttpNotFound();
                    }
                    DurumEditDropdown();
                    return View(durum);
                }
                else
                {
                    return RedirectToAction("Index", "Personels");
                }

            }
        }

        // POST: Durums/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SahipsizHayvanlar1.Models.Durum durum)
        {
            var user = Session["PersonelId"] as SahipsizHayvanlar1.Models.Personel;

            if (Session["PersonelId"] == null)
            {
                 return Redirect("~/Login/Index");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(durum).State = EntityState.Modified;
                    db.SaveChanges();
                    logsController.Loglama(user, "Güncellendi", "Durum Tablosuna " + durum.AcıklamaId + " İd'li " + durum.IhbarId + " İhbar Id ye Sahip Durum Güncellendi");
                    return RedirectToAction("Index", "Ihbars");
                }
                return View(durum);
            }
        }

        // GET: Durums/Delete/5
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
                if (Yetkilendirme.Yetki == "admin" || Yetkilendirme.Yetki == "yönetici")
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Durum durum = db.Durum.Find(id);
                    if (durum == null)
                    {
                        return HttpNotFound();
                    }
                    return View(durum);
                }
                else
                {
                    return RedirectToAction("Index", "Personels");
                }
            }
        }

        // POST: Durums/Delete/5
        [AllowAnonymous]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = Session["PersonelId"] as SahipsizHayvanlar1.Models.Personel;

            using (SahipsizHayvanlar1Entities2 db = new SahipsizHayvanlar1Entities2())
                if (Session["PersonelId"] == null)
                {
                    return Redirect("~/Login/Index");
                }
                else
                {
                    Durum durum = new Durum();
                    durum = db.Durum.Where(x => x.AcıklamaId == id).FirstOrDefault();
                    durum.DurumSilindi = true;
                    db.SaveChanges();
                    logsController.Loglama(user, "Silindi", "Durum Tablosuna " + durum.AcıklamaId + " İd'li " + durum.IhbarId + " İhbar Id ye Sahip Durum Silindi");


                    return RedirectToAction("Index", "Ihbars");
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
