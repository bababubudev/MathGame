namespace MathGame.Services;

using MathGame.Models;

public interface IGameHistory
{
  void AddRound(GameRound round);
  IReadOnlyList<GameRound> GetHistory();
  void Clear();
}