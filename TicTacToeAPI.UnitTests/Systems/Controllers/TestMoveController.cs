using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicTacToeAPI.Controllers;
using TicTacToeAPI.Data;
using TicTacToeAPI.Model;
using Xunit;

namespace TicTacToeAPI.UnitTests.Systems.Controllers
{
    public class MovePlayerController
    {
        [Fact]
        public async Task Post_OnSuccess_ReturnStatusCode200()
        {
            //add in memory database context
            var optionsBuilder = new DbContextOptionsBuilder<TicTacToeAPIDbContext>()
                .UseInMemoryDatabase("GameDb");
            var dbContext = new TicTacToeAPIDbContext(optionsBuilder.Options);
            //Arrange
            var sut = new MoveController(dbContext);
            //add a new game
            var newGame = new Game { Id = Guid.NewGuid(), PlayerX = "TestPlayer1", PlayerO = "TestPlayer1", GameStatus = GameState.ongoing, MoveRegistered = 0 };
            await dbContext.Games.AddAsync(newGame);
            await dbContext.SaveChangesAsync();

            var newMove = new NewMove() { GameID = newGame.Id.ToString(), MoveIndex = 0, PlayerNameId = "TestPlayer1" };

            //Act
            var result = (OkObjectResult)await sut.PostNewMove(newMove);

            //Assert
            result.StatusCode.Should().Be(200);
        }
    }
}
