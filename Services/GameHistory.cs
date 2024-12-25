namespace MathGame.Services;

using System.Collections.Generic;
using MathGame.Models;

public class GameHistory : IGameHistory
{
  private readonly List<GameRound> _history = [];

  public void AddRound(GameRound round) => _history.Add(round);

  public void Clear() => _history.Clear();

  public IReadOnlyList<GameRound> GetHistory() => _history.AsReadOnly();
}