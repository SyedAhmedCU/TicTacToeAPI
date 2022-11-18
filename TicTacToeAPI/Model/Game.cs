namespace TicTacToeAPI.Model
{
    public class GamePlayers
    {
        public string PlayerX { get; set; } = string.Empty;
        public string PlayerO { get; set; } = string.Empty;
    }
    /// <summary>
    /// This class is to store game information in database
    /// </summary>
    public class Game : GamePlayers
    {
        public Guid Id { get; set; }
        public GameState GameStatus { get; set; } = GameState.ongoing;
        public int? MoveRegistered { get; set; } = 0;
    }
    /// <summary>
    /// GameState enum : ongoing = 0, win = 1, draw=2
    /// </summary>
    public enum GameState { ongoing, win, draw }

      /// <summary>
    /// Class for showing newly started game with player name and game id
    /// </summary>
    public class StartedGame  : GamePlayers
    {
       public string GameId { get; set; } = string.Empty;
    }
    /// <summary>
    /// Class for showing list of running games includes game id, status, players and mode registerd for each
    /// </summary>
    public class ActiveGame : StartedGame
    {
        public string GameStatus { get; set; } = string.Empty;
        public int? MoveRegistered { get; set; } = 0;
    }
}
