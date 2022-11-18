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
    /// 8 1 6 //
    /// 3 5 7 // 
    /// 4 9 2 //
    /// A player will win when the sum of the move index is equal to 15 which is the magic constant
    /// </summary>
    public enum MoveConstraint { firstPlace = 1, lastPlace = 9, magixConstant = 15 }
}
