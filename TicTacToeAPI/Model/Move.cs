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
    /// Tictactoe has total 9 places where player can move, 1 ,2, 3,4 ,5,6,7, 8,9
    /// Using magic square, its is easier to calculate the win condition \\
    /// 2 7 6 //
    /// 9 5 1 // 
    /// 4 3 8 //
    /// A player will win when the sum of the move index is equal to 15
    /// </summary>
    public enum MoveConstraint { firstPlace = 1, lastPlace = 9 }
}
