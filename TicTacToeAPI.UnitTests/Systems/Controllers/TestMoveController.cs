using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using System;
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
            var newGame = new Game { Id = Guid.NewGuid(), PlayerX = "TestPlayer1", PlayerO = "TestPlayer2", GameStatus = GameState.ongoing, MoveRegistered = 0 };
            await dbContext.Games.AddAsync(newGame);
            await dbContext.SaveChangesAsync();

            var newMove = new NewMove() { GameID = newGame.Id.ToString(), MoveIndex = 0, PlayerNameId = "TestPlayer1" };

            //Act
            var result2 = await sut.PostNewMove(newMove);
            var result = (OkObjectResult)result2;

            //Assert
            result.StatusCode.Should().Be(200);
        }
        [Fact]
        public async Task Post_ExceptionalCase1_ReturnStatusCode400()
        {
            //add in memory database context
            var optionsBuilder = new DbContextOptionsBuilder<TicTacToeAPIDbContext>()
                .UseInMemoryDatabase("GameDb");
            var dbContext = new TicTacToeAPIDbContext(optionsBuilder.Options);
            //Arrange
            var sut = new MoveController(dbContext);
            //add a new game
            var newGame = new Game { Id = Guid.NewGuid(), PlayerX = "TestPlayer1", PlayerO = "TestPlayer2", GameStatus = GameState.ongoing, MoveRegistered = 0 };
            await dbContext.Games.AddAsync(newGame);
            await dbContext.SaveChangesAsync();

            //Invalid game id
            var newMove = new NewMove() { GameID = "InvalidGameID", MoveIndex = 0, PlayerNameId = "TestPlayer1" };

            //Act
            var badRequestObjectResult = (BadRequestObjectResult)await sut.PostNewMove(newMove);

            //Assert
            badRequestObjectResult.StatusCode.Should().Be(400);
        }
        [Fact]
        public async Task Post_ExceptionalCase2_ReturnStatusCode400()
        {
            //add in memory database context
            var optionsBuilder = new DbContextOptionsBuilder<TicTacToeAPIDbContext>()
                .UseInMemoryDatabase("GameDb");
            var dbContext = new TicTacToeAPIDbContext(optionsBuilder.Options);
            //Arrange
            var sut = new MoveController(dbContext);
            //add a new game
            var newGame = new Game { Id = Guid.NewGuid(), PlayerX = "TestPlayer1", PlayerO = "TestPlayer2", GameStatus = GameState.draw, MoveRegistered = 0 };
            await dbContext.Games.AddAsync(newGame);
            await dbContext.SaveChangesAsync();

            //game already finished
            var newMove = new NewMove() { GameID = newGame.Id.ToString(), MoveIndex = 0, PlayerNameId = "TestPlayer1" };

            //Act
            var badRequestObjectResult = (BadRequestObjectResult)await sut.PostNewMove(newMove);

            //Assert
            badRequestObjectResult.StatusCode.Should().Be(400);
        }
    }
}
