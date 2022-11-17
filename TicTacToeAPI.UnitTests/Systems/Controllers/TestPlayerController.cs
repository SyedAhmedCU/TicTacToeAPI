using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicTacToeAPI.Controllers;
using TicTacToeAPI.Data;
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

            //Act
            var OkObjectResult = (OkObjectResult)await sut.AddPlayer("TestPlayer");
            //Assert
            OkObjectResult.Value.Should().BeOfType<Model.Player>();
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

            //Act
            //adding same player should return a bad request 
            var OkObjectResult = (OkObjectResult)await sut.AddPlayer("SameTestPlayer");
            var BadRequestObjectResult = (BadRequestObjectResult)await sut.AddPlayer("SameTestPlayer");
            //Assert
            OkObjectResult.Value.Should().BeOfType<Model.Player>();
            BadRequestObjectResult.StatusCode.Should().Be(400);
        }
    }
}
