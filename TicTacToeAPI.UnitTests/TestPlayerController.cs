using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicTacToeAPI.Controllers;
using TicTacToeAPI.Data;
using Xunit;
using System.Threading.Tasks;

namespace TicTacToe.UnitTests.Systems.Controllers
{
    public class TestPlayerController
    {
        [Fact]
        public async Task Get_OnSuccess_ReturnStatusCode200()
        {
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
    }
}
