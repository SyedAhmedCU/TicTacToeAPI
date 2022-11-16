namespace TicTacToeAPI.Model
{
    public class Move
    {
        public Guid Id { get; set; }
        public int PlayerId { get; set; }
        public string GameID { get; set; } = string.Empty;
        public int MoveIndex { get; set; }
    }
}
