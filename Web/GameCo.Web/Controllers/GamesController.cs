using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GameCo.WebModels.Binding;
using GameCo.Services;
using GameCo.Services.Models.Games;

namespace GameCo.Web.Controllers
{
    public class GamesController : Controller
    {
        private readonly IMappingService mappingService;
        private readonly IGamesService gameService;

        public GamesController(IMappingService mappingService, IGamesService gameService)
        {
            this.mappingService = mappingService;
            this.gameService = gameService;
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
    }
}
