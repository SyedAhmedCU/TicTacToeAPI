# TicTacToe
[![CI](https://github.com/SyedAhmedCU/TicTacToeAPI/actions/workflows/ci.yaml/badge.svg?&service=github)](https://github.com/SyedAhmedCU/TicTacToeAPI/actions/workflows/ci.yaml)
[![Coverage Status](https://coveralls.io/repos/github/SyedAhmedCU/TicTacToeAPI/badge.svg?branch=main)](https://coveralls.io/github/SyedAhmedCU/TicTacToeAPI?branch=main)

Creating a .NET 6.0 Web API that provides endpoints for managing games of Tic-Tac-Toe. These endpoints are to take the described inputs as JSON and return the described output. The API application is runnable using Docker and Docker Compose. Game data is managed using Entity Framework and in-memory database.
### Endpoint 1
Add an endpoint for starting a game. This endpoint should return a game Id and Ids for the two players.
### Endpoint 2
Add an endpoint for registering a player move. This endpoint should take the player Id and return a success response or appropriate errors. It should also notify the caller if the current move wins the game.
### Endpoint 3
Add an endpoint for retrieving a list of currently running games including the number of moves registered for each and the names of the players.

## Game Logic Using Magic Square
<img src="https://user-images.githubusercontent.com/55814513/202608065-e682080f-6fd9-4b0c-8d29-08d0e2f324e0.png" alt="Magic Square" width="200px" /> <br>
- A magic square is a square array of numbers consisting of the distinct positive integers 1, 2, ..., n^2 arranged such that the sum of the n numbers in any horizontal, vertical, or main diagonal line is always the same number known as the magic constant. Source: <a href="https://mathworld.wolfram.com/MagicSquare.html">Wolfram Mathworld</a> <br>
- This can be applied to Tictactoe which has a 3x3 grid, every grid can has a positive integer from 1-9. Using Magic square, a player will win when the sum of the move index is equal to 15.