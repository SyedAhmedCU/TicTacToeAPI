namespace TicTacToeAPI.Model
{
    public class Game
    {
        public Guid Id { get; set; }
        public string PlayerX { get; set; } = string.Empty;
        public string PlayerO { get; set; } = string.Empty;
        public GameState GameStatus { get; set; } = GameState.ongoing;
        public int? MoveRegistered { get; set; } = 0;
    }
    public enum GameState { ongoing, playerXwin, playerOwin, draw }

    //class for showing newly started game
    public class StartedGame 
    {
       public string GameId { get; set; } = string.Empty;
       public string PlayerX { get; set; } = string.Empty;
       public string PlayerO { get; set; } = string.Empty;
    }
    //class for showing list of running games
    public class ActiveGame
    {
        public string GameId { get; set; } = string.Empty;
        public string PlayerX { get; set; } = string.Empty;
        public string PlayerO { get; set; } = string.Empty;
        public string GameStatus { get; set; } = string.Empty;
        public int? MoveRegistered { get; set; } = 0;
    }

}
