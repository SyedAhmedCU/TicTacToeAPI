namespace TicTacToeAPI.Model
{
    /// <summary>
    /// The game has a 3x3 grid with 9 possible moves. Each move has an index number from 1-9. 
    /// These are arranged according to the magic square. So, sum of 3 numbers joining horizontal, vertical, or main diagonal line 
    /// is the magic constant which is 15 for this case. 
    /// A player will win when the sum of his moves is equal to 15.
    /// </summary>
    public enum MoveConstraint { magicConstant = 15, maxMoves = 9 }
    /// <summary>
    /// GameState enum : ongoing = 0, win = 1, draw=2
    /// </summary>
    public enum GameState { ongoing, win, draw }
}