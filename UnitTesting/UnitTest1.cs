using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace UnitTesting
{
    public class GamesControllerTesting
    {
        private readonly MappingService _test;
        private readonly Mock<IMappingSevice> MappingSeviceMock = new Mock<IMappingSevice>();
        [Fact]
        public async Task CreateGame_IfNotExisting()
        {
            //Arrange

            //Act

            //Assert
        }
    }
}
