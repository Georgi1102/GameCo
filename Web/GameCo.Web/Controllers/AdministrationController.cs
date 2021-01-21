using GameCo.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameCo.Web.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CrateRoleViewModel createModel)
        {
           
            if (ModelState.IsValid)
            {
                IdentityRole identity = new IdentityRole
                {
                    Name = createModel.NameRole
                };
                IdentityResult result = await roleManager.CreateAsync(identity);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListAllRoles", "Administration");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(createModel);
        }

        [HttpGet]
        public IActionResult ListAllRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }
            

    }
}
