# TicTacToe
[![CI](https://github.com/SyedAhmedCU/TicTacToeAPI/actions/workflows/ci.yaml/badge.svg?kill_cache=1)](https://github.com/SyedAhmedCU/TicTacToeAPI/actions/workflows/ci.yaml)
[![Coverage Status](https://coveralls.io/repos/github/SyedAhmedCU/TicTacToeAPI/badge.svg?branch=2-endpoint-for-starting-a-game&kill_cache=1)](https://coveralls.io/github/SyedAhmedCU/TicTacToeAPI?branch=2-endpoint-for-starting-a-game)

Creating a .NET 6.0 Web API that provides endpoints for managing games of Tic-Tac-Toe. These endpoints are to take the described inputs as JSON strings and return the described output. The API application needs to be runnable using Docker and Docker Compose. Use the most appropriate verbs for the endpoints. Game data should be managed using Entity Framework and a database of your choice (an in-memory database is fine).
## Endpoint 1
Add an endpoint for starting a game. This endpoint should return a game Id and Ids for the two players.
## Endpoint 2
Add an endpoint for registering a player move. This endpoint should take the player Id and return a success response or appropriate errors. It should also notify the caller if the current move wins the game.
## Endpoint 3
Add an endpoint for retrieving a list of currently running games including the number of moves registered for each and the names of the players.
