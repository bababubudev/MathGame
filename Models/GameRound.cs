namespace MathGame.Models;

public record GameRound(
  DateTime Timestamp,
  TimeSpan ResponseTime,
  string Operation,
  bool IsCorrect,
  int FirstNumber,
  int SecondNumber,
  int? UserAnswer,
  int CorrectAnswer,
  int ScoreEarned
);