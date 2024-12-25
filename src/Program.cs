using System.Diagnostics;
using MathGame;

MathGameLogic mathGame = new();
Random random = new();

int firstNum, secondNum;
int userScore = 0;

bool isGameOver = false;

DifficultyLevel currentDifficulty = DifficultyLevel.Easy;

DifficultyLevel ChangeDifficulty() {
  Console.WriteLine("Choose a difficulty: ");
  Console.WriteLine("1. Easy");
  Console.WriteLine("2. Medium");
  Console.WriteLine("3. Hard");

  var selectedDifficulty = Utility.GetOptionWithRange(1, 3);

  return selectedDifficulty switch
  {
    1 => DifficultyLevel.Easy,
    2 => DifficultyLevel.Medium,
    3 => DifficultyLevel.Hard,
    _ => DifficultyLevel.Easy,
  };
}

void DisplayMathGameQuestions(int firstNum, int secondNum, char operation) {
  Console.WriteLine($"\n[ {firstNum} {operation} {secondNum} ]");
  Console.Write("> ");
}

int GetUserMenuSelection() {
  MathGameLogic.ShowMenu();

  var selection = Utility.GetOptionWithRange(1, 8);

  return selection;
}

async Task<int?> GetUserResponse(DifficultyLevel difficulty) {
  int response = 0;
  int timeout = (int)difficulty;

  Stopwatch stopwatch = new();
  stopwatch.Start();

  Task<string?> getUserInputTask = Task.Run(() => Console.ReadLine());

  try {
    string? result = await Task.WhenAny(getUserInputTask, Task.Delay(timeout * 1000)) == getUserInputTask ? getUserInputTask.Result: null;
    stopwatch.Stop();

    if (result != null && int.TryParse(result, out response)) {
      Console.WriteLine($"\nTime taken to answer: {stopwatch.Elapsed.ToString(@"m\:ss\.fff")}");
      return response;
    }
    else {
      throw new OperationCanceledException();
    }
  }
  catch (OperationCanceledException) {
    Console.WriteLine("\nTime's Up!");
    return null;
  }
}

int ValidateResult(int result, int? userResponse, int score) {
  int incrementScore = 5;

  if (userResponse == null) {
    Console.WriteLine("\nYou didn't provide an answer in time!\n");
    return score;
  }

  if (result == userResponse) {
    Console.WriteLine("Well done! You answered correctly");
    Console.WriteLine($"You earned {incrementScore} points\n");

    score += incrementScore;
  }
  else {
    Console.WriteLine($"Incorrect! Correct answer is {result}\n");
  }

  return score;
}

async Task<int> PerformOperation(MathGameLogic mathGame, int firstNum, int secondNum, char operation, int score, DifficultyLevel difficultyLevel) {
  int result;
  int? userResponse;

  DisplayMathGameQuestions(firstNum, secondNum, operation);

  result = mathGame.MathOperation(firstNum, secondNum, operation);
  userResponse = await GetUserResponse(difficultyLevel);
  score += ValidateResult(result, userResponse, score);

  return score;
}

while (!isGameOver) {
  int menuSelection = GetUserMenuSelection();

  firstNum = random.Next(1, 101);
  secondNum = random.Next(1, 101);

  switch (menuSelection) {
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
      if (firstNum % secondNum != 0) {
        secondNum = random.Next(1, 101);
        firstNum = secondNum * random.Next(1, 101 / secondNum);
      }

      userScore += await PerformOperation(mathGame, firstNum, secondNum, '/', userScore, currentDifficulty);
      break;
    case 5:
      Console.WriteLine("Please enter a number of questions to attempt: ");
      int numberOfQuestions = Utility.GetOptionWithRange(0, 99);

      /* Random question generations */
      for (int i = 0; i < numberOfQuestions; i ++) {
        int randomOperation = random.Next(1, 5);

        firstNum = random.Next(1, 101);
        secondNum = random.Next(1, 101);

        switch (randomOperation) {
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
            if (firstNum % secondNum != 0) {
              secondNum = random.Next(1, 101);
              firstNum = secondNum * random.Next(1, 101);
            }

            userScore += await PerformOperation(mathGame, firstNum, secondNum, '/', userScore, currentDifficulty);
            break;
        }
      }

      break;
    case 6:
      Console.WriteLine("[ HISTORY ]\n");
      foreach (var item in mathGame.GameHistory)
      {
        Console.WriteLine(item);
      }
      Console.WriteLine();

      break;
    case 7:
      currentDifficulty = ChangeDifficulty();
      DifficultyLevel diffEnum = (DifficultyLevel)currentDifficulty;
      Enum.IsDefined<DifficultyLevel>(diffEnum);
      Console.WriteLine($"Difficulty changed: {currentDifficulty}");
      break;
    case 8:
      isGameOver = true;
      Console.WriteLine($"Your final score is {userScore}");
      break;
  }
}