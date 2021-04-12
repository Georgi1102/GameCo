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

        [SetUp]
        public void Setup()
        {
            DbContextOptions<GameCoDbContext> options = new DbContextOptionsBuilder<GameCoDbContext>()
                .UseInMemoryDatabase($"TESTS-DB-{Guid.NewGuid().ToString()}")
                .Options;

            this.gameCoDbContext = new GameCoDbContext(options);
            this.mappingService = new MappingService();
            this.gameService = new GameService(gameCoDbContext, mappingService);
        }

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
           
            GameCoGames expectedGameEntity = new GameCoGames
            {
                Id = "27",
                Name = "Test"
            };

            GameCoGames game = this.mappingService.MapOject<GameCoGames>(expectedGameEntity);
            await this.gameCoDbContext.AddAsync(game);
            await this.gameCoDbContext.SaveChangesAsync();

            string wantedId = this.gameService.FindGameById(expectedGameEntity.Id);
            GameCoGames theEnity = await this.gameCoDbContext.Games.FirstOrDefaultAsync();

            Assert.AreEqual(wantedId, theEnity.Id);

        }


        [TearDown]
        public void Dispose()
        {
            this.gameCoDbContext.Dispose();
            this.mappingService = null;
            this.gameService = null;
        }
    }
}