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



        // initial route pages



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





        [HttpGet("/users/{userId}")]
        public IActionResult UserDetails(int userId)
        {
            if (uid == null)
            {
                return RedirectToAction("BrightIdeas", "BrightIdeas");
            }

            User selectedUser = db.Users
                .Include(user => user.Ideas)
                .Include(user => user.Likes)
                .FirstOrDefault(user => user.UserId == userId);

            ViewBag.UserId = userId;
            return View("UserDetails", selectedUser);
        }





        [HttpGet("/bright_ideas/{brightIdeaId}")]
        public IActionResult IdeaDetails(int brightIdeaId)
        {
            if (uid == null)
            {
                return RedirectToAction("BrightIdeas", "BrightIdeas");
            }

            BrightIdea selectedIdea = db.BrightIdeas.FirstOrDefault(idea => idea.BrightIdeaId == brightIdeaId);

            if (selectedIdea == null || selectedIdea.UserId != uid)
            {
                return RedirectToAction("BrightIdeas");
            }

            return View("IdeaDetails", selectedIdea);
        }



        // action routes



        [HttpPost("/bright_ideas/create")]
        public IActionResult Create(BrightIdea newIdea)
        {

            if (uid == null)
            {
                return RedirectToAction("BrightIdea", "BrightIdea");
            }

            if (ModelState.IsValid)
            {
                newIdea.UserId = (int)uid;
                db.Add(newIdea);
                db.SaveChanges();
                return RedirectToAction("BrightIdeas");

            }

            return RedirectToAction("BrightIdeas", "BrightIdeas");
        }





        [HttpGet("/bright_ideas/{brightIdeaId}/delete")]
        public IActionResult Delete(int brightIdeaId)
        {
            BrightIdea ideaToDelete = db.BrightIdeas.FirstOrDefault(idea => idea.BrightIdeaId == brightIdeaId);
            db.BrightIdeas.Remove(ideaToDelete);
            db.SaveChanges();

            return RedirectToAction("BrightIdeas", "BrightIdeas");
        }





        [HttpPost("/bright_ideas/like")]
        public IActionResult Like(int brightIdeaId, bool isLiked)
        {

            Like currentLikeStatus = db.Likes.FirstOrDefault(like => like.BrightIdeaId == brightIdeaId && like.UserId == (int)uid);

            if (currentLikeStatus == null)
            {
                Like newLikeSatus = new Like()
                {
                    BrightIdeaId = brightIdeaId,
                    UserId = (int)uid,
                    IsLiked = isLiked,
                };

                db.Likes.Add(newLikeSatus);
            }
            else
            {
                // if already voted, only update if changing vote
                if (currentLikeStatus.IsLiked != isLiked)
                {
                    currentLikeStatus.IsLiked = isLiked;
                    db.Likes.Update(currentLikeStatus);
                }
            }

            db.SaveChanges();
            return RedirectToAction("BrightIdeas", "BrightIdeas");
        }

    }
}