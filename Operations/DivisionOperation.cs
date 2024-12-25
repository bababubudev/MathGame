namespace MathGame.Operations;

using MathGame.Models;
using MathGame.Services;

public class DivisionOperation : IMathOperation
{
  public string Symbol => "/";
  public string Name => "Division";

  public int Calculate(int firstNum, int secondNum)
  {
    if (secondNum == 0) throw new DivideByZeroException("Cannot divide by zero");
    return firstNum / secondNum;
  }

  public (int, int) GeneratedNumbers(DifficultyLevel difficulty, int maxRange)
  {
    var random = new Random();
    var min = Utility.GetMinRangeWithDifficulty(difficulty, maxRange);
    var max = Utility.GetMaxRangeWithDifficulty(difficulty, maxRange);

    var secondNum = random.Next(min, max);
    var multiplier = random.Next(min, max);
    var firstNum = secondNum * multiplier;

    return (firstNum, secondNum);
  }
}