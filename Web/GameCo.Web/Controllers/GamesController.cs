using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GameCo.WebModels.Binding;
using GameCo.Services;
using GameCo.Services.Models.Games;
using Microsoft.AspNetCore.Identity;
using GameCo.Data.Models;
using System.Security.Claims;

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
        public async Task<IActionResult> SetRatingValue([FromBody] int ratingValue, CreateRatingBindingModel ratingBindingModel)
        {

            RatingServiceModel ratingServiceModel = this.mappingService.MapOject<RatingServiceModel>(ratingBindingModel);

            ratingServiceModel.RatingValue = ratingValue;

            ratingServiceModel.UserId = GetUserId();

            //TODO add and gameId

            bool result = await this.gameService.CreateRating(ratingServiceModel);

            return Redirect("/");
        }


        private string GetUserId()
        {
            var currUser = userManager.GetUserAsync(HttpContext.User);

            return ((ClaimsIdentity)this.User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
         
        }
    }
}
