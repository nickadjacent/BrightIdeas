using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BrightIdeas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BrightIdeas.Controllers
{
    public class BrightIdeasController : Controller
    {
        private BrightIdeasContext db;

        private int? uid
        {
            get
            {
                return HttpContext.Session.GetInt32("UserId");
            }
        }

        public BrightIdeasController(BrightIdeasContext context)
        {
            db = context;
        }


        [HttpGet("/bright_ideas")]
        public IActionResult BrightIdeas()
        {
            if (uid == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<BrightIdea> allIdeas = db.BrightIdeas
                .Include(idea => idea.Likes)
                .Include(idea => idea.SubmittedBy)
                .ToList();

            return View("BrightIdeas", allIdeas);
        }






        [HttpPost("/newIdea")]
        public IActionResult Create(BrightIdea newIdea)
        {

            if (uid == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                newIdea.UserId = (int)uid;
                db.Add(newIdea);
                db.SaveChanges();
                return RedirectToAction("BrightIdeas");

            }
            // above return did not run, so not valid, re-render page to display error messages
            return RedirectToAction("New", "BrightIdeas");
        }


        [HttpGet("/new")]
        public IActionResult New()
        {
            if (uid == null)
            {
                return RedirectToAction("BrightIdeas", "BrightIdeas");
            }

            return View("BrightIdeas", "BrightIdeas");
        }



        [HttpGet("/bright_ideas/{userId}")]
        public IActionResult Details(int userId)
        {
            return View("Details");
        }



        [HttpGet("/bright_ideas/{brightIdeaId}/delete")]
        public IActionResult Delete(int brightIdeaId)
        {
            BrightIdea dbIdea = db.BrightIdeas.FirstOrDefault(truck => truck.BrightIdeaId == brightIdeaId);
            db.BrightIdeas.Remove(dbIdea);
            db.SaveChanges();

            return RedirectToAction("BrightIdeas");
        }

    }
}