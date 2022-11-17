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
        /// Returns list of players from the database context using HttpGet method
        /// </summary>
        /// <returns> List of Players with their id and names</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllPlayers()
        {
            var players = await dbContext.Players.ToListAsync();
            return Ok(players);
        }
        /// <summary>
        /// Creates a player object similar to Player model using HttpPost method. 
        /// Input parameter is the name.
        /// </summary>
        /// <returns> Newly added player object with id and name.
        /// If the player already exist, returns bad request with a string message.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> AddPlayer(NewPlayer newPlayer)
        {
            var registerPlayer = new Player()
            {
                Name = newPlayer.Name,
            };
            var nameExist = await dbContext.Players.Where(p => p.Name.ToLower() == registerPlayer.Name.ToLower()).FirstOrDefaultAsync();
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