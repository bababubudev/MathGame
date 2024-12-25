using System.Diagnostics;
using MathGame;

MathGameLogic mathGame = new();
Random random = new();

int firstNum, secondNum;
int maxRange = 30;
int userScore = 0;

bool isGameOver = false;

DifficultyLevel currentDifficulty = DifficultyLevel.Easy;

static int GetMaxRangeWithDifficulty(DifficultyLevel difficulty, int maxRange)
{
  return difficulty switch
  {
    DifficultyLevel.Easy => maxRange * 1 + 1,
    DifficultyLevel.Medium => maxRange * 2 + 1,
    DifficultyLevel.Hard => maxRange * 3 + 1,
    _ => maxRange + 1,
  };
}

static int GetMinRangeWithDifficulty(DifficultyLevel difficulty, int maxRange)
{
  return difficulty switch
  {
    DifficultyLevel.Easy => 1,
    DifficultyLevel.Medium => maxRange,
    DifficultyLevel.Hard => maxRange * 1 + 1 / 2,
    _ => 1,
  };
}

static DifficultyLevel ChangeDifficulty()
{
  Console.WriteLine("\nChoose a difficulty: ");
  Console.WriteLine("1. Easy");
  Console.WriteLine("2. Medium");
  Console.WriteLine("3. Hard");
  Console.Write("> ");

  var selectedDifficulty = Utility.GetOptionWithRange(1, 3);

  return selectedDifficulty switch
  {
    1 => DifficultyLevel.Easy,
    2 => DifficultyLevel.Medium,
    3 => DifficultyLevel.Hard,
    _ => DifficultyLevel.Easy,
  };
}

static void DisplayMathGameQuestions(int firstNum, int secondNum, char operation)
{
  Console.WriteLine($"\n[ {firstNum} {operation} {secondNum} ]");
  Console.Write("> ");
}

static int GetUserMenuSelection()
{
  MathGameLogic.ShowMenu();

  var selection = Utility.GetOptionWithRange(1, 8);

  return selection;
}

static async Task<int?> GetUserResponseByDifficulty(DifficultyLevel difficulty)
{
  int response = 0;
  int timeout = (int)difficulty;

  Stopwatch stopwatch = new();
  stopwatch.Start();

  using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeout));

  try
  {
    string? result = await Task.Run(() =>
    {
      var input = "";
      while (!cts.Token.IsCancellationRequested)
      {
        if (Console.KeyAvailable)
        {
          var keyInfo = Console.ReadKey(intercept: true);
          if (keyInfo.Key == ConsoleKey.Enter) { break; }
          input += keyInfo.KeyChar;
          Console.Write(keyInfo.KeyChar);
        }
      }

      if (cts.Token.IsCancellationRequested && string.IsNullOrEmpty(input))
        throw new OperationCanceledException();

      return input;
    }, cts.Token);

    stopwatch.Stop();

    if (int.TryParse(result, out response))
    {
      Console.WriteLine($"\nTime taken to answer: {stopwatch.Elapsed.ToString(@"m\:ss\.fff")}");
      return response;
    }
    else
    {
      Console.WriteLine("\nInvalid input. Please try again.");
      return null;
    }
  }
  catch (OperationCanceledException)
  {
    stopwatch.Stop();
    Console.WriteLine("\nTime's Up!");
    return null;
  }
}

static int ValidateResult(int result, int? userResponse, int score)
{
  int incrementScore = 5;

  if (userResponse == null)
  {
    Console.WriteLine("You didn't provide an answer in time!\n");
    return score;
  }

  if (result == userResponse)
  {
    Console.WriteLine("Well done! You answered correctly");
    Console.WriteLine($"You earned {incrementScore} points\n");

    score += incrementScore;
  }
  else
  {
    Console.WriteLine($"Incorrect! Correct answer is {result}\n");
  }

  return score;
}

static async Task<int> PerformOperation(MathGameLogic mathGame, int firstNum, int secondNum, char operation, int score, DifficultyLevel difficultyLevel)
{
  int result;
  int? userResponse;

  DisplayMathGameQuestions(firstNum, secondNum, operation);

  result = mathGame.MathOperation(firstNum, secondNum, operation);
  userResponse = await GetUserResponseByDifficulty(difficultyLevel);
  score += ValidateResult(result, userResponse, score);

  return score;
}

//* GAME LOOP *//
while (!isGameOver)
{
  int menuSelection = GetUserMenuSelection();
  int difficultyMinThreshold = GetMinRangeWithDifficulty(currentDifficulty, maxRange);
  int difficultyMaxThreshold = GetMaxRangeWithDifficulty(currentDifficulty, maxRange);

  firstNum = random.Next(difficultyMinThreshold, difficultyMaxThreshold);
  secondNum = random.Next(difficultyMinThreshold, difficultyMaxThreshold);

  switch (menuSelection)
  {
    case 1:
      userScore += await PerformOperation(mathGame, firstNum, secondNum, '+', userScore, currentDifficulty);
      break;
    case 2:
      userScore += await PerformOperation(mathGame, firstNum, secondNum, '-', userScore, currentDifficulty);
      break;
    case 3:
      userScore += await PerformOperation(mathGame, firstNum, secondNum, '*', userScore, currentDifficulty);
      break;
    case 4:
      if (firstNum % secondNum != 0)
      {
        secondNum = random.Next(difficultyMinThreshold, difficultyMaxThreshold);
        firstNum = secondNum * random.Next(difficultyMinThreshold, difficultyMaxThreshold);
      }

      userScore += await PerformOperation(mathGame, firstNum, secondNum, '/', userScore, currentDifficulty);
      break;
    case 5:
      Console.Write("\nPlease enter a number of questions to attempt: ");
      int numberOfQuestions = Utility.GetOptionWithRange(0, 99);

      /* Random question generations */
      for (int i = 0; i < numberOfQuestions; i++)
      {
        if (i == 0) Console.WriteLine();
        Console.Write("Press enter to continue or type 'exit' to quit: ");
        string? input = Console.ReadLine();
        if (input != null && input.ToLower().Equals("exit"))
        {
          Console.WriteLine("\nExiting random mode");
          break;
        }

        int randomOperation = random.Next(1, 5);

        firstNum = random.Next(difficultyMinThreshold, difficultyMaxThreshold);
        secondNum = random.Next(difficultyMinThreshold, difficultyMaxThreshold);

        switch (randomOperation)
        {
          case 1:
            userScore += await PerformOperation(mathGame, firstNum, secondNum, '+', userScore, currentDifficulty);
            break;
          case 2:
            userScore += await PerformOperation(mathGame, firstNum, secondNum, '-', userScore, currentDifficulty);
            break;
          case 3:
            userScore += await PerformOperation(mathGame, firstNum, secondNum, '*', userScore, currentDifficulty);
            break;
          case 4:
            if (firstNum % secondNum != 0)
            {
              secondNum = random.Next(1, difficultyMaxThreshold);
              firstNum = secondNum * random.Next(1, difficultyMaxThreshold);
            }

            userScore += await PerformOperation(mathGame, firstNum, secondNum, '/', userScore, currentDifficulty);
            break;
        }
      }

      break;
    case 6:
      Console.WriteLine("\n[ HISTORY ]");

      if (mathGame.GameHistory.Count == 0)
      {
        Console.WriteLine("Nothing here...\n");
      }
      else
      {
        foreach (var item in mathGame.GameHistory)
        {
          Console.WriteLine(item);
        }
        Console.WriteLine();
      }

      break;
    case 7:
      currentDifficulty = ChangeDifficulty();
      DifficultyLevel diffEnum = (DifficultyLevel)currentDifficulty;
      Enum.IsDefined<DifficultyLevel>(diffEnum);
      Console.WriteLine($"\nDifficulty changed: [ {currentDifficulty} ]\n");
      break;
    case 8:
      isGameOver = true;
      Console.WriteLine($"\n[ Your final score is {userScore} ]\n");
      break;
  }
}