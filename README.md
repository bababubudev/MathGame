# Math Game Console Application

A dynamic, interactive console-based mathematics game built in C# that helps users practice basic arithmetic operations with adjustable difficulty levels and time constraints.

## Features

- Multiple operation modes:
  - Addition
  - Subtraction
  - Multiplication
  - Division
  - Random mode (mix of all operations)

- Three difficulty levels:
  - Easy (45 seconds per question)
  - Medium (30 seconds per question)
  - Hard (15 seconds per question)

- Scoring system that rewards:
  - Correct answers
  - Quick responses
  - Higher difficulties

- Game history tracking
- Time-based challenges
- Customizable number of questions in random mode

## How to Play

1. Start the game and choose from the following options:
   - [1] Addition
   - [2] Subtraction
   - [3] Multiplication
   - [4] Division
   - [5] Random mode
   - [6] Show game history
   - [7] Change difficulty
   - [8] Exit

2. For individual operations (options 1-4):
   - A question will be displayed in the format `[ number1 operation number2 ]`
   - Enter your answer before the time runs out
   - Receive immediate feedback and points

3. For random mode (option 5):
   - Choose how many questions you want to attempt
   - Questions will be generated randomly from all operation types
   - Press enter to continue to the next question or type 'exit' to quit

## Scoring System

- Base points: 5 points per correct answer
- Bonus points are awarded based on:
  - Response time
  - Difficulty level
  - The faster you answer and the higher the difficulty, the more bonus points you earn

## Difficulty Levels

Each difficulty level affects:
- Time limit per question
- Number range for operations
- Scoring multiplier

| Difficulty | Time Limit | Number Range            | Score Multiplier |
|------------|------------|------------------------|------------------|
| Easy       | 45 seconds | 1 to maxRange x 1        | 0.2             |
| Medium     | 30 seconds | maxRange to maxRange x 2 | 0.133           |
| Hard       | 15 seconds | maxRange x 1.5 to maxRange x 3 | 0.067       |

## Technical Requirements

- .NET Core SDK (latest version recommended)
- Console-compatible terminal or command prompt
- Windows, macOS, or Linux operating system

## Building and Running

1. Clone the repository
2. Navigate to the project directory
3. Run `dotnet build`
4. Run `dotnet run`

## Project Structure

- `Program.cs`: Main game loop and core gameplay functions
- `MathGameLogic.cs`: Core game logic and history tracking
- `UtilityFunctions.cs`: Utility functions and enums for the game

## Contributing

Feel free to fork the repository and submit pull requests for any improvements or bug fixes.