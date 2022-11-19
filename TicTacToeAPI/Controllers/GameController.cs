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
        /// Get a list of running games
        /// </summary>
        /// <remarks>
        ///     
        /// Sample response:
        /// 
        ///     [
        ///         {
        ///             "gameStatus": "ongoing",
        ///             "moveRegistered": 0,
        ///             "gameId": "fdb21950-f250-4886-aa20-0ca6b0da6850",
        ///             "playerX": "Pam",
        ///             "playerO": "Jim"
        ///         },
        ///         {
        ///             "gameStatus": "ongoing",
        ///             "moveRegistered": 0,
        ///             "gameId": "161d4d24-da3a-4701-bcd6-5147c8b708fb",
        ///             "playerX": "Michael",
        ///             "playerO": "Toby"
        ///         }
        ///     ]
        /// 
        /// </remarks>
        /// <returns> List of active games with their game id, status, the number of moves registered for each and the names of the players.</returns>
        [HttpGet]
        public async Task<IActionResult> GetCurrentGames()
        {
            var games = await dbContext.Games.Where(g => g.GameStatus == GameState.ongoing).ToListAsync();
            List<ActiveGame> activeGames = new();
            foreach (var game in games)
            {
                ActiveGame activeGame = new()
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
        /// Start a new game 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Game
        ///     {
        ///         "playerX": "Ryan",
        ///         "playerO": "Kelly"
        ///     }
        ///
        /// Sample response:
        ///
        ///     {
        ///         "gameId": "de905ed5-8742-437d-9e1a-f7aa7c4ad01d",
        ///         "playerX": "Ryan",
        ///         "playerO": "Kelly"
        ///     }
        ///
        /// </remarks>
        /// <returns> New gameId and players nameId.
        /// If both nameIds are same, returns bad request status code 400.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> StartGame(GamePlayers gamePlayers)
        {
            var xPlayer = await dbContext.Players.Where(p => p.NameId.ToLower() == gamePlayers.PlayerX.ToLower()).FirstOrDefaultAsync();
            var oPlayer = await dbContext.Players.Where(p => p.NameId.ToLower() == gamePlayers.PlayerO.ToLower()).FirstOrDefaultAsync();

            if (gamePlayers.PlayerX == gamePlayers.PlayerO)
                return BadRequest("PlayerX and PlayerO can't be same.");
            //add PlayerX and PlayerO if they don't exist database
            if (xPlayer == null)
            {
                Player newPlayer = new()
                {
                    NameId = gamePlayers.PlayerX
                };
                await dbContext.Players.AddAsync(newPlayer);
                await dbContext.SaveChangesAsync();
                xPlayer = await dbContext.Players.Where(p => p.NameId.ToLower() == gamePlayers.PlayerX.ToLower()).FirstOrDefaultAsync();
            }
            if (oPlayer == null)
            {
                Player newPlayer = new()
                {
                    NameId = gamePlayers.PlayerO
                };
                await dbContext.Players.AddAsync(newPlayer);
                await dbContext.SaveChangesAsync();
                oPlayer = await dbContext.Players.Where(p => p.NameId.ToLower() == gamePlayers.PlayerO.ToLower()).FirstOrDefaultAsync();
            }

            Game game = new()
            {
                Id = Guid.NewGuid(),
                PlayerX = xPlayer.NameId,
                PlayerO = oPlayer.NameId,
                GameStatus = GameState.ongoing,
                MoveRegistered = 0
            };
            await dbContext.Games.AddAsync(game);
            await dbContext.SaveChangesAsync();

            //show game id and 2 player name id
            StartedGame startedGame = new()
            {
                GameId = game.Id.ToString(),
                PlayerX = game.PlayerX,
                PlayerO = game.PlayerO,
            };
            return Ok(startedGame);
        }
    }
}