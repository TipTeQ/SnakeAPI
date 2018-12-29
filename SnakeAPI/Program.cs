using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SnakeAPI.Agents;
using System.Collections.Generic;
using System.Threading;

namespace SnakeAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //BuildWebHost(args).Run();

            var snake = new Snake
            {
                Parts = new List<Coordinate>
                {
                    new Coordinate(6, 2),
                    new Coordinate(9, 2)
                }
            };
            var agent = new SnakeAgent(snake, "Player1");
            var snakeGame = new SnakeGame(32, 32, 4);
            snakeGame.InitialiseNewGame(new List<ISnakeAgent> { agent });

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://*:6666")
                .ConfigureServices(services => {
                    services.AddSingleton(snakeGame);
                })
                .UseStartup<Startup>()
                .Start();

            using (host)
            {
                while (true)
                {
                    snakeGame.Iterate();
                    snakeGame.PrepareNextIteration();
                    Thread.Sleep(50);
                }
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
