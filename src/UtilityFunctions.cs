namespace MathGame
{
  public class Utility {
    public static int GetOptionWithRange(int min, int max) {
      int selection = 0;
      while (!int.TryParse(Console.ReadLine(), out selection) || selection < min || selection > max) {
        Console.WriteLine($"Number should be in the range of {min} to {max}");
      }

      return selection;
    }
  }

  enum DifficultyLevel
  {
    Easy = 45,
    Medium = 30,
    Hard = 15
  }
}