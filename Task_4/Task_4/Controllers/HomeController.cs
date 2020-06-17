using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Task_4.Data;
using Task_4.Models;

namespace Task_4.Controllers
{
    [Authorize(Roles = "Unblock")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext applicationDbContext;
        RoleManager<IdentityRole> roleManager;


        public HomeController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext)
        {
            logger = logger;
            this.userManager = userManager;
            this.applicationDbContext = applicationDbContext;
            this.roleManager = roleManager;
        }

       
        public async Task<IActionResult> Index()
        {
            var result = await(from user in applicationDbContext.Users
                               join userRole in applicationDbContext.UserRoles on user.Id equals userRole.UserId
                               join role in applicationDbContext.Roles on userRole.RoleId equals role.Id
                               select new ViewUser{ Id = user.Id, UserName = user.UserName, Email = user.Email, RegistrationDate = user.RegistrationDate, LoginDate = user.LoginDate, Role = role.Name }).ToListAsync(); 
                
            return View(result);
        }

        public async Task<JsonResult> Delete(string[] id)
        {
            try 
            {
                foreach (var idDelete in id)
                {
                    ApplicationUser user = await userManager.FindByIdAsync(idDelete);
                    if (user != null)
                    {
                        await userManager.DeleteAsync(user);
                    }
                }
                return new JsonResult(1);
            }
            catch 
            {
                return new JsonResult(0);
            }
        }
        public async Task<JsonResult> EditStatus(string[] id, string block)
        {
            try
            {
                foreach (var idDelete in id)
                {
                    ApplicationUser user = await userManager.FindByIdAsync(idDelete);
                   
                    if (user != null)
                    {
                        if (block.Equals("Unblock"))
                        {
                            await userManager.AddToRoleAsync(user, "Unblock");
                            await userManager.RemoveFromRoleAsync(user, "Block");
                        }
                        else
                        {
                            await userManager.AddToRoleAsync(user, "Block");
                            await userManager.RemoveFromRoleAsync(user, "Unblock");
                        }
                    }
                }
                return new JsonResult(1);
            }
            catch 
            {
                return new JsonResult(0);
            } 
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
