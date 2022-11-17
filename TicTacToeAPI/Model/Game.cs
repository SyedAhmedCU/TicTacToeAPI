namespace TicTacToeAPI.Model
{
    public class Game
    {
        public Guid Id { get; set; }
        public string PlayerX { get; set; } = string.Empty;
        public string PlayerO { get; set; } = string.Empty;
        public GameState GameStateId { get; set; } = GameState.ongoing;
        public int? MoveRegistered { get; set; } = 0;
    }
    public enum GameState { ongoing, playerXwin, playerOwin, draw }
}
