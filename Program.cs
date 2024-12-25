using MathGame.Services;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
  .SetBasePath(Directory.GetCurrentDirectory())
  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
  .Build();

var gameHistory = new GameHistory();
var maxRange = ConfigurationBinder.GetValue<int?>(configuration, "GameSettings:MaxRange") ?? 30;

var gameEngine = new GameEngine(gameHistory, maxRange);
await gameEngine.RunAsync();