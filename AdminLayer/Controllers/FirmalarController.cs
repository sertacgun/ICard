﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityLayer;
using Microsoft.AspNet.Identity;

namespace AdminLayer.Controllers
{
    [Authorize]
    public class FirmalarController : BaseController
    {
        public ActionResult Index()
        {
            List<Firmalar> r = new List<Firmalar>();
            if (User.IsInRole("Admin"))
            {
                r = db.Firmalar.OrderByDescending(x => x.IsActive).ToList();
            }
            else
            {
                var UId = User.Identity.GetUserId();
                r = db.Firmalar.Where(x => x.UserId == UId).OrderByDescending(x => x.IsActive).ToList();
                // r = db.Firmalar.Where(x=>x.UserId == User.Identity.GetUserId()).OrderByDescending(x => x.IsActive).ToList();
            }
            return View(r);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.users = db.AspNetUsers.ToList();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Firmalar f)
        {

            string qr = Guid.NewGuid().ToString();

            db.Firmalar.Add(new Firmalar
            {
                FirmaAd = f.FirmaAd,
                IsActive = true,
                QR = qr,
                CreateDate = DateTime.Now,
                UserId = f.UserId
            });

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Firmalar f = db.Firmalar.Find(id);
            if (f.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
            {
                ViewBag.users = db.AspNetUsers.ToList();
                return View(f);
            }
            else {
                return RedirectToAction("Index");
            }
          
        }

        [HttpPost]
        public ActionResult Edit(Firmalar f)
        {
            if (f.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
            {
                Firmalar oldF = db.Firmalar.Find(f.Id);
                oldF.FirmaAd = f.FirmaAd;
                oldF.UserId = f.UserId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Firmalar f = db.Firmalar.Find(id);
            if (f.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
            {
                db.Firmalar.Remove(db.Firmalar.Find(id));
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }         
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Active(int id)
        {
            Firmalar f = db.Firmalar.Find(id);
            if (f.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
            {
                f.IsActive = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
           
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Passive(int id)
        {
            Firmalar f = db.Firmalar.Find(id);
            if (f.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
            {
                 f.IsActive = false;
                 db.SaveChanges();
                 return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
           
        }

        #region Contact
        public ActionResult Contact(int id)
        {

            Firmalar f = db.Firmalar.Find(id);
            TempData["FirmaId"] = f.Id;
            return View(f.Iletisim);

        }


        public ActionResult EditContact(int id)
        {
            Firmalar f = db.Firmalar.Find(id);
            if (f.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
            {
                Iletisim I = db.Iletisim.Find(id);
                TempData["FirmaId"] = I.FirmaId;
                return View(I);
            }
            else
            {
                return RedirectToAction("Index");
            }          
        }

        [HttpPost]
        public ActionResult EditContact(Iletisim I)
        {
            Firmalar f = db.Firmalar.Find(I.Id);
            if (f.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
            {
                Iletisim oldI = db.Iletisim.Find(I.Id);
                TempData["FirmaId"] = oldI.FirmaId;

                oldI.Adres = I.Adres;
                oldI.Telefon = I.Telefon;
                oldI.Telefon2 = I.Telefon2;
                db.SaveChanges();

                return RedirectToAction("Contact", new { id = Convert.ToInt32(TempData["FirmaId"]) });
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }
        public ActionResult DeleteContact(int id)
        {
            Firmalar f = db.Firmalar.Find(id);
            if (f.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
            {
                Iletisim I = db.Iletisim.Find(id);
                TempData["FirmaId"] = I.FirmaId;

                db.Iletisim.Remove(db.Iletisim.Find(id));
                db.SaveChanges();

                return RedirectToAction("Contact", new { id = Convert.ToInt32(TempData["FirmaId"]) });
            }
            else
            {
                return RedirectToAction("Index");
            }         
        }

        public ActionResult CreateContact(int id)
        {
            TempData["FirmaId"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult CreateContact(Iletisim I)
        {
            Firmalar f = db.Firmalar.Find(I.Id);
            if (f.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
            {
                db.Iletisim.Add(new Iletisim
                {
                    Adres = I.Adres,
                    Website = I.Website,
                    Telefon = I.Telefon,
                    Telefon2 = I.Telefon2,
                    FirmaId = Convert.ToInt32(TempData["FirmaId"])
                });

                db.SaveChanges();

                return RedirectToAction("Contact", new { id = Convert.ToInt32(TempData["FirmaId"]) });
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }

        #endregion
        #region Bank
        public ActionResult Banka(int id)
        {
            Firmalar f = db.Firmalar.Find(id);
            TempData["FirmaId"] = f.Id;
            return View(f.Banka);
        }


        public ActionResult EditBanka(int id)
        {
            Banka B = db.Banka.Find(id);
            Firmalar f = db.Firmalar.Find(B.Id);
            if (f.UserId == User.Identity.GetUserId() || User.IsInRole("Admin"))
            {
                TempData["FirmaId"] = B.FirmaId;

                return View(B);
            }
            else
            {
                return RedirectToAction("Index");
            }



        }

        [HttpPost]
        public ActionResult EditBanka(Banka B)
        {
            Banka oldB = db.Banka.Find(B.Id);
            TempData["FirmaId"] = oldB.FirmaId;

            oldB.BankaAdi = B.BankaAdi;
            oldB.Iban = B.Iban;
            oldB.SubeKodu = B.SubeKodu;
            db.SaveChanges();

            return RedirectToAction("Banka", new { id = Convert.ToInt32(TempData["FirmaId"]) });
        }
        public ActionResult DeleteBanka(int id)
        {
            Banka B = db.Banka.Find(id);
            TempData["FirmaId"] = B.FirmaId;
            db.Banka.Remove(db.Banka.Find(id));
            db.SaveChanges();

            return RedirectToAction("Banka", new { id = Convert.ToInt32(TempData["FirmaId"]) });
        }

        public ActionResult CreateBanka(int id)
        {
            TempData["FirmaId"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult CreateBanka(Banka B)
        {
            db.Banka.Add(new Banka
            {
                Iban = B.Iban,
                SubeKodu = B.SubeKodu,
                BankaAdi = B.BankaAdi,
                FirmaId = Convert.ToInt32(TempData["FirmaId"])
            });

            db.SaveChanges();

            return RedirectToAction("Banka", new { id = Convert.ToInt32(TempData["FirmaId"]) });
        }
        #endregion
        #region Extra
        public ActionResult Ekstra(int id)
        {
            Firmalar f = db.Firmalar.Find(id);
            TempData["FirmaId"] = f.Id;

            return View(f.Ekstra);
        }


        public ActionResult EditEkstra(int id)
        {
            Ekstra E = db.Ekstra.Find(id);
            TempData["FirmaId"] = E.FirmaId;
            return View(E);
        }

        [HttpPost]
        public ActionResult EditEkstra(Ekstra E)
        {
            Ekstra oldE = db.Ekstra.Find(E.Id);
            TempData["FirmaId"] = oldE.FirmaId;
            oldE.Baslik = E.Baslik;
            oldE.Aciklama = E.Aciklama;
            oldE.Resim = E.Resim;
            db.SaveChanges();
            return RedirectToAction("Ekstra", new { id = Convert.ToInt32(TempData["FirmaId"]) });

        }
        public ActionResult DeleteEkstra(int id)
        {
            Ekstra oldE = db.Ekstra.Find(id);
            TempData["FirmaId"] = oldE.FirmaId;

            db.Ekstra.Remove(db.Ekstra.Find(id));
            db.SaveChanges();
            return RedirectToAction("Ekstra", new { id = Convert.ToInt32(TempData["FirmaId"]) });
        }

        public ActionResult CreateEkstra(int id)
        {
            TempData["FirmaId"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult CreateEkstra(Ekstra E)
        {
            db.Ekstra.Add(new Ekstra
            {
                Baslik = E.Baslik,
                Aciklama = E.Aciklama,
                Resim = E.Resim,
                FirmaId = Convert.ToInt32(TempData["FirmaId"])
            });

            db.SaveChanges();

            return RedirectToAction("Ekstra", new { id = Convert.ToInt32(TempData["FirmaId"]) });
        } 
        #endregion
    }

}