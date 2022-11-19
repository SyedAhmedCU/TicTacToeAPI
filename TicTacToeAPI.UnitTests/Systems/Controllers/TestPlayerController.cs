using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicTacToeAPI.Controllers;
using TicTacToeAPI.Data;
using TicTacToeAPI.Model;
using Xunit;

namespace TicTacToeAPI.UnitTests.Systems.Controllers
{
    public class TestPlayerController
    {
        [Fact]
        public async Task Get_OnSuccess_ReturnStatusCode200()
        {
            //add in memory database context
            var optionsBuilder = new DbContextOptionsBuilder<TicTacToeAPIDbContext>()
                .UseInMemoryDatabase("GameDb");
            var dbContext = new TicTacToeAPIDbContext(optionsBuilder.Options);
            //Arrange
            var sut = new PlayerController(dbContext);

            //Act
            var result = (OkObjectResult)await sut.GetAllPlayers();

            //Assert
            result.StatusCode.Should().Be(200);
        }
        [Fact]
        public async Task Post_OnSuccess_ReturnNewPlayerModel()
        {
            //add in memory database context
            var optionsBuilder = new DbContextOptionsBuilder<TicTacToeAPIDbContext>()
                .UseInMemoryDatabase("GameDb");
            var dbContext = new TicTacToeAPIDbContext(optionsBuilder.Options);
            //Arrange
            var sut = new PlayerController(dbContext);
            var testPlayer = new NewPlayer() { NameId = "TestPlayer" };
            //Act
            var OkObjectResult = (OkObjectResult)await sut.AddPlayer(testPlayer);
            //Assert
            OkObjectResult.Value.Should().BeOfType<Player>();
        }
        [Fact]
        public async Task Post_OnSuccess_ReturnStatusCode400PlayerExist()
        {
            //add in memory database context
            var optionsBuilder = new DbContextOptionsBuilder<TicTacToeAPIDbContext>()
                .UseInMemoryDatabase("GameDb");
            var dbContext = new TicTacToeAPIDbContext(optionsBuilder.Options);
            //Arrange
            var sut = new PlayerController(dbContext);
            var testPlayer1 = new NewPlayer() { NameId = "SameTestPlayer" };
            var testPlayer2 = new NewPlayer() { NameId = "SameTestPlayer" };
            //Act
            //adding same player should return a bad request 
            var OkObjectResult = (OkObjectResult)await sut.AddPlayer(testPlayer1);
            var BadRequestObjectResult = (BadRequestObjectResult)await sut.AddPlayer(testPlayer2);
            //Assert
            OkObjectResult.Value.Should().BeOfType<Player>();
            BadRequestObjectResult.StatusCode.Should().Be(400);
        }
    }
}
