namespace MathGame.Services;

using MathGame.Models;

public class Statistics
{
  private readonly IGameHistory _gameHistory;

  public Statistics(IGameHistory gameHistory)
  {
    _gameHistory = gameHistory;
  }

  public void ShowStatistics()
  {
    var history = _gameHistory.GetHistory();
    if (!history.Any())
    {
      ColorConsole.WriteWarning("\nNo statistics available");
      return;
    }

    var totalQuestions = history.Count;
    var correctAnswers = history.Count(r => r.IsCorrect);
    var accuracy = (double)correctAnswers / totalQuestions * 100;
    var averageResponseTime = history.Average(r => r.ResponseTime.TotalSeconds);
    var totalScore = history.Sum(r => r.ScoreEarned);

    var statsByOperation = history
      .GroupBy(r => r.OperationName)
      .Select(g => new
      {
        Operation = g.Key,
        TotalQuestions = g.Count(),
        CorrectAnswers = g.Count(r => r.IsCorrect),
        AverageTime = g.Average(r => r.ResponseTime.TotalSeconds),
        AverageScore = g.Average(r => r.ScoreEarned)
      });

    ColorConsole.WriteHighlight("\n=== Game Statistics ===");
    Console.WriteLine($"\nTotal Questions: {totalQuestions}");
    Console.WriteLine($"Correct Answers: {correctAnswers}");
    Console.WriteLine($"Accuracy: {accuracy:F1}%");
    Console.WriteLine($"Average Response Time: {averageResponseTime:F2} s");
    ColorConsole.WriteSuccess($"Total Score: {totalScore}");

    ColorConsole.WriteHighlight("\nStatistics by Operation:");
    foreach (var stat in statsByOperation)
    {
      Console.WriteLine($"\n{stat.Operation}:");
      Console.WriteLine($"  Questions: {stat.TotalQuestions}");
      Console.WriteLine($"  Accuracy: {(double)stat.CorrectAnswers / stat.TotalQuestions * 100:F1}%");
      Console.WriteLine($"  Average Time: {stat.AverageTime:F2} seconds");
      Console.WriteLine($"  Average Score: {stat.AverageScore:F1} points");
    }
  }
}