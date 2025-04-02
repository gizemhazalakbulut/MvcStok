using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;  //Modeli kütüphane gibi yazarız
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcDbStokEntities db = new MvcDbStokEntities(); // MvcDbStokEntities modelimi tutuyor,modelimin içinde de tablolar var. Tablolara ulaşmak için db nesnesine ihtiyacım var.
        public ActionResult Index(int sayfa=1)
        {
            /*var degerler = db.TBLKATEGORILER.ToList();*/  // tolist metoduyla değerlerimi listelemiş olucam select sorgusu gibi yani
            var degerler =db.TBLKATEGORILER.ToList().ToPagedList(sayfa,4);
            return View(degerler); //geriye degerleri döndürsün
        }

        [HttpGet] //sayfa ilk yüklendiğinde sadece sayfayı getir (sayfa yüklenince bunu yap)
        public ActionResult YeniKategori()
        { 
            return View(); 
        }

        [HttpPost] //arka tarafta yani form içinde bir post yani gönderme işlemi yaparsam o zaman bu işlemi yap yani yeni kategori ekle  (ben butona tıklayınca bunu yap)
        public ActionResult YeniKategori(TBLKATEGORILER p1)
        {
            if(!ModelState.IsValid) //doğrulama işlemi yapılmadıysa yenikategori viewini geri döndürsün
            {
                return View("YeniKategori");
            }
            db.TBLKATEGORILER.Add(p1);
            db.SaveChanges();
            return View();  //bana sayfayı geri döndür
        }

        public ActionResult SIL(int id)
        {
            var kategori=db.TBLKATEGORILER.Find(id); // id den gelen degeri bul
            db.TBLKATEGORILER.Remove(kategori); //kategoriden gelen degeri sil 
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.TBLKATEGORILER.Find(id);
            return View("KategoriGetir",ktgr);

        }

        public ActionResult Guncelle(TBLKATEGORILER p1)
        {
            var ktg = db.TBLKATEGORILER.Find(p1.KATEGORIID);
            ktg.KATEGORIAD = p1.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}