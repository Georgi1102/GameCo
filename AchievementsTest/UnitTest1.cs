using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AchievementsTest
{
   
    public class UnitTest1
    {
        private GameCoDbContext gameCoDbContext;
        private IMappingService mappingService;
        private IAchievementService serviceService;

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
    }
}
