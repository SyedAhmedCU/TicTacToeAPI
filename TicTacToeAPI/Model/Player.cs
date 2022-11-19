using System.ComponentModel.DataAnnotations;

namespace TicTacToeAPI.Model
{
    /// <summary>
    /// This class is used as a input for the AddPlayer() method
    /// </summary>
    public class NewPlayer
    {
        [Required]
        public string NameId { get; set; } = string.Empty;
    }
    public class Player : NewPlayer
    {
        public Guid Id { get; set; }
    }
}