namespace MathGame.Services;

public static class ColorConsole
{
  public static void WriteSuccess(string message)
  {
    var currentColor = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(message);
    Console.ForegroundColor = currentColor;
  }

  public static void WriteError(string message)
  {
    var currentColor = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(message);
    Console.ForegroundColor = currentColor;
  }

  public static void WriteWarning(string message)
  {
    var currentColor = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(message);
    Console.ForegroundColor = currentColor;
  }

  public static void WriteHighlight(string message)
  {
    var currentColor = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine(message);
    Console.ForegroundColor = currentColor;
  }
}