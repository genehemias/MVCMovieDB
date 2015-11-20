using MVCOdysseyTwo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCOdysseyTwo.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Index()
        {
            using (var db = new MovieDBContext())
            {
                var movies = db.Movies.ToList();

                return View(movies);
            }                
        }

        [HttpGet]
        public ActionResult Add()
        {
            Movie movie = new Movie();

            return View(movie);
        }

        [HttpPost]
        public ActionResult Add(Movie movieFromView)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MovieDBContext())
                {
                    db.Movies.Add(movieFromView);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(movieFromView);
        }

        //these are the Edit and Delete actions from the tutorial. They result in a DbUpdateConcurrency Exception (they don't work)
        //because the views lacked @Html.HiddenFor(model.=>model.MovieID)
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            using (var db = new MovieDBContext())
            {
                Movie movie = db.Movies.Find(id);
                if (movie == null)
                {
                    return HttpNotFound();
                }

                return View(movie);
            }
        }

        [HttpPost]
        public ActionResult Edit(Movie movieToUpdate)
        {
            
                if (ModelState.IsValid)
                {
                    using (var db = new MovieDBContext())
                    {
                        db.Entry(movieToUpdate).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(movieToUpdate);           
        }


        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            using (var db = new MovieDBContext())
            {
                Movie movie = db.Movies.Find(id);
                if (movie == null)
                {
                    return HttpNotFound();
                }

                return View(movie);
            }
        }

        [HttpPost]
        public ActionResult Delete(Movie movieToDelete)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MovieDBContext())
                {
                    db.Entry(movieToDelete).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(movieToDelete);
        }     
    }
}