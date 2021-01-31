using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GameCo.Data;
using GameCo.Data.Models;
using GameCo.Services.Models.Games;

namespace GameCo.Services
{
    public class GameService : IGamesService
    {
        private readonly GameCoDbContext gameCoDbContext;
        private readonly IMappingService mappingService;

        public GameService(GameCoDbContext gameCoDbContext, IMappingService mappingService)
        {
            this.gameCoDbContext = gameCoDbContext;
            this.mappingService = mappingService;
        }

        public async Task<bool> CreateGame(GameServiceModel gameServiceModel)
        {
            GameCoGames game = this.mappingService.MapOject<GameCoGames>(gameServiceModel);

            game.Id = Guid.NewGuid().ToString();

            bool result = await this.gameCoDbContext.AddAsync(game) != null;
            await this.gameCoDbContext.SaveChangesAsync();

            return result;
        }

       
    }
}
