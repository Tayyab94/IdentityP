﻿using IdentityProjectPractice.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityProjectPractice.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        public Boolean isAdminUser()
        {
            if(User.Identity.IsAuthenticated)
            {
                var user = User.Identity;

                ApplicationDbContext _context = new ApplicationDbContext();

                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
            
                var s = userManager.GetRoles(user.GetUserId());
           //     userManager.GetEmail(user.GetUserId());
                if(s[0].ToString()=="Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        // GET: Users
        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;

                ViewBag.displayMenu = "No";

                if(isAdminUser())
                {

                    ViewBag.displayMenu = "Yes";
                }
                return View();
            }
            else
            {
                ViewBag.Name ="Not LogedIn!";
            }
            return View();
        }
    }
}