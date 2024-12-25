namespace MathGame.Services;

using System.Diagnostics;
using MathGame.Models;
using MathGame.Operations;

public class GameEngine
{
  private readonly IGameHistory _gameHistory;
  private readonly GameState _gameState;
  private readonly Dictionary<int, IMathOperation> _operations;
  private readonly int _maxRange;

  public GameEngine(IGameHistory gameHistory, int maxRange = 30)
  {
    _gameHistory = gameHistory;
    _gameState = new GameState();
    _maxRange = maxRange;

    _operations = new Dictionary<int, IMathOperation> {
      {1, new AdditionOperation() },
      {2, new SubtractionOperation() },
      {3, new MultiplicationOperation() },
      {4, new DivisionOperation() }
    };
  }

  public async Task RunAsync()
  {
    var start = true;
    while (!_gameState.IsGameOver)
    {
      ShowMenu(showScore: !start);

      var selection = Utility.GetOptionWithRange(1, 8);
      await ProcessMenuSelection(selection);
      start = false;
    }
  }

  private void ShowMenu(bool showScore = false)
  {
    if (showScore) ColorConsole.WriteHighlight($"\nCurrent score: [ {_gameState.Score} ]");
    Console.WriteLine($"Choose from the options: ");
    Console.WriteLine("[1] Addition");
    Console.WriteLine("[2] Subtraction");
    Console.WriteLine("[3] Multiplication");
    Console.WriteLine("[4] Division");
    Console.WriteLine("[5] Random mode");
    Console.WriteLine("[6] Show history");
    Console.WriteLine("[7] Change difficulty");
    Console.WriteLine("[8] Exit");
    Console.Write("> ");
  }

  private async Task ProcessMenuSelection(int selection)
  {
    switch (selection)
    {
      case >= 1 and <= 4:
        await PerformOperation(_operations[selection]);
        break;
      case 5:
        await RunRandomMode();
        break;
      case 6:
        ShowHistory();
        break;
      case 7:
        ChangeDifficulty();
        break;
      case 8:
        EndGame();
        break;
    }
  }

  private async Task PerformOperation(IMathOperation operation)
  {
    try
    {
      var (firstNum, secondNum) = operation.GeneratedNumbers(_gameState.CurrentDifficulty, _maxRange);
      var correctAnswer = operation.Calculate(firstNum, secondNum);

      DisplayQuestion(firstNum, secondNum, operation.Symbol);

      var (userAnswer, responseTime) = await GetUserAnswer();

      if (!userAnswer.HasValue)
      {
        ColorConsole.WriteError("\nTime's up!");
        return;
      }

      var isCorrect = userAnswer == correctAnswer;
      var scoreEarned = 0;

      if (isCorrect)
      {
        var baseScore = _gameState.CurrentDifficulty switch
        {
          DifficultyLevel.Easy => 5,
          DifficultyLevel.Medium => 10,
          DifficultyLevel.Hard => 20,
          _ => 5
        };


        var bonusScore = CalculateBonusScore(responseTime);
        var bonusInfo = bonusScore > 0 ? $" + [ {bonusScore} ] time bonus" : ".";

        scoreEarned = baseScore + bonusScore;
        _gameState.Score += scoreEarned;

        ColorConsole.WriteSuccess($"\n\nCorrect! You answered it in [ {responseTime.TotalSeconds:F1}s ]\nYou earned [ {baseScore} ] points{bonusInfo}");
      }
      else
      {
        ColorConsole.WriteError($"\n\nIncorrect! The answer was [ {correctAnswer} ]");
      }

      _gameHistory.AddRound(new GameRound(
        DateTime.Now,
        responseTime,
        operation.Symbol,
        isCorrect,
        firstNum,
        secondNum,
        userAnswer,
        correctAnswer,
        scoreEarned
      ));
    }
    catch (Exception ex)
    {
      ColorConsole.WriteError($"\nAn error occured: {ex.Message}");
    }
  }

  private async Task RunRandomMode()
  {
    Console.Write("\nHow many questions would you like to attempt?: ");
    var count = Utility.GetOptionWithRange(1, 20);

    var random = new Random();
    for (var i = 0; i < count; i++)
    {
      if (i > 0)
      {
        ColorConsole.WriteHighlight("\nPress ENTER for next question or type 'exit' to quit: ");
        var input = Console.ReadLine();
        if (input?.ToLower() == "exit") break;
      }

      var operation = _operations[random.Next(1, 5)];
      await PerformOperation(operation);
    }
  }

  private void ShowHistory()
  {
    var history = _gameHistory.GetHistory();
    if (history.Count == 0)
    {
      ColorConsole.WriteWarning("\nNo game history available.");
      return;
    }

    Console.WriteLine("\n[ HISTORY ]");
    foreach (var round in history)
    {
      Console.WriteLine(
        $"{round.Timestamp:HH:mm:ss} - {(round.IsCorrect ? "Correct" : "Incorrect")}: [ {round.FirstNumber} {round.Operation} {round.SecondNumber} = {round.UserAnswer ?? 0}" +
        $" ] ({round.ScoreEarned} points, {round.ResponseTime.TotalSeconds:F1}s)"
      );
    }
  }

  private void ChangeDifficulty()
  {
    Console.WriteLine($"\nSelect new difficulty (current: {_gameState.CurrentDifficulty})");
    Console.WriteLine("1. Easy");
    Console.WriteLine("2. Medium");
    Console.WriteLine("3. Hard");
    Console.Write("> ");

    var selection = Utility.GetOptionWithRange(1, 3);
    _gameState.CurrentDifficulty = selection switch
    {
      1 => DifficultyLevel.Easy,
      2 => DifficultyLevel.Medium,
      3 => DifficultyLevel.Hard,
      _ => _gameState.CurrentDifficulty
    };

    ColorConsole.WriteSuccess($"\nDifficulty changed:[ {_gameState.CurrentDifficulty} ]");
  }

  private void EndGame()
  {
    _gameState.IsGameOver = true;
    ColorConsole.WriteHighlight($"\nFinal score: [ {_gameState.Score} ]");
    ColorConsole.WriteWarning($"Game Over!");
  }

  private static void DisplayQuestion(int firstNum, int secondNum, string operation)
  {
    Console.WriteLine($"\n[ {firstNum} {operation} {secondNum} ]");
    Console.Write("> ");
  }

  private async Task<(int? Answer, TimeSpan ResponseTime)> GetUserAnswer()
  {
    var timeLimit = (int)_gameState.CurrentDifficulty;
    var stopwatch = Stopwatch.StartNew();

    using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeLimit));

    try
    {
      var input = await Task.Run(() =>
      {
        var userInput = "";
        while (!cts.Token.IsCancellationRequested)
        {
          if (Console.KeyAvailable)
          {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter) break;
            if (key.Key == ConsoleKey.Backspace && userInput.Length > 0)
            {
              userInput = userInput[..^1];
              Console.WriteLine("\b \b");
            }
            else if (char.IsDigit(key.KeyChar) || key.KeyChar == '-')
            {
              userInput += key.KeyChar;
              Console.Write(key.KeyChar);
            }
          }
        }

        return userInput;
      }, cts.Token);

      stopwatch.Stop();

      if (int.TryParse(input, out var answer))
      {
        return (answer, stopwatch.Elapsed);
      }
    }
    catch (OperationCanceledException)
    {
      stopwatch.Stop();
    }

    return (null, stopwatch.Elapsed);
  }

  private int CalculateBonusScore(TimeSpan responseTime)
  {
    const int maxBonus = 5;

    var difficultyMultiplier = (double)_gameState.CurrentDifficulty / 15.0;

    var timeFactor = Math.Min(maxBonus, 1 / (0.25 * responseTime.TotalSeconds));
    var bonusPoints = (int)Math.Round(timeFactor * (1 / difficultyMultiplier));

    return bonusPoints;
  }
}