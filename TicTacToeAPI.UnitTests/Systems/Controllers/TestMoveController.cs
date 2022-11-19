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

            var newMove = new NewMove() { GameId = newGame.Id.ToString(), MoveIndex = 5, PlayerNameId = "TestPlayer1" };

            //Act
            var okObjectResult = (OkObjectResult)await sut.PostNewMove(newMove);

            //Assert
            okObjectResult.StatusCode.Should().Be(200);
            var expectedMessage = Assert.IsType<string>(okObjectResult.Value);
            Assert.Equal(newMove.PlayerNameId + "'s move is registered successfully!", expectedMessage);
        }
        [Fact]
        public async Task Post_InvalidGameID_ReturnStatusCode400WithErrorMsg()
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
            var newMove = new NewMove() { GameId = "InvalidGameID", MoveIndex = 0, PlayerNameId = "TestPlayer1" };

            //Act
            var badRequestObjectResult = (BadRequestObjectResult)await sut.PostNewMove(newMove);

            //Assert
            badRequestObjectResult.StatusCode.Should().Be(400);
            //test message
            var expectedMessage = Assert.IsType<string>(badRequestObjectResult.Value);
            Assert.Equal("No ongoing game was found with the game id. Please use correct game id or start a game.", expectedMessage);
        }
        [Fact]
        public async Task Post_GameFinsihed_ReturnStatusCode400WithErrorMsg()
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
            var newMove = new NewMove() { GameId = newGame.Id.ToString(), MoveIndex = 0, PlayerNameId = "TestPlayer1" };

            //Act
            var badRequestObjectResult = (BadRequestObjectResult)await sut.PostNewMove(newMove);

            //Assert
            badRequestObjectResult.StatusCode.Should().Be(400);
            //test message
            var expectedMessage = Assert.IsType<string>(badRequestObjectResult.Value);
            Assert.Equal("No ongoing game was found with the game id. Please use correct game id or start a game.", expectedMessage);
        }
        [Fact]
        public async Task Post_InvalidPlayerNameID_ReturnStatusCode400WithErrorMsg()
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

            //player is not register in the game 
            var newMove = new NewMove() { GameId = newGame.Id.ToString(), MoveIndex = 0, PlayerNameId = "InvalidPlayerID" };

            //Act
            var badRequestObjectResult = (BadRequestObjectResult)await sut.PostNewMove(newMove);

            //Assert
            badRequestObjectResult.StatusCode.Should().Be(400);
            //test message
            var expectedMessage = Assert.IsType<string>(badRequestObjectResult.Value);
            Assert.Equal("Player is not in this game.", expectedMessage);
        }
        [Fact]
        public async Task Post_MoveAlreadyRegisterd_ReturnStatusCode400WithErrorMsg()
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

            //select move index 4
            var oldMove = new NewMove() { GameId = newGame.Id.ToString(), MoveIndex = 4, PlayerNameId = "TestPlayer1" };
            //select move index 4 again
            var newMove = new NewMove() { GameId = newGame.Id.ToString(), MoveIndex = 4, PlayerNameId = "TestPlayer2" };

            //Act
            var okObjectResult = (OkObjectResult)await sut.PostNewMove(oldMove);
            var badRequestObjectResult = (BadRequestObjectResult)await sut.PostNewMove(newMove);

            //Assert
            //old move is registered successfully
            okObjectResult.StatusCode.Should().Be(200);
            //new move is not registered
            badRequestObjectResult.StatusCode.Should().Be(400);
            //test message
            var expectedMessage = Assert.IsType<string>(badRequestObjectResult.Value);
            Assert.Equal("Move already registered. Try another.", expectedMessage);
        }
    }
}
