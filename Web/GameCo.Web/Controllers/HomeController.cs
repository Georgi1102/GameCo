using GameCo.Data.Models;
using GameCo.Services;
using GameCo.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GameCo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGamesService gameService;
        private readonly UserManager<GameCoUser> userManager;

        public HomeController(ILogger<HomeController> logger, IGamesService gameService, UserManager<GameCoUser> userManager)
        {
            _logger = logger;
            this.gameService = gameService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            

            string userId = GetUserId();

            if (userId == null)
            {
                return View();
            }
            else
            {
                ViewBag.result = gameService.GetAllRatingEntities(userId);
                ViewBag.resultGame = gameService.GetAllGameEntities();

                return View();
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

        private string GetUserId()
        {
            var currUser = userManager.GetUserAsync(HttpContext.User);
            
            if (!currUser.IsCompleted)
            {
                return ((ClaimsIdentity)this.User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            return "";
            
        }
    }
}
