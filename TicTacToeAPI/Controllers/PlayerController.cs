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
        private readonly TicTacToeAPIDbContext dbContext;
        public PlayerController(TicTacToeAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPlayers()
        {
            var players = await dbContext.Players.ToListAsync();
            return Ok(players);
        }
        [HttpPost]
        public async Task<IActionResult> AddPlayer(string name)
        {
            var newPlayer = new Player()
            {
                Name = name
            };
            var nameExist = await dbContext.Players.Where(p => p.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
            if (nameExist != null)
            {
                return BadRequest("Player already exist, try a different name.");
            }
            await dbContext.Players.AddAsync(newPlayer);
            await dbContext.SaveChangesAsync();
            return Ok(newPlayer);
        }
    }
}
