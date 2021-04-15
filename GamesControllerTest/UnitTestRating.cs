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
    public class UnitTestRating
    {

        private GameCoDbContext gameCoDbContext;
        private IMappingService mappingService;
        private IRatingService ratingService;



        [SetUp]
        public void Setup()
        {
            DbContextOptions<GameCoDbContext> options = new DbContextOptionsBuilder<GameCoDbContext>()
                .UseInMemoryDatabase($"TESTS-DB-{Guid.NewGuid().ToString()}")
                .Options;

            this.gameCoDbContext = new GameCoDbContext(options);
            this.mappingService = new MappingService();
        }

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

        [TearDown]
        public void Dispose()
        {
            this.gameCoDbContext.Dispose();
            this.mappingService = null;
            this.ratingService = null;
        }
    }
}
