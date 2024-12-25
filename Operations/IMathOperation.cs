namespace MathGame.Operations;

using MathGame.Models;

public interface IMathOperation
{
  string Symbol { get; }
  string Name { get; }
  int Calculate(int firstNum, int secondNum);
  (int, int) GeneratedNumbers(DifficultyLevel difficulty, int maxRange);
}