using GameCo.Data;
using GameCo.Data.Models;
using GameCo.Services;
using GameCo.Services.Models.Games;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace AchievementsTests
{
    public class Tests
    {
        private GameCoDbContext gameCoDbContext;
        private IMappingService mappingService;
        private IAchievementsService achievementService;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<GameCoDbContext> options = new DbContextOptionsBuilder<GameCoDbContext>()
                .UseInMemoryDatabase($"TEST-DB-{Guid.NewGuid().ToString()}")
                .Options;

            this.gameCoDbContext = new GameCoDbContext(options);
            this.mappingService = new MappingService();
            this.achievementService = new AchievementService(gameCoDbContext, mappingService);
        }




        [Test]
        public async Task TestIfCreatingAchievementAndSettingInToNotNull()
        {
            AchievementServiceModel achievementServiceModel = new AchievementServiceModel
            {
                Id = "27",
                Name = "TestName",
                Description = "Testdescription",
                GameId = "20"
            };


        }
    }
}