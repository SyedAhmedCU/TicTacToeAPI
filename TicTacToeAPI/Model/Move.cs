using System.ComponentModel.DataAnnotations;

namespace TicTacToeAPI.Model
{
    /// <summary>
    /// This class is used as a input for the PostNewMove() method
    /// </summary>
    public class NewMove
    {
        [Required]
        public string PlayerNameId { get; set; } = string.Empty;
        [Required]
        public string GameId { get; set; } = string.Empty;
        [Required]
        public int MoveIndex { get; set; }
    }
    //For creating dbset 
    public class Move : NewMove
    {
        public Guid Id { get; set; }
    }
}