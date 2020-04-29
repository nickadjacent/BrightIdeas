using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BrightIdeas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace BrightIdeas.Controllers
{
    public class HomeController : Controller
    {
        private BrightIdeasContext db;



        private int? uid
        {
            get
            {
                return HttpContext.Session.GetInt32("UserId");
            }
        }



        public HomeController(BrightIdeasContext context)
        {
            db = context;
        }



        public IActionResult Index()
        {
            if (uid != null)
            {
                return RedirectToAction("BrightIdeas", "BrightIdeas");
            }

            return View();
        }



        [HttpPost("/register")]
        public IActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(user => user.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "is taken");
                    return View("Index");
                }
            }

            if (ModelState.IsValid)
            {
                if (db.Users.Any(user => user.Alias == newUser.Alias))
                {
                    ModelState.AddModelError("Alias", "is taken");
                    return View("Index");
                }
            }

            else
            {
                return View("Index");
            }


            PasswordHasher<User> hasher = new PasswordHasher<User>();
            newUser.Password = hasher.HashPassword(newUser, newUser.Password);

            db.Users.Add(newUser);
            db.SaveChanges();

            // add to session, they are now "logged in"
            HttpContext.Session.SetInt32("UserId", newUser.UserId);
            HttpContext.Session.SetString("Alias", newUser.Alias);
            return RedirectToAction("BrightIdeas", "BrightIdeas");
        }



        [HttpPost("/login")]
        public IActionResult Login(LoginUser loginUser)
        {
            if (ModelState.IsValid == false)
            {
                return View("Index");
            }

            User dbUser = db.Users.FirstOrDefault(user => user.Email == loginUser.LoginEmail);

            if (dbUser == null)
            {
                ModelState.AddModelError("LoginEmail", "Email not found");
            }
            else
            {
                PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
                PasswordVerificationResult pwCompareResult = hasher.VerifyHashedPassword(loginUser, dbUser.Password, loginUser.LoginPassword);

                if (pwCompareResult == 0)
                {
                    ModelState.AddModelError("LoginPassword", "Password mismatch");
                }
            }

            if (ModelState.IsValid == false)
            {
                return View("Index");
            }

            HttpContext.Session.SetInt32("UserId", dbUser.UserId);
            HttpContext.Session.SetString("Alias", dbUser.Alias);
            return RedirectToAction("BrightIdeas", "BrightIdeas");
        }



        [HttpGet("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }



        public IActionResult Privacy()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
