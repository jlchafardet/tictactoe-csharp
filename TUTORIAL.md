# Tutorial: Building a Tic-Tac-Toe Game in C#

## Introduction
Welcome to this step-by-step tutorial on building a Tic-Tac-Toe game in C#. This tutorial is designed for beginners who want to learn how to develop a simple console-based game from scratch. By the end of this tutorial, you will have a fully functional Tic-Tac-Toe game with various features, including a smart opponent using the Minimax algorithm.

### Objective
- Build a console-based Tic-Tac-Toe game in C#.

### Prerequisites
- Basic understanding of C# and programming concepts.

## Step 1: Setting Up the Project

### Create a New Console Application
1. Open Visual Studio or your preferred C# IDE.
2. Create a new Console Application project.
3. Name the project "TicTacToe".

## Step 2: Basic Tic-Tac-Toe Game

### Define the Game Board and Print Method
1. **Define the Game Board**:
   - Use a 2D array to represent the game board.
   - Initialize the board with numbers 1-9.

2. **Print the Game Board**:
   - Create a method to print the current state of the game board.

3. **User and Computer Moves**:
   - Implement user input for moves using x/y coordinates.
   - Implement random moves for the computer.

4. **Check for Win**:
   - Create a method to check if the game is won.

5. **Main Game Loop**:
   - Implement the main game loop to alternate turns between the user and the computer.

```csharp:Program.cs
using System;

// This class represents the main program
class Program
{
    // The game board is represented by a 2D array of characters
    static char[,] board = {
        { '1', '2', '3' },
        { '4', '5', '6' },
        { '7', '8', '9' }
    };
    // The current player, 'X' for user and 'O' for computer
    static char player = 'X';

    // The main method, the entry point of the program
    static void Main(string[] args)
    {
        int turns = 0; // Counter for the number of turns taken
        bool gameWon = false; // Flag to check if the game is won
        Random rand = new Random(); // Random number generator for computer's move

        // Main game loop, runs until the game is won or all turns are taken
        while (!gameWon && turns < 9)
        {
            PrintBoard(); // Print the current state of the game board

            if (player == 'X')
            {
                // User's turn
                Console.WriteLine("User, enter your move (row and column): ");
                int row = int.Parse(Console.ReadLine()) - 1;
                int col = int.Parse(Console.ReadLine()) - 1;

                // Input validation loop
                while (row < 0 || row > 2 || col < 0 || col > 2 || board[row, col] == 'X' || board[row, col] == 'O')
                {
                    Console.WriteLine("Invalid move, try again.");
                    row = int.Parse(Console.ReadLine()) - 1;
                    col = int.Parse(Console.ReadLine()) - 1;
                }

                board[row, col] = player;
            }
            else
            {
                // Computer's turn
                int row, col;
                do
                {
                    row = rand.Next(0, 3);
                    col = rand.Next(0, 3);
                } while (board[row, col] == 'X' || board[row, col] == 'O');

                board[row, col] = player;
                Console.WriteLine($"Computer chose position ({row + 1}, {col + 1})");
            }

            turns++; // Increment the turn counter
            gameWon = CheckWin(); // Check if the game is won
            // Switch the player for the next turn
            player = (player == 'X') ? 'O' : 'X';
        }

        PrintBoard(); // Print the final state of the game board

        // Display the result of the game
        if (gameWon)
        {
            Console.WriteLine($"{(player == 'X' ? "Computer" : "User")} wins!");
        }
        else
        {
            Console.WriteLine("It's a draw!");
        }
    }

    // Method to print the current state of the game board
    static void PrintBoard()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(board[i, j]);
                if (j < 2) Console.Write("|");
            }
            if (i < 2) Console.WriteLine("\n-----");
        }
        Console.WriteLine();
    }

    // Method to check if the game is won
    static bool CheckWin()
    {
        // Array of winning positions
        int[,] winPositions = {
            {0, 0, 0, 1, 0, 2}, {1, 0, 1, 1, 1, 2}, {2, 0, 2, 1, 2, 2}, // Rows
            {0, 0, 1, 0, 2, 0}, {0, 1, 1, 1, 2, 1}, {0, 2, 1, 2, 2, 2}, // Columns
            {0, 0, 1, 1, 2, 2}, {0, 2, 1, 1, 2, 0}  // Diagonals
        };

        // Check each winning position
        for (int i = 0; i < winPositions.GetLength(0); i++)
        {
            if (board[winPositions[i, 0], winPositions[i, 1]] == board[winPositions[i, 2], winPositions[i, 3]] &&
                board[winPositions[i, 2], winPositions[i, 3]] == board[winPositions[i, 4], winPositions[i, 5]])
            {
                return true; // Return true if a winning position is found
            }
        }
        return false; // Return false if no winning position is found
    }
}
```

### Sample Output for initial Game Board and User Input

```plaintext
1|2|3
-----
4|5|6
-----
7|8|9

```

## Step 3: Enhancements

### 1-9 Selection Instead of x/y Coordinates
Update the game to use 1-9 selection for moves.

```csharp:Program.cs
// ... existing code ...
if (player == 'X')
{
    // User's turn
    Console.WriteLine("User, enter your move (1-9): ");
    int move = int.Parse(Console.ReadLine()) - 1;
    int row = move / 3;
    int col = move % 3;

    // Input validation loop
    while (move < 0 || move > 8 || board[row, col] == 'X' || board[row, col] == 'O')
    {
        Console.WriteLine("Invalid move, try again.");
        move = int.Parse(Console.ReadLine()) - 1;
        row = move / 3;
        col = move % 3;
    }

    board[row, col] = player;
}
// ... existing code ...
```

### Screen Clearing

Add screen clearing to improve the user experience.

```csharp:Program.cs
// ... existing code ...
while (!gameWon && turns < 9)
{
    Console.Clear(); // Clear the console screen
    PrintBoard(); // Print the current state of the game board
    // ... existing code ...
}
Console.Clear(); // Clear the console screen
PrintBoard(); // Print the final state of the game board
// ... existing code ...
```

### Add Colors

Add colors to the game board for better visual representation.

```csharp:Program.cs
// ... existing code ...
static void PrintBoard()
{
    for (int i = 0; i < 3; i++)
    {
        for (int j = 0; j < 3; j++)
        {
            if (board[i, j] == 'X')
            {
                Console.ForegroundColor = ConsoleColor.Blue; // User's move color
            }
            else if (board[i, j] == 'O')
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow; // Computer's move color
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White; // Empty cell color
            }

            Console.Write(board[i, j]);
            Console.ResetColor(); // Reset the console color
            if (j < 2) Console.Write("|");
        }
        if (i < 2) Console.WriteLine("\n-----");
    }
    Console.WriteLine();
}
// ... existing code ...
```

## Step 4: Storing and Displaying Results

### Store Game Results

Use a JSON file to store the number of wins, losses, and ties.

```csharp:Program.cs
using System.Text.Json;

// ... existing code ...
static string resultsFilePath = "gameResults.json"; // The file path where game results will be stored

// ... existing code ...
GameResults results = LoadResults(); // Load previous game results from the JSON file

// ... existing code ...
if (gameWon)
{
    if (player == 'X')
    {
        Console.WriteLine("Computer wins!");
        results.Losses++; // Increment the loss counter for the user
    }
    else
    {
        Console.WriteLine("User wins!");
        results.Wins++; // Increment the win counter for the user
    }
}
else
{
    Console.WriteLine("It's a draw!");
    results.Ties++; // Increment the tie counter
}

SaveResults(results); // Save the updated results to the JSON file
DisplayResults(results); // Display the updated results

// Method to load game results from a JSON file
static GameResults LoadResults()
{
    if (File.Exists(resultsFilePath))
    {
        string json = File.ReadAllText(resultsFilePath); // Read the JSON file
        return JsonSerializer.Deserialize<GameResults>(json) ?? new GameResults(); // Deserialize the JSON to a GameResults object
    }
    return new GameResults(); // Return a new GameResults object if the file does not exist
}

// Method to save game results to a JSON file
static void SaveResults(GameResults results)
{
    string json = JsonSerializer.Serialize(results); // Serialize the GameResults object to JSON
    File.WriteAllText(resultsFilePath, json); // Write the JSON to the file
}

// Method to display the game results
static void DisplayResults(GameResults results)
{
    Console.WriteLine("\nGame Results:");
    Console.WriteLine($"Wins: {results.Wins}"); // Display the number of wins
    Console.WriteLine($"Losses: {results.Losses}"); // Display the number of losses
    Console.WriteLine($"Ties: {results.Ties}"); // Display the number of ties
}

// Class to represent the game results
class GameResults
{
    public int Wins { get; set; } // Property to store the number of wins
    public int Losses { get; set; } // Property to store the number of losses
    public int Ties { get; set; } // Property to store the number of ties
}
```

## Step 5: Smart Opponent

### Add Smart Opponent Option

Ask the user if they want to play against a smart opponent.

```csharp:Program.cs
// ... existing code ...
static bool smartOpponent = false; // Flag to check if the player wants to play against a smart opponent

// ... existing code ...
// Ask the player if they want to play against a smart opponent
Console.WriteLine("Do you want to play against a smart opponent? (yes/no): ");
string? response = Console.ReadLine();
if (response != null)
{
    smartOpponent = (response.ToLower() == "yes");
}

// ... existing code ...
if (player == 'X')
{
    // User's turn
    Console.WriteLine("User, enter your move (1-9): ");
    int move = int.Parse(Console.ReadLine()) - 1;
    int row = move / 3;
    int col = move % 3;

    // Input validation loop
    while (move < 0 || move > 8 || board[row, col] == 'X' || board[row, col] == 'O')
    {
        Console.WriteLine("Invalid move, try again.");
        move = int.Parse(Console.ReadLine()) - 1;
        row = move / 3;
        col = move % 3;
    }

    board[row, col] = player;
}
else
{
    // Computer's turn
    int move;
    if (smartOpponent)
    {
        move = GetBestMove(); // Get the best move using the Minimax algorithm
    }
    else
    {
        do
        {
            move = rand.Next(0, 9); // Generate a random move
        } while (board[move / 3, move % 3] == 'X' || board[move / 3, move % 3] == 'O');
    }
    int row = move / 3;
    int col = move % 3;
    board[row, col] = player;
    Console.WriteLine($"Computer chose position {move + 1}");
}

// Method to get the best move for the computer using the Minimax algorithm
static int GetBestMove()
{
    int bestMove = -1;
    int bestValue = int.MinValue;

    // Loop through all possible moves
    for (int i = 0; i < 9; i++)
    {
        int row = i / 3;
        int col = i % 3;
        // Check if the cell is empty
        if (board[row, col] != 'X' && board[row, col] != 'O')
        {
            char original = board[row, col]; // Store the original value
            board[row, col] = 'O'; // Make the move for the computer
            int moveValue = Minimax(board, 0, false); // Get the value of the move using Minimax
            board[row, col] = original; // Undo the move

            // Update the best move if the current move is better
            if (moveValue > bestValue)
            {
                bestMove = i;
                bestValue = moveValue;
            }
        }
    }

    return bestMove; // Return the best move
}

// Minimax algorithm to evaluate the best move
static int Minimax(char[,] board, int depth, bool isMaximizing)
{
    // Check if the game is won
    if (CheckWin())
    {
        return isMaximizing ? -1 : 1;
    }

    // Check if the game is a draw
    bool isDraw = true;
    for (int i = 0; i < 3; i++)
    {
        for (int j = 0; j < 3; j++)
        {
            if (board[i, j] != 'X' && board[i, j] != 'O')
            {
                isDraw = false;
                break;
            }
        }
    }
    if (isDraw)
    {
        return 0;
    }

    // If it's the computer's turn (maximizing player)
    if (isMaximizing)
    {
        int bestValue = int.MinValue;

        // Loop through all possible moves
        for (int i = 0; i < 9; i++)
        {
            int row = i / 3;
            int col = i % 3;
            // Check if the cell is empty
            if (board[row, col] != 'X' && board[row, col] != 'O')
            {
                char original = board[row, col]; // Store the original value
                board[row, col] = 'O'; // Make the move for the computer
                int moveValue = Minimax(board, depth + 1, false); // Get the value of the move using Minimax
                board[row, col] = original; // Undo the move
                bestValue = Math.Max(bestValue, moveValue); // Update the best value
            }
        }

        return bestValue; // Return the best value
    }
    else // If it's the user's turn (minimizing player)
    {
        int bestValue = int.MaxValue;

        // Loop through all possible moves
        for (int i = 0; i < 9; i++)
        {
            int row = i / 3;
            int col = i % 3;
            // Check if the cell is empty
            if (board[row, col] != 'X' && board[row, col] != 'O')
            {
                char original = board[row, col]; // Store the original value
                board[row, col] = 'X'; // Make the move for the user
                int moveValue = Minimax(board, depth + 1, true); // Get the value of the move using Minimax
                board[row, col] = original; // Undo the move
                bestValue = Math.Min(bestValue, moveValue); // Update the best value
            }
        }

        return bestValue; // Return the best value
    }
}
```

## Conclusion

### Recap

In this tutorial, we built a console-based Tic-Tac-Toe game in C# from scratch. We started with a basic version of the game and gradually added features such as 1-9 selection, screen clearing, colors, result storing, and a smart opponent using the Minimax algorithm.

### Next Steps

- Encourage learners to add more features or try building other simple games.
- Some ideas for further enhancements:
  - Add a graphical user interface (GUI) using a framework like Windows Forms or WPF.
  - Implement a two-player mode where two users can play against each other.
  - Add sound effects and animations to make the game more engaging.

By following this structured approach, beginners can understand the process of developing an application from start to finish, gradually adding complexity and features. This tutorial can serve as a comprehensive guide for those who are either unable to access or prefer not to use AI-assisted coding.

Happy coding!