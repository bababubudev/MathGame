# Math Game Console Application

An advanced, interactive console-based mathematics game built in C# that helps users practice basic arithmetic operations with adjustable difficulty levels, time constraints, and comprehensive statistics tracking.

## Features

### Core Functionality
- Multiple operation modes:
  - Addition
  - Subtraction
  - Multiplication
  - Division
  - Random mode (mix of all operations)

### Difficulty System
- Three difficulty levels with customizable settings:
  - Easy (45 seconds per question)
  - Medium (30 seconds per question)
  - Hard (15 seconds per question)
- Each level affects:
  - Time limits
  - Number ranges
  - Scoring multipliers

### Scoring System
- Base points for correct answers
- Dynamic bonus points based on:
  - Response time
  - Difficulty level
  - Speed multipliers

### Advanced Features
- Comprehensive statistics tracking
- Color-coded feedback system
- Real-time progress monitoring
- Game history with detailed records
- Configurable game settings via appsettings.json

## Technical Requirements

- .NET 8.0 SDK or later
- Compatible operating systems:
  - Windows
  - macOS
  - Linux

## Installation

1. Clone the repository:
```bash
git clone https://github.com/bububabadev/MathGame.git
cd MathGame
```

2. Install dependencies:
```bash
dotnet restore
```

3. Build the project:
```bash
dotnet build
```

4. Run the application:
```bash
dotnet run
```

## Project Structure

```
MathGame/
  ├── Program.cs                 # Application entry point
  ├── appsettings.json          # Configuration settings
  ├── MathGame.csproj           # Project file
  ├── Models/                   # Data models
  ├── Services/                 # Core game services
  └── Operations/               # Math operations
```

## Configuration

The game can be customized through `appsettings.json`:

```json
{
  "GameSettings": {
    "BasePoints": 5,
    "MaxBonus": 10,
    "MaxRange": 30,
    "Difficulties": {
      "Easy": {
        "TimeLimit": 45,
        "RangeMultiplier": 1,
        "ScoreMultiplier": 0.2
      }
      // ... other difficulty settings
    }
  }
}
```

## How to Play

1. Start the game and choose from the available options:
   - [1] Addition
   - [2] Subtraction
   - [3] Multiplication
   - [4] Division
   - [5] Random mode
   - [6] Show game history
   - [7] Change difficulty
   - [8] Exit

2. For each question:
   - A math problem will be displayed
   - Enter your answer before the timer runs out
   - Receive immediate color-coded feedback
   - Earn points based on speed and accuracy

3. Random Mode:
   - Choose the number of questions you want to attempt
   - Questions are randomly selected from all operation types
   - Track your progress with the built-in statistics

## Statistics and Tracking

The game provides detailed statistics including:
- Overall accuracy rate
- Average response time
- Performance by operation type
- Score distribution
- Historical game records

## Visual Feedback

The game uses color-coding for better user experience:
- Green: Correct answers
- Red: Incorrect answers
- Yellow: Warnings and important notices
- Cyan: Highlights and headers

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request