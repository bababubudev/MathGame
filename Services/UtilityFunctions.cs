namespace MathGame.Services;

using MathGame.Models;

public static class Utility
{
  public static int GetOptionWithRange(int min, int max)
  {
    int selection = 0;
    while (!int.TryParse(Console.ReadLine(), out selection) || selection < min || selection > max)
    {
      Console.WriteLine($"\nOnly numbers between {min} and {max} allowed!");
      Console.Write("> ");
    }

    return selection;
  }

  public static int GetMaxRangeWithDifficulty(DifficultyLevel difficulty, int maxRange)
  {
    return difficulty switch
    {
      DifficultyLevel.Easy => maxRange * 1 + 1,
      DifficultyLevel.Medium => maxRange * 2 + 1,
      DifficultyLevel.Hard => maxRange * 3 + 1,
      _ => maxRange + 1,
    };
  }

  public static int GetMinRangeWithDifficulty(DifficultyLevel difficulty, int maxRange)
  {
    return difficulty switch
    {
      DifficultyLevel.Easy => 1,
      DifficultyLevel.Medium => maxRange,
      DifficultyLevel.Hard => maxRange * 3 / 2,
      _ => 1,
    };
  }
}