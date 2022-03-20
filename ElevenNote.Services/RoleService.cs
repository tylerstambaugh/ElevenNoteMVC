﻿using ElevenNote.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class RoleService
    {

        public void CreateAdmin()
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ctx));

            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));

            if(!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }
        }

        public void MakeUserAdmin()
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));

            var myUser = ctx.Users.SingleOrDefault(u => u.Email == "email@addr.com");
            if (myUser != null)
            {
                var result = userManager.AddToRole(myUser.Id, "Admin");
            }

        }
    }
}
