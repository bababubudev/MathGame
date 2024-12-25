namespace MathGame.Operations;

using MathGame.Models;
using MathGame.Services;

public class MultiplicationOperation : IMathOperation
{
  public string Symbol => "*";
  public string Name => "Multiplication";

  public int Calculate(int firstNum, int secondNum) => firstNum * secondNum;

  public (int, int) GeneratedNumbers(DifficultyLevel difficulty, int maxRange)
  {
    var random = new Random();
    var min = Utility.GetMinRangeWithDifficulty(difficulty, maxRange);
    var max = Utility.GetMaxRangeWithDifficulty(difficulty, maxRange);
    return (random.Next(min, max), random.Next(min, max));
  }
}