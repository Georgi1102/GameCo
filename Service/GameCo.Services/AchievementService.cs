using GameCo.Data;
using GameCo.Data.Models;
using GameCo.Services.Models.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCo.Services
{
    public class AchievementService : IAchievementsService
    {
        private readonly GameCoDbContext gameCoDbContext;
        private readonly IMappingService mappingService;
        //private readonly GameServiceModel game;

        public AchievementService(GameCoDbContext gameCoDbContext, IMappingService mappingService)// GameServiceModel game)
        {
            this.gameCoDbContext = gameCoDbContext;
            this.mappingService = mappingService;
           // this.game = game;
        }


        public async Task<bool> CreateAchievement(AchievementServiceModel achievementServiceModel)
        {
            GameCoAchievements achievements = this.mappingService.MapOject<GameCoAchievements>(achievementServiceModel);

            achievements.Id = Guid.NewGuid().ToString();

            bool result = await this.gameCoDbContext.AddAsync(achievements) != null;

            await this.gameCoDbContext.SaveChangesAsync();

            return result;
        }
    }
}
