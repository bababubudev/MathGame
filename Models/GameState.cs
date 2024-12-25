namespace MathGame.Models;

public class GameState
{
  public int Score { get; set; }
  public DifficultyLevel CurrentDifficulty { get; set; } = DifficultyLevel.Easy;
  public bool IsGameOver { get; set; }
}