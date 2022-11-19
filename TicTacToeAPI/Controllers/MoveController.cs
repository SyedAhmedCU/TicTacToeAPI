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
        /// Takes player's name id, game id and move and store it  
        /// </summary>
        /// <param name="newMove"></param>
        /// <returns>If all validates, returns success response or appropriate errors.</returns>
        [HttpPost]
        public async Task<IActionResult> PostNewMove(NewMove newMove)
        {
            var gameExist = await dbContext.Games.Where(g => g.Id.ToString() == newMove.GameID && g.GameStatus == GameState.ongoing).FirstOrDefaultAsync();
            // find a game with the game id 
            if (gameExist == null)
                return BadRequest("No ongoing game was found with the game id. Please use correct game id or start a game.");

            //check if the player is in the game
            var playerX = gameExist.PlayerX;
            var playerO = gameExist.PlayerO;
            var currentPlayer = newMove.PlayerNameId;
            if (currentPlayer != playerX && currentPlayer != playerO)
                return BadRequest("Player is not in this game.");

            //check if the move is available or already registered
            var moveExist = await dbContext.Moves.Where(g => g.GameID.ToString() == newMove.GameID && g.MoveIndex == newMove.MoveIndex).FirstOrDefaultAsync();
            if (moveExist != null)
                return BadRequest("Move already registered. Try another.");

            //check if the move is outside the limit (1-9)
            if (newMove.MoveIndex < (int)MoveConstraint.firstPlace || newMove.MoveIndex > (int)MoveConstraint.lastPlace)
                return BadRequest("Invalid move, choose between 1-9.");

            //If the move passes the validation, increase the move count
            var latestMoveCount = gameExist.MoveRegistered + 1;

            var move = new Move()
            {
                Id = Guid.NewGuid(),
                GameID = newMove.GameID,
                PlayerNameId = newMove.PlayerNameId,
                MoveIndex = newMove.MoveIndex
            };

            //register current move
            await dbContext.Moves.AddAsync(move);
            //update newly registered move in the game 
            gameExist.MoveRegistered = latestMoveCount;
            await dbContext.SaveChangesAsync();

            bool currentPlayerWin = false;
            var checkMoves = await dbContext.Moves.Where(g => g.GameID == move.GameID && g.PlayerNameId == move.PlayerNameId).ToListAsync();
            if (checkMoves.Count() == 3)
                currentPlayerWin = GameLogic(checkMoves);

            if (currentPlayerWin == true)
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
                return Ok("Move is registered successfully!");
            }
        }
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