using GameCo.Data;
using GameCo.Data.Models;
using GameCo.Services;
using GameCo.Services.Models.Games;
using GameCo.Web.Models;
using GameCo.WebModels.Binding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameCo.Web.Controllers
{
    public class AchievementsController : Controller
    {
        private readonly IMappingService mappingService;
        private readonly IAchievementService achService;
        private readonly IGamesService currGame;
        private readonly GameCoDbContext gameCoDbContext;

        public AchievementsController(IMappingService mappingService, IAchievementService achService,
            IGamesService currGame, GameCoDbContext gameCoDbContext)
        {
            this.mappingService = mappingService;
            this.achService = achService;
            this.currGame = currGame;
            this.gameCoDbContext = gameCoDbContext;
        }

        [HttpGet]
        public IActionResult CreateAchievement()
        {
            var list = (from games in gameCoDbContext.Games
                        select new SelectListItem()
                        {
                            Text = games.Name,
                            Value = games.Id.ToString(),
                        }).ToList();

            list.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });

            AchievementViewModel achievementViewModel = new AchievementViewModel();
            achievementViewModel.ListofAchievements = list;

            return View(achievementViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAchievement(CreateAchievementBindingModel createAchievementBindingModel,
            AchievementViewModel achievementViewModel)
        {
            AchievementServiceModel achServiceModel = 
                this.mappingService.MapOject<AchievementServiceModel>(createAchievementBindingModel);

            var selectedValueForGameId = achievementViewModel.GameId;

            achServiceModel.GameId = selectedValueForGameId;

            bool result = await this.achService.CreateAchievement(achServiceModel);


            //var currGameId = currGame.FindGameById(gameId);

            return Redirect("/");
        }





    }
}
