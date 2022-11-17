using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using TicTacToeAPI.Data;
using TicTacToeAPI.Model;

namespace TicTacToeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        //Dependency injection for in memory database context
        private readonly TicTacToeAPIDbContext dbContext;
        public GameController(TicTacToeAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// using HttpGet method to find the active games from the database context
        /// </summary>
        /// <returns> List of active games with their game id, status, the number of moves registered for each and the names of the players.</returns>
        [HttpGet]
        public async Task<IActionResult> GetCurrentGames()
        {
            var games = await dbContext.Games.Where(g => g.GameStatus == GameState.ongoing).ToListAsync();
            var activeGames = new List<ActiveGame>();
            foreach (var game in games)
            {
                var activeGame = new ActiveGame()
                {
                    GameId = game.Id.ToString(),
                    PlayerX = game.PlayerX,
                    PlayerO = game.PlayerO,
                    GameStatus = game.GameStatus.ToString(),
                    MoveRegistered = game.MoveRegistered
                };
                activeGames.Add(activeGame);
            }
            return Ok(activeGames);
        }
        /// <summary>
        /// Creates a new game object using model and HttpPost method and stores it in memory. 
        /// Input parameters are the unique name id of player X and player O. 
        /// </summary>
        /// <returns> New game id and players name id.
        /// If both name ids are same, returns bad request 400.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> StartGame(string playerX, string playerO)
        {
            var xPlayer = await dbContext.Players.Where(p => p.Name == playerX).FirstOrDefaultAsync();
            var oPlayer = await dbContext.Players.Where(p => p.Name == playerO).FirstOrDefaultAsync();

            if (playerX == playerO)
                return BadRequest("PlayerX and PlayerO cant be same.");
            if (xPlayer == null)
            {
                var newPlayer = new Player()
                {
                    Name = playerX
                };
                await dbContext.Players.AddAsync(newPlayer);
                await dbContext.SaveChangesAsync();
                xPlayer = await dbContext.Players.Where(p => p.Name == playerX).FirstOrDefaultAsync();
            }
            if (oPlayer == null)
            {
                var newPlayer = new Player()
                {
                    Name = playerO
                };
                await dbContext.Players.AddAsync(newPlayer);
                await dbContext.SaveChangesAsync();
                oPlayer = await dbContext.Players.Where(p => p.Name == playerO).FirstOrDefaultAsync();
            }

            var game = new Game()
            {
                Id = Guid.NewGuid(),
                PlayerX = xPlayer.Name,
                PlayerO = oPlayer.Name,
                GameStatus = GameState.ongoing,
                MoveRegistered = 0
            };
            await dbContext.Games.AddAsync(game);
            await dbContext.SaveChangesAsync();

            //show game id and 2 player name id
            var currentGame = new StartedGame
            {
                GameId = game.Id.ToString(),
                PlayerX = game.PlayerX,
                PlayerO = game.PlayerO,
            };
            return Ok(currentGame);
        }
    }
}
