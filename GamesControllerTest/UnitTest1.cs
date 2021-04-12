using GameCo.Data;
using GameCo.Data.Models;
using GameCo.Services;
using GameCo.Services.Models.Games;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GamesControllerTest
{
    public class Tests
    {
        private GameCoDbContext gameCoDbContext;
        private IMappingService mappingService;
        private IGamesService gameService;
        private IRatingService ratingService;
        private IAchievementsService achievementService;


        [SetUp]
        public void Setup()
        {
            DbContextOptions<GameCoDbContext> options = new DbContextOptionsBuilder<GameCoDbContext>()
                .UseInMemoryDatabase($"TESTS-DB-{Guid.NewGuid().ToString()}")
                .Options;

            this.gameCoDbContext = new GameCoDbContext(options);
            this.mappingService = new MappingService();
            this.gameService = new GameService(gameCoDbContext, mappingService);
            this.achievementService = new AchievementService(gameCoDbContext, mappingService);
        }

        #region Game tests
        //should work
        [Test]
        public async Task TestIfCanCreateGameAndCheckIfTheNamesAreEqual_AndIfItCreatesNewEntity()
        {
            GameServiceModel gamesServiceModel = new GameServiceModel
            {
                Id = "1234",
                Name = "CallOfDuty"
            };

            GameCoGames expectedGameEntity = new GameCoGames
            {
                Id = "1234",
                Name = "CallOfDuty"
            };

            bool result = await this.gameService.CreateGame(gamesServiceModel);
            GameCoGames theEnity = await this.gameCoDbContext.Games.FirstOrDefaultAsync();

            Assert.True(result);

            Assert.AreEqual(expectedGameEntity.Name, theEnity.Name);

        }

        //should work
        [Test]
        public async Task TestIfTheIdsAreNull_AndIfItCreatesNewEntity()
        {
            GameServiceModel gamesServiceModel = new GameServiceModel
            {
                Id = "1234",
                Name = "CallOfDuty"
            };

            GameCoGames expectedGameEntity = new GameCoGames
            {
                Id = "1234",
                Name = "CallOfDuty"
            };

            bool result = await this.gameService.CreateGame(gamesServiceModel);
            GameCoGames theEnity = await this.gameCoDbContext.Games.FirstOrDefaultAsync();

            Assert.True(result);

            Assert.NotNull(expectedGameEntity.Id, theEnity.Id);

        }

        //should work
        [Test]
        public async Task TestIfFindingIdIsReturningTheCorrectValue()
        {
           
            GameCoGames ratingGameModel = new GameCoGames
            {
                Id = "27",
                Name = "Test"
            };

            GameCoGames game = this.mappingService.MapOject<GameCoGames>(ratingGameModel);
            await this.gameCoDbContext.AddAsync(game);
            await this.gameCoDbContext.SaveChangesAsync();

            string wantedId = this.gameService.FindGameById(ratingGameModel.Id);
            GameCoGames theEnity = await this.gameCoDbContext.Games.FirstOrDefaultAsync();

            Assert.AreEqual(wantedId, theEnity.Id);

        }
        #endregion

        #region Rating tests
        [Test]
        public async Task TestIfTheUserIdIsTheCorrectOne()
        {

            GameCoRating ratingServiceModel = new GameCoRating
            {
                Id = "27",
                RatingValue = 4,
                UserId = "45",
                GameId = "1-3"
            };
            GameCoRating expectedRatingEntity = new GameCoRating
            {
                Id = "27",
                RatingValue = 4,
                UserId = "45",
                GameId = "1-3"
            };

            Assert.AreEqual(ratingServiceModel.UserId, expectedRatingEntity.UserId);
        }

        #endregion

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
            this.gameService = null;
            this.ratingService = null;
            this.achievementService = null;
        }
    }
}