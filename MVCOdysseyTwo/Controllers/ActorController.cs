using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCOdysseyTwo.Models;
using System.Data.Entity;

namespace MVCOdysseyTwo.Controllers
{
    public class ActorController : Controller
    {
        // GET: Actor
        public ActionResult Index()
        {
            using(var db= new MovieDBContext())
            {
                var actors = db.Actors.ToList();
                return View(actors);
            }
            
        }

        
        //Create a new actor 
        [HttpGet]
        public ActionResult Add()
        {
            Actor actor = new Actor();

            return View(actor);
        }
                
        [HttpPost]
        public ActionResult Add(Actor actorFromView)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MovieDBContext())
                {
                    db.Actors.Add(actorFromView);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(actorFromView);
        }

        //Edit
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            using (var db = new MovieDBContext())
            {
                Actor actor = db.Actors.Find(id);
                if (actor == null)
                {
                    return HttpNotFound();
                }

                return View(actor);
            }
        }

        [HttpPost]
        public ActionResult Edit(Actor actorToUpdate)
        {

            if (ModelState.IsValid)
            {
                using (var db = new MovieDBContext())
                {
                    db.Entry(actorToUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(actorToUpdate);
        }

        //Delete
        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            using (var db = new MovieDBContext())
            {
                Actor actor = db.Actors.Find(id);
                if (actor == null)
                {
                    return HttpNotFound();
                }

                return View(actor);
            }
        }

        [HttpPost]
        public ActionResult Delete(Actor actorToDelete)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MovieDBContext())
                {
                    db.Entry(actorToDelete).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(actorToDelete);
        }
    }
}