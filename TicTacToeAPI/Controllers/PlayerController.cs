using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicTacToeAPI.Data;

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
    }
}
