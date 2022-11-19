# TicTacToe
[![CI](https://github.com/SyedAhmedCU/TicTacToeAPI/actions/workflows/ci.yaml/badge.svg?&service=github)](https://github.com/SyedAhmedCU/TicTacToeAPI/actions/workflows/ci.yaml)
[![Coverage Status](https://coveralls.io/repos/github/SyedAhmedCU/TicTacToeAPI/badge.svg?branch=main&kill_cache=1)](https://coveralls.io/github/SyedAhmedCU/TicTacToeAPI?branch=main)

Creating a .NET 6.0 Web API that provides endpoints for managing games of Tic-Tac-Toe. These endpoints are to take the described inputs as JSON and return the described output. The API application is runnable using Docker and Docker Compose. Game data is managed using Entity Framework and in-memory database.

## Overview

|API | Description | Request body | Response body |
|--- | ---- | ---- | ---- |
|`GET /api/Game` | Get all running games | None | JSON array of ActiveGame blocks|
|`POST /api/Game` | Start a new game | GamePlayer JSON | A StartedGame JSON block|
|`GET /api/Move` | Get all moves | None | JSON array of Move blocks|
|`POST /api/Move` | Register a move | NewMove JSON | Success message |
|`GET /api/Player` | Get all players | None | JSON array of Player blocks |
|`POST /api/Player` | Add a new player | NewPlayer JSON | A Player JSON block |

### Endpoint 1 `POST /api/Game`
This endpoint is for starting a game. This endpoint returns a new game Id (Guid) and name Ids for the two players.
- Sample request: GamePlayer JSON
```JSON
{
    "playerX": "Ryan",
    "playerO": "Kelly"
}
```
- Sample response: StartedGame JSON block
```Json
{
    "gameId": "de905ed5-8742-437d-9e1a-f7aa7c4ad01d",
    "playerX": "Ryan",
    "playerO": "Kelly"
}
```
### Endpoint 2 `POST /api/Move`
This endpoint is for registering a player move. This endpoint takes the player Id, game id, and move index (0-9) and return a success response or appropriate errors. It also notifies the caller if the current move wins the game or if it's a draw.
- Sample request: NewMove JSON 
```JSON
{
    "playerNameId": "Jim",
    "gameId": "fdb21950-f250-4886-aa20-0ca6b0da6850",
    "moveIndex": 5
}
```
- Sample response: Success message
```JSON
"Jim's move is registered successfully!"
```
### Endpoint 3 `GET /api/Game`
This endpoint is for retrieving a list of currently running games including the number of moves registered for each and the names of the players.
- Sample response: JSON array of ActiveGame blocks
```JSON
[
    {
        "gameStatus": "ongoing",
        "moveRegistered": 0,
        "gameId": "fdb21950-f250-4886-aa20-0ca6b0da6850",
        "playerX": "Pam",
        "playerO": "Jim"
    },
    {
        "gameStatus": "ongoing",
        "moveRegistered": 0,
        "gameId": "161d4d24-da3a-4701-bcd6-5147c8b708fb",
        "playerX": "Michael",
        "playerO": "Toby"
    }
]
````
### Endpoint 4

### Endpoint 5

### Endpont 6


### In-memory database ERD
<img src="https://user-images.githubusercontent.com/55814513/202872060-03d5fba5-a24e-45f1-b558-2fd63ee8785b.png" alt="ERD of tictactoe Api" width="600px" /> <br>

## Game Logic Using Magic Square
<img src="https://user-images.githubusercontent.com/55814513/202608065-e682080f-6fd9-4b0c-8d29-08d0e2f324e0.png" alt="Magic Square" width="200px" /> <br>
- A magic square is a square array of numbers consisting of the distinct positive integers 1, 2, ..., n^2 arranged such that the sum of the n numbers in any horizontal, vertical, or main diagonal line is always the same number known as the magic constant. Source: <a href="https://mathworld.wolfram.com/MagicSquare.html">Wolfram Mathworld</a> <br>
- The game has a 3x3 grid with 9 possible moves. Each move has an index number from 1-9. These are arranged according to the magic square. So, sum of 3 numbers joining horizontal, vertical, or main diagonal line is the magic constant which is 15 for this case. A player will win when the sum of his moves is equal to 15.

## How To Run

### Steps For Cloning the Project
- Create a folder named "TicTacToe", open terminal adn run the following command
```sh
$ git clone https://github.com/SyedAhmedCU/TicTacToeAPI.git .
```

### Build Docker Image
- Install [Docker](https://www.docker.com/) on desktop
- To build Docker image, open git bash terminal from the root directory where the docker file is located and run the follwoing command
```sh
$ docker build --rm -t syed-ahmed/tic-tac-toe-api:latest .
```
- If build is successful, run the following command to setup and run container
```sh
$ docker run --rm -p 5000:5000 -p 5001:5001 -e ASPNETCORE_HTTP_PORT=https://+:5001 -e ASPNETCORE_URLS=http://+:5000 syed-ahmed/tic-tac-toe-api
```
- Open the following links in browser:
- GET: api/Game (endpoint 3) - List of active games: http://localhost:5000/api/game
- GET: api/Player - List of players: http://localhost:5000/api/player
- To check endpoint 1 and 2 (both use POST method), use Postman

### Testing With Postman
- Install [Postman](https://www.postman.com/) on desktop and login (create a free acount)
- In the desktop app, add a new tab and paste the following links. Select Body > Raw > JSON and use the request body format to send POST request and test the response.  
- POST: api/Game (endpoint 1) - start a new game: http://localhost:5000/api/game <br>
Request body format: ```{ "playerX": "string", "playerO": "string" }```
- POST: api/Move (endpoint 2) - register a player move: http://localhost:5000/api/move <br>
request body format: ```{ "playerNameId": "string", "gameID": "string", "moveIndex": int }```

<img src="https://user-images.githubusercontent.com/55814513/202830331-e9747f7f-ed19-466f-b7d1-5c3a4d84b1c9.png" alt="Start Game API POST Postman" width="45%" /> <img src="https://user-images.githubusercontent.com/55814513/202830404-b53b7b74-b941-43e2-a738-bc2b8162d583.png" alt="Register Player Move API POST Postman" width="49%" />
