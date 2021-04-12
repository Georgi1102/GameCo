using GameCo.Data;
using GameCo.Data.Models;
using GameCo.Services;
using GameCo.Services.Models.Games;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GamesTest
{
    public class UnitTestAchievement
    {
        private GameCoDbContext gameCoDbContext;
        private IMappingService mappingService;
        private IAchievementsService achievementService;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<GameCoDbContext> options = new DbContextOptionsBuilder<GameCoDbContext>()
                .UseInMemoryDatabase($"TESTS-DB-{Guid.NewGuid().ToString()}")
                .Options;

            this.gameCoDbContext = new GameCoDbContext(options);
            this.mappingService = new MappingService();
            this.achievementService = new AchievementService(gameCoDbContext, mappingService);
        }

        #region Achievement tests

        [Test]
        public async Task TestIfCreatingAchievementAndSettingIdToNotNull()
        {
            AchievementServiceModel achievementServiceModel = new AchievementServiceModel
            {
                Id = "27",
                Name = "TestName",
                Description = "Testdescription",
                GameId = "20"
            };
            bool result = await this.achievementService.CreateAchievement(achievementServiceModel);

            Assert.True(result);
            Assert.NotNull(achievementServiceModel.Id);
        }

        #endregion

        [TearDown]
        public void Dispose()
        {
            this.gameCoDbContext.Dispose();
            this.mappingService = null;
            this.achievementService = null;
        }
    }
}
