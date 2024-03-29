﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OHET_Project.Models.models;
using OHET_Project.Persistence;
using Microsoft.AspNet.Identity;

namespace OHET_Project.Controllers
{
    [Authorize(Roles = "Admin, Editor, User")]
    public class HeroesController : Controller
    {
        private Persistence.DbContext db = new Persistence.DbContext();

        // GET: Heroes
        public ActionResult Index()
        {
            ViewBag.userId = User.Identity.GetUserId();
            var heroes = db.heroes.Include(h => h.Content).Include(u => u.Class);
            return View(heroes.ToList());
        }

        // GET: Heroes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hero hero = db.heroes.Include(h => h.Class).Where(h => h.IDHero == id).FirstOrDefault();
            if (hero == null)
            {
                return HttpNotFound();
            }
            return View(hero);
        }

        // GET: Heroes/Create
        public ActionResult Create()
        {
            //ViewBag.IDConcept = new SelectList(db.concepts, "IDConcept", "description");
            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId");

            return View();
        }

        // GET:
        public ActionResult _createClassChoice()
        {
            ViewBag.userId = User.Identity.GetUserId();
            List<Class> classes = db.classes.Include(c => c.Content).ToList();

            return PartialView("_createClassChoice", classes);
        }

        // ?POST:
        public ActionResult _createClassChoiceResult(string classID)
        {
            ViewBag.classID = classID;

            return View("Create");
        }

        // GET
        public ActionResult _createPersonalData(string classID)
        {
            ViewBag.classID = classID;

            return PartialView();
        }

        // ?POST:
        public ActionResult _createPersonalDataResult(string classID, string heroName, string description, string background, string appearance, string character)
        {
            ViewBag.classID = classID;
            ViewBag.heroName = heroName;
            ViewBag.description = description;
            ViewBag.background = background;
            ViewBag.appearance = appearance;
            ViewBag.character = character;

            return View("Create");
        }

        // GET
        public ActionResult _createAttributesData(string classID, string heroName, string description, string background, string appearance, string character)
        {
            ViewBag.classID = classID;
            ViewBag.heroName = heroName;
            ViewBag.description = description;
            ViewBag.background = background;
            ViewBag.appearance = appearance;
            ViewBag.character = character;

            return PartialView();
        }

        // ?POST:
        public ActionResult _createAttributesDataResult(string classID, string heroName, string description,
            string background, string appearance, string character,
            AttributeLvl StrAttribute, AttributeLvl DexAttribute, AttributeLvl ConAttribute, AttributeLvl IntAttribute, 
            AttributeLvl WisAttribute, AttributeLvl ChaAttribute)
        {
            ViewBag.classID = classID;
            ViewBag.heroName = heroName;
            ViewBag.description = description;

            ViewBag.background = background;
            ViewBag.appearance = appearance;
            ViewBag.character = character;

            ViewBag.StrAttribute = StrAttribute;
            ViewBag.DexAttribute = DexAttribute;
            ViewBag.ConAttribute = ConAttribute;
            ViewBag.IntAttribute = IntAttribute;
            ViewBag.WisAttribute = WisAttribute;
            ViewBag.ChaAttribute = ChaAttribute;

            int heroID = SaveNewHero(classID, heroName, description, background, appearance, character,
                StrAttribute, DexAttribute, ConAttribute, IntAttribute, WisAttribute, ChaAttribute);

            return RedirectToAction("Details", "Heroes", new { id = heroID });
        }

        public int SaveNewHero(string _classID, string _heroName, string _description,
            string _background, string _appearance, string _character,
            AttributeLvl _StrAttribute, AttributeLvl _DexAttribute, AttributeLvl _ConAttribute, 
            AttributeLvl _IntAttribute, AttributeLvl _WisAttribute, AttributeLvl _ChaAttribute)
        {

            int idClass = Int32.Parse(_classID);
            var x = db.classes.Where(p => p.IDClass == idClass).FirstOrDefault();


            Hero hero = new Hero
            {
                name = _heroName,
                conceptLvl = CountWords(_description),
                description = _description,
                background = _background,
                appearance = _appearance,
                character = _character,
                StrAttribute = _StrAttribute,
                DexAttribute = _DexAttribute,
                ConAttribute = _ConAttribute,
                IntAttribute = _IntAttribute,
                WisAttribute = _WisAttribute,
                ChaAttribute = _ChaAttribute,
                gold = 500,
                exp = 0,
                Content = db.contents.First(u => u.ApplicationUser.UserName == User.Identity.Name),
                IDContent = db.contents.First(u => u.ApplicationUser.UserName == User.Identity.Name).IDContent,
                Class = db.classes.First(p => p.IDClass == idClass),
                IDClass = idClass
            };

            db.heroes.Add(hero);
            db.SaveChanges();

            return hero.IDHero;
        }

        // POST: Heroes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDHero,name,totalDR,gold,exp,IDConcept,IDContent")] Hero hero)
        {
            if (ModelState.IsValid)
            {
                db.heroes.Add(hero);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.IDConcept = new SelectList(db.concepts, "IDConcept", "description", hero.IDConcept);
            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId", hero.IDContent);
            return View(hero);
        }

        // GET: Heroes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hero hero = db.heroes.Include(h => h.Class).Where(h => h.IDHero == id).FirstOrDefault();
            if (hero == null)
            {
                return HttpNotFound();
            }
            return View(hero);
        }

        // POST: Heroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hero hero = db.heroes.Find(id);
            db.heroes.Remove(hero);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Heroes/Edit/5
        [Authorize(Roles = "Admin, Editor, User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Hero hero = db.heroes.Include(h => h.Class).Where(h => h.IDHero == id).FirstOrDefault();

            if (hero == null)
            {
                return HttpNotFound();
            }

            ViewBag.IDContent = new SelectList(db.contents, "IDContent", "ApplicationUserId", hero.IDContent);

            return View(hero);
        }

        // POST: Heroes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([
            Bind(Include = "IDHero,name,description,background,appearance,character,gold,exp,StrAttribute,DexAttribute,ConAttribute,IntAttribute,WisAttribute,ChaAttribute,IDContent,IDClass,Content,Class")] Hero hero)
        {
            hero.conceptLvl = CountWords(hero.description);
            db.Entry(hero).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [DebuggerStepThrough]
        private int CountWords(string str)
        {
            var text = str.Trim();
            int wordCount = 0, index = 0;

            while (index < text.Length)
            {
                while (index < text.Length && !char.IsWhiteSpace(text[index]))
                    index++;

                wordCount++;

                while (index < text.Length && char.IsWhiteSpace(text[index]))
                    index++;
            }

            return wordCount;
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
