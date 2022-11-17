using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicTacToeAPI.Controllers;
using TicTacToeAPI.Data;
using Xunit;
using System.Threading.Tasks;
using TicTacToeAPI.Model;

namespace TicTacToeAPI.UnitTests.Systems.Controllers
{
    public class TestGameController
    {
        [Fact]
        public async Task Get_OnSuccess_ReturnStatusCode200()
        {
            //Dependency injection for in memory database context
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
        [Fact]
        public async Task Post_OnSuccess_ReturnNewGameModel()
        {
            //Dependency injection for in memory database context
            var optionsBuilder = new DbContextOptionsBuilder<TicTacToeAPIDbContext>()
                .UseInMemoryDatabase("GameDb");
            var dbContext = new TicTacToeAPIDbContext(optionsBuilder.Options);
            //Arrange
            var sut = new GameController(dbContext);

            //Act
            var OkObjectResult = (OkObjectResult)await sut.StartGame("TestPlayer1", "TestPlayer2");
            //Assert
            OkObjectResult.Value.Should().BeOfType<StartedGame>();
        }
        [Fact]
        public async Task Get_OnSuccess_GetListOfCurrentGames()
        {
            //Dependency injection for in memory database context
            var optionsBuilder = new DbContextOptionsBuilder<TicTacToeAPIDbContext>()
                .UseInMemoryDatabase("GameDb");
            var dbContext = new TicTacToeAPIDbContext(optionsBuilder.Options);
            //Arrange
            var sut = new GameController(dbContext);
            //start a game
            await sut.StartGame("TestPlayer1", "TestPlayer2");
            //Act
            var OkObjectResult = (OkObjectResult)await sut.GetCurrentGames();
            //Assert
            OkObjectResult.Value.Should().BeOfType<List<ActiveGame>>();
        }
    }
}
