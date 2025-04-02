using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun

        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLURUNLER.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniUrun()
        {  
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                Text = i.KATEGORIAD,
                                                Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View(); 
        } 

        [HttpPost]
        public ActionResult YeniUrun(TBLURUNLER p3)
        {
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p3.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            p3.TBLKATEGORILER = ktg;
            db.TBLURUNLER.Add(p3);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SIL(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult UrunGetir(int id)
        {
            var urun= db.TBLURUNLER.Find(id);

            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;

            return View("UrunGetir",urun);
        }

        public ActionResult Guncelle(TBLURUNLER p)
        {
            var urun =db.TBLURUNLER.Find(p.URUNID); //işlemleri ürünıd ye göre yapıcaz o id li kayıtı güncelleyeceğimiz için 
            urun.URUNAD = p.URUNAD;
            urun.MARKA = p.MARKA;
            urun.FIYAT = p.FIYAT;
            urun.STOK = p.STOK;
            //urun.URUNKATEGORI = p.URUNKATEGORI;
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            urun.URUNKATEGORI = ktg.KATEGORIID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}