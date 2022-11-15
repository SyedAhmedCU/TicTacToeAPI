using Microsoft.EntityFrameworkCore;

namespace TicTacToeAPI.Data
{
    public class TicTacToeAPIDbContext : DbContext
    {
        public TicTacToeAPIDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
