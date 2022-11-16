namespace TicTacToeAPI.Model
{
    public class Move
    {
        public Guid Id { get; set; }
        public string PlayerId { get; set; } = string.Empty;
        public string GameID { get; set; } = string.Empty;
        public int MoveIndex { get; set; }
    }
}
