using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicTacToeAPI.Controllers;
using TicTacToeAPI.Data;
using TicTacToeAPI.Model;
using Xunit;

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
            var gamePlayers = new GamePlayers() { PlayerO = "TestPlayer1", PlayerX = "TestPlayer2" };
            //Act
            var OkObjectResult = (OkObjectResult)await sut.StartGame(gamePlayers);
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
            var gamePlayers = new GamePlayers() { PlayerO = "TestPlayer1", PlayerX = "TestPlayer2" };
            await sut.StartGame(gamePlayers);

            //Act
            var OkObjectResult = (OkObjectResult)await sut.GetCurrentGames();

            //Assert
            OkObjectResult.Value.Should().BeOfType<List<ActiveGame>>();
        }
        [Fact]
        public async Task Post_OnSuccess_ReturnStatusCode400PlayersAreSame()
        {
            //add in memory database context
            var optionsBuilder = new DbContextOptionsBuilder<TicTacToeAPIDbContext>()
                .UseInMemoryDatabase("GameDb");
            var dbContext = new TicTacToeAPIDbContext(optionsBuilder.Options);

            //Arrange
            var sut = new GameController(dbContext);
            var gamePlayers = new GamePlayers() { PlayerO = "SameTestPlayer", PlayerX = "SameTestPlayer" };

            //Act
            //adding same players should return a bad request 
            var BadRequestObjectResult = (BadRequestObjectResult)await sut.StartGame(gamePlayers);

            //Assert
            BadRequestObjectResult.StatusCode.Should().Be(400);
        }
    }
}