using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicTacToeAPI.Data;
using TicTacToeAPI.Model;

namespace TicTacToeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoveController : ControllerBase
    {
        //Dependency injection for in memory database context
        private readonly TicTacToeAPIDbContext dbContext;
        public MoveController(TicTacToeAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Register a new move for a player in a game  
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Move
        ///     {
        ///         "playerNameId": "Jim",
        ///         "gameId": "fdb21950-f250-4886-aa20-0ca6b0da6850", //replaced with new game id 
        ///         "moveIndex": 5
        ///     }
        ///
        /// Sample response:
        ///
        ///     Jim's move is registered successfully!
        ///
        /// </remarks>
        /// <param name="newMove"></param>
        /// <returns>If all validates, returns success response or appropriate errors.</returns>
        [HttpPost]
        public async Task<IActionResult> PostNewMove(NewMove newMove)
        {
            var gameExist = await dbContext.Games.Where(g => g.Id.ToString() == newMove.GameId && g.GameStatus == GameState.ongoing).FirstOrDefaultAsync();
            // find a game with the game id 
            if (gameExist == null)
                return BadRequest("No ongoing game was found with the game id. Please use correct game id or start a game.");

            //check if the player is in the game
            string playerX = gameExist.PlayerX;
            string playerO = gameExist.PlayerO;
            string currentPlayer = newMove.PlayerNameId;
            if (currentPlayer != playerX && currentPlayer != playerO)
                return BadRequest("Player is not in this game.");

            //check if the move is available or already registered
            var moveExist = await dbContext.Moves.Where(g => g.GameId.ToString() == newMove.GameId && g.MoveIndex == newMove.MoveIndex).FirstOrDefaultAsync();
            if (moveExist != null)
                return BadRequest("Move already registered. Try another.");

            //If the move passes the validation, increase the move count
            int? latestMoveCount = gameExist.MoveRegistered + 1;

            var move = new Move()
            {
                Id = Guid.NewGuid(),
                GameId = newMove.GameId,
                PlayerNameId = newMove.PlayerNameId,
                MoveIndex = newMove.MoveIndex
            };

            //register current move
            await dbContext.Moves.AddAsync(move);
            //update newly registered move in the game 
            gameExist.MoveRegistered = latestMoveCount;
            await dbContext.SaveChangesAsync();

            bool currentPlayerWin = false;
            var checkMoves = await dbContext.Moves.Where(g => g.GameId == move.GameId && g.PlayerNameId == move.PlayerNameId).ToListAsync();
            if (checkMoves.Count() == 3)
                currentPlayerWin = GameLogic(checkMoves);

            if (currentPlayerWin)
            {
                gameExist.GameStatus = GameState.win;
                await dbContext.SaveChangesAsync();
                return Ok(currentPlayer + " wins the game!");
            }
            else if (latestMoveCount >= (int)MoveConstraint.maxMoves || checkMoves.Count() > 3)
            {
                gameExist.GameStatus = GameState.draw;
                await dbContext.SaveChangesAsync();
                return Ok("Game ended as a draw!");
            }
            else
            {
                return Ok(currentPlayer + "'s move is registered successfully!");
            }
        }
        /// <summary>
        /// The game has a 3x3 grid with 9 possible moves. Each move has an index number from 1-9. 
        /// These are arranged according to the magic square. So, sum of 3 numbers joining horizontal, vertical, or main diagonal line 
        /// is the magic constant which is 15 for this case. 
        /// A player will win when the sum of his moves is equal to 15.
        /// </summary>
        /// <param name="allMoves"></param>
        /// <returns>True if the player win or false.</returns>
        private static bool GameLogic(List<Move> allMoves)
        {
            int sum = 0;
            foreach (var move in allMoves)
            {
                sum += move.MoveIndex; 
            }
            if (sum == (int)MoveConstraint.magicConstant)
                return true;

            return false;
        }
    }
}