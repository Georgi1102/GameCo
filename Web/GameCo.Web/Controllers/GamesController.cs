using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace GameCo.Web.Controllers
{
    public class GamesController : Controller
    {    
        [HttpGet]
        public IActionResult CreateGame()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGame(CreateGameBindigModel model)
        {
           return Redirect("/");
        }
    }
}
