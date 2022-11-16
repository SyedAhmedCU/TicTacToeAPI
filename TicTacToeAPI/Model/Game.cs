namespace TicTacToeAPI.Model
{
    public class Game
    {
        public Guid Id { get; set; }
        public int PlayerXId { get; set; }
        public int PlayerOId { get; set; }
        public int GameStateId { get; set; } = 0;
        public int? MoveRegistered { get; set; } = 0;
    }
}
