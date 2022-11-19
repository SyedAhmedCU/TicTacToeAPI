namespace TicTacToeAPI.Model
{
    public class NewMove
    {
        public string PlayerNameId { get; set; } = string.Empty;
        public string GameID { get; set; } = string.Empty;
        public int MoveIndex { get; set; }
    }
    //For creating dbset 
    public class Move : NewMove
    {
        public Guid Id { get; set; }
    }
}