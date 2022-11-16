using Microsoft.EntityFrameworkCore;
using TicTacToeAPI.Model;

namespace TicTacToeAPI.Data
{
    public class TicTacToeAPIDbContext : DbContext
    {
        public TicTacToeAPIDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
    }
}
