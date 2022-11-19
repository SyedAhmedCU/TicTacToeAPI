using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicTacToeAPI.Data;
using TicTacToeAPI.Model;

namespace TicTacToeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        //Dependency injection for in memory database context
        private readonly TicTacToeAPIDbContext dbContext;
        public PlayerController(TicTacToeAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Get list a of players
        /// </summary>
        /// /// <remarks>
        ///     
        /// Sample response:
        /// 
        ///     [
        ///         {
        ///             "id": "c19233b7-a52a-494c-9b2e-a9d28d1dfaabb",
        ///             "nameId": "Michael"
        ///         },
        ///         {
        ///             "id": "14aecbd3-6206-4862-be08-1b22ceabe4a5",
        ///             "nameId": "Toby"
        ///         }
        ///     ]
        /// 
        /// </remarks>
        /// <returns> List of players with their ids and nameIds.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllPlayers()
        {
            var players = await dbContext.Players.ToListAsync();
            return Ok(players);
        }
        /// <summary>
        /// Add a player 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Game
        ///     {
        ///         "nameId": "Oscar"
        ///     }
        ///
        /// Sample response:
        ///
        ///     {
        ///         "id": "e71b0cdc-d0f4-4123-8fa9-bd05b7614207",
        ///         "nameId": "Oscar"
        ///     }
        ///
        /// </remarks>
        /// <returns> Newly added player object with id and name.
        /// If the player already exist, returns bad request with a string message.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> AddPlayer(NewPlayer newPlayer)
        {
            Player registerPlayer = new()
            {
                Id = Guid.NewGuid(),
                NameId = newPlayer.NameId,
            };
            var nameExist = await dbContext.Players.Where(p => p.NameId.ToLower() == registerPlayer.NameId.ToLower()).FirstOrDefaultAsync();
            if (nameExist != null)
            {
                return BadRequest("Player already exist, try a different name.");
            }
            await dbContext.Players.AddAsync(registerPlayer);
            await dbContext.SaveChangesAsync();
            return Ok(registerPlayer);
        }
    }
}