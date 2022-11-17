namespace TicTacToeAPI.Model
{
    public class NewPlayer
    {
        public string Name { get; set; } = string.Empty;
    }
    public class Player : NewPlayer
    {
        public Guid Id { get; set; }
    }
}
