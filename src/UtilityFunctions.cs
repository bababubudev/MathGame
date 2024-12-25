namespace MathGame
{
  public class Utility
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
  }

  enum DifficultyLevel
  {
    Easy = 45,
    Medium = 30,
    Hard = 15
  }
}