using GameCo.Data;
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
        private Mock<GameCoDbContext> gameCoDbContext = new Mock<GameCoDbContext>();
        private Mock<IMappingService> mappingService = new Mock<IMappingService>();
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

        [Test]
        public async Task TestIfCanCreateGame()
        {
            GameServiceModel gamesService = new GameServiceModel
            {
                Id = "Wasd",
                Name = "Goshko"
            };
            bool result = await this.gameService.CreateGame(gamesService);

            MappingService mapService = new MappingService();
            mapService.
            //Valid data returns true

            //Exception ex = Assert.ThrowsAsync<ArgumentException>(() => this.gameService.CreateGame(gamesService));

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