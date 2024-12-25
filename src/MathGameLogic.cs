namespace MathGame
{
    public class MathGameLogic
    {
        public List<String> GameHistory { get; set; } = [];

        public static void ShowMenu() {
            Console.WriteLine("Choose an option to select the game mode: ");
            Console.WriteLine("1. Addition");
            Console.WriteLine("2. Subtraction");
            Console.WriteLine("3. Multiplication");
            Console.WriteLine("4. Division");
            Console.WriteLine("5. Random mode");
            Console.WriteLine("6. Show game history");
            Console.WriteLine("7. Change difficulty");
            Console.WriteLine("8. Exit");
        }

        public int MathOperation(int firstNum, int secondNum, char mathOperation) {
            switch (mathOperation) {
                case '+':
                    GameHistory.Add($"{firstNum} + {secondNum} = {firstNum + secondNum}");
                    return firstNum + secondNum;
                case '-':
                    GameHistory.Add($"{firstNum} - {secondNum} = {firstNum - secondNum}");
                    return firstNum - secondNum;
                case '*':
                    GameHistory.Add($"{firstNum} * {secondNum} = {firstNum * secondNum}");
                    return firstNum * secondNum;
                case '/':
                    while (firstNum < 0 || secondNum > 100) {
                        try
                        {
                            Console.WriteLine("Number should be between 0 and 100");
                            firstNum = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (System.Exception)
                        {
                            Console.WriteLine("Error reading input.");
                        }
                    }

                    GameHistory.Add($"{firstNum} / {secondNum} = {firstNum / secondNum}");
                    return firstNum / secondNum;
                default:
                    return 0;
            }
        }
    }
}