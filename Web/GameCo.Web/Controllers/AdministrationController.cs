using GameCo.Data.Models;
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
        private readonly UserManager<GameCoUser> userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<GameCoUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
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

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var roleToEdit = await roleManager.FindByIdAsync(id);

            if (roleManager == null)
            {
                ViewBag.ErrorMessage = $"Role with the given Id: {id} is not found";
                return View("NotFound");
            }

            //This is the view just bad naming. Welcome to 3am. 
            var model = new EditRoleModel
            {
                Id = roleToEdit.Id,
                RoleName = roleToEdit.Name
            };

            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, roleToEdit.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleModel editRoleModel)
        {
            var roleToEdit = await roleManager.FindByIdAsync(editRoleModel.Id);

            if (roleManager == null)
            {
                ViewBag.ErrorMessage = $"Role with the given Id: {editRoleModel.Id} is not found";
                return View("NotFound");
            }

            else
            {
                roleToEdit.Name = editRoleModel.RoleName;
                var editedResult = await roleManager.UpdateAsync(roleToEdit);

                if (editedResult.Succeeded)
                {
                    return RedirectToAction("ListAllRoles");
                }

                foreach (var error in editedResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            return View(editRoleModel);
        }
    }
}
