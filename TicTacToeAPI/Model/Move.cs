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
    /// <summary>
    /// Tictactoe has total 9 places where player can move, 0, 1 ,2, 3,4 ,5,6,7, 8,9
    /// </summary>
    public enum MoveConstraint { firstPlace = 0, lastPlace = 8 }
}
