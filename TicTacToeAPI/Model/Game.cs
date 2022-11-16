namespace TicTacToeAPI.Model
{
    public class Game
    {
        public Guid Id { get; set; }
        public string? PlayerXId { get; set; } = string.Empty;
        public string? PlayerOId { get; set; } = string.Empty;
        public int GameStateId { get; set; } = 0;
        public int? MoveRegistered { get; set; } = 0;
    }
}
