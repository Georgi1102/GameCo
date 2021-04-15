using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GameCo.WebModels.Binding;
using GameCo.Services;
using GameCo.Services.Models.Games;
using Microsoft.AspNetCore.Identity;
using GameCo.Data.Models;
using System.Security.Claims;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;


namespace GameCo.Web.Controllers
{
    public class GamesController : Controller
    {
        private readonly IMappingService mappingService;
        private readonly IGamesService gameService;
        private readonly UserManager<GameCoUser> userManager;


        public GamesController(IMappingService mappingService, IGamesService gameService, UserManager<GameCoUser> userManager)
        {
            this.mappingService = mappingService;
            this.gameService = gameService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateGame()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateGame(CreateGameBindingModel createGameBindingModel)
        {
            GameServiceModel gameServiceModel = this.mappingService.MapOject<GameServiceModel>(createGameBindingModel);

            bool result = await this.gameService.CreateGame(gameServiceModel);

            return Redirect("/");
        }


        [HttpPost]
        public async Task<IActionResult> SetRatingValue([FromBody] CreateRatingBindingModel ratingBindingModel)
        {
            RatingServiceModel ratingServiceModel = this.mappingService.MapOject<RatingServiceModel>(ratingBindingModel);

            var userId = ratingServiceModel.UserId = GetUserId();

            var currUserRateEntities = gameService.GetAllRatingEntities(userId);


            if (currUserRateEntities.Count <= 0)
            {               
                bool result = await this.gameService.CreateRating(ratingServiceModel);
            }
           

            else
            {
                bool isInTheIf = false;

                for (int i = 0; i < currUserRateEntities.Count; i++)
                {
                    if (currUserRateEntities[i].GameId == ratingServiceModel.GameId)
                    {
                        isInTheIf = true;

                        currUserRateEntities[i].RatingValue = ratingServiceModel.RatingValue;
                        ratingServiceModel.Id = currUserRateEntities[i].Id;
                        bool result = await gameService.UpdateRating(ratingServiceModel);
                        break;
                    }
                }

                if (!isInTheIf)
                {
                    bool resultOtherResult = await this.gameService.CreateRating(ratingServiceModel);
                }
                           
            }
            
            return Ok();
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
