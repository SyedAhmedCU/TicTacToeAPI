using System;
using Microsoft.AspNetCore.Builder;
using TicTacToeAPI.Data;
using TicTacToeAPI.Model;

namespace TicTacToeAPI.Data
{
    public class DataSeeder
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var gameContext = serviceScope.ServiceProvider.GetService<TicTacToeAPIDbContext>();
                if (!gameContext.Players.Any())
                {
                    var players = new List<Player>()
                    {
                        new Player {
                            Name = "Michael"
                        },
                        new Player {
                            Name = "Toby"
                        },
                        new Player {
                            Name = "Pam"
                        },
                        new Player {
                            Name = "Jim"
                        }
                    };
                    gameContext.Players.AddRange(players);
                    gameContext.SaveChanges();
                }
                if (!gameContext.Games.Any())
                {
                    var game = new List<Game>()
                    {
                        new Game()
                        {
                            Id = Guid.NewGuid(),
                            PlayerX = "Michael",
                            PlayerO = "Toby",
                            GameStatus = GameState.ongoing,
                            MoveRegistered = 0
                        },
                        new Game()
                        {
                            Id = Guid.NewGuid(),
                            PlayerX = "Pam",
                            PlayerO = "Jim",
                            GameStatus = GameState.ongoing,
                            MoveRegistered = 0
                        }
                    };
                    gameContext.Games.AddRange(game);
                    gameContext.SaveChanges();
                }
            }
        }
    }
}