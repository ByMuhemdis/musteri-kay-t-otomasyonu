using BusinessLayer.Concrete;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace musteri_kayıt_otomasyonu.Controllers
{
    public class adminController : Controller
    {
        tbl_musteriRepository musteri =new tbl_musteriRepository();
      
        denemeEntities db = new denemeEntities();

        //proje katmanlı mimariye uygun   sekilde yapılmıştır 
        //projede Dbfirst yakılaşımıyla ypılmıştır 
        //musterinin adını konrol eip en az üç sesli harf varsa onu tabloda belirmekteyiz
        //tek tablo kullanılmıştır 
        //silme işlleminde sadece müşterinin durumu = false çekilmekte ve databaseden veri kaybı yaşamamak için silinmemektedir.
        //Ajax kullanarak da istenirse databaseden tamamen silme işlemi yapılmıstır 
        //projeye arama kısmı olusturulup isim alanına girilen degerlere göre müşteri durumu true ise müşteriler listelenmektedir 
        //silinen musterileri daha sonra aktif edebilecegimiz bir sayfa olusturdum buradan musteriyi arayabilir(bu aramadada musterilerin durmu false olanlar geliyor)
        //ve aktif edebilirsiniz
      

        // GET: admin
        public ActionResult Index(string p)
        {
            var degerler = from d in db.tbl_musteri select d;

            if (!string.IsNullOrEmpty(p))
            {
                //arama kısmında girilen deger üzerinden arama yapıyor
                degerler = degerler.Where(m => m.ad.Contains(p));
            
                //musteri silme işleminden sonra durumu true olanları listeliyor 
                degerler = degerler.Where(m => m.durum==true);
               //yada index sayfasında şart ile durumu kontrol ettirip true olanlarıda çagırabılırız. 
            }
            degerler = degerler.Where(m => m.durum == true);
            return View(degerler.ToList());

        }
        //silinen müşterileri görmek için 

        public ActionResult hepsi(string p)
        {
            var degerler = from d in db.tbl_musteri select d;
            

            

            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.ad.Contains(p));
                //arama kısmında girilen deger üzerinden arama yapıyor
                degerler = degerler.Where(m => m.durum == false);

               
            }
            degerler = degerler.Where(m => m.durum == false);
            return View(degerler.ToList());

        }


        [HttpGet]

        public ActionResult Create()
        {
          
            return View();
        }
        [HttpPost]
        public ActionResult Create(tbl_musteri data)
        {

            
            data.durum = true;
           
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Hata oluştu.");

            }

            musteri.Insert(data);//datadan gelen verileri ekle
                                  
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            var update=musteri.GetById(id);
            return View(update);
        }
        [HttpPost]
        public ActionResult Update(tbl_musteri data)
        {
               var update = musteri.GetById(data.id);

           
                update.ad = data.ad;
                update.soyad = data.soyad;
                update.tc = data.tc;
                update.mail = data.mail;
                update.cep_tel = data.cep_tel;
                update.ev_tel = data.ev_tel;
                update.is_tel = data.is_tel;
                update.ev_adress = data.ev_adress;
                update.is_adress = data.is_adress;
               

                musteri.Update(update);
           
                return RedirectToAction("Index");

          

        }
        [HttpGet]

        //kalıcı silme
        public ActionResult Delete(int id)
        {
            var delete = musteri.GetById(id);
            musteri.Delete(delete);
            return RedirectToAction("Index");

        }
        //durumu pasif yapma
        public ActionResult Delete1(int id)
        {

            var update = musteri.GetById(id);
            update.durum = false;
            musteri.Update(update);
            return RedirectToAction("Index");


        }
        //durmu aktif yapma
        public ActionResult Delete2(int id)
        {

            var update = musteri.GetById(id);
            update.durum =true ;
            musteri.Update(update);
            return RedirectToAction("Index");


        }
    }
}