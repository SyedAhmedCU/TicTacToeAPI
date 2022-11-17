using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicTacToeAPI.Controllers;
using TicTacToeAPI.Data;
using Xunit;
using System.Threading.Tasks;

namespace TicTacToeAPI.UnitTests.Systems.Controllers
{
    public class TestGameController
    {
        [Fact]
        public async Task Get_OnSuccess_ReturnStatusCode200()
        {
            //add in memory database context
            var optionsBuilder = new DbContextOptionsBuilder<TicTacToeAPIDbContext>()
                .UseInMemoryDatabase("GameDb");
            var dbContext = new TicTacToeAPIDbContext(optionsBuilder.Options);
            //Arrange
            var sut = new GameController(dbContext);

            //Act
            var result = (OkObjectResult)await sut.GetCurrentGames();

            //Assert
            result.StatusCode.Should().Be(200);
        }
    }
}
