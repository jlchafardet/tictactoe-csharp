﻿using System;
using System.IO;
using System.Text.Json;

// This class represents the main program
class Program
{
    // The game board is represented by an array of characters
    static char[] board = { '0', '1', '2', '3', '4', '5', '6', '7', '8' };
    // The current player, 'X' for user and 'O' for computer
    static char player = 'X';
    // The file path where game results will be stored
    static string resultsFilePath = "gameResults.json";
    // Flag to check if the player wants to play against a smart opponent
    static bool smartOpponent = false;

    // The main method, the entry point of the program
    static void Main(string[] args)
    {
        int move; // Variable to store the player's move
        int turns = 0; // Counter for the number of turns taken
        bool gameWon = false; // Flag to check if the game is won
        Random rand = new Random(); // Random number generator for computer's move

        // Ask the player if they want to play against a smart opponent
        Console.WriteLine("Do you want to play against a smart opponent? (yes/no): ");
        string? response = Console.ReadLine();
        if (response != null)
        {
            smartOpponent = (response.ToLower() == "yes");
        }

        // Load previous game results from the JSON file
        GameResults results = LoadResults();

        // Main game loop, runs until the game is won or all turns are taken
        while (!gameWon && turns < 9)
        {
            Console.Clear(); // Clear the console screen
            PrintBoard(); // Print the current state of the game board

            if (player == 'X')
            {
                // User's turn
                Console.WriteLine("User, enter your move (0-8): ");
                // Input validation loop
                while (!int.TryParse(Console.ReadLine(), out move) || move < 0 || move > 8 || board[move] == 'X' || board[move] == 'O')
                {
                    Console.WriteLine("Invalid move, try again.");
                }
            }
            else
            {
                // Computer's turn
                if (smartOpponent)
                {
                    move = GetBestMove(); // Get the best move using the Minimax algorithm
                }
                else
                {
                    do
                    {
                        move = rand.Next(0, 9); // Generate a random move
                    } while (board[move] == 'X' || board[move] == 'O'); // Ensure the move is valid
                }
                Console.WriteLine($"Computer chose position {move}");
            }

            // Update the board with the player's move
            board[move] = player;
            turns++; // Increment the turn counter
            gameWon = CheckWin(); // Check if the game is won
            // Switch the player for the next turn
            player = (player == 'X') ? 'O' : 'X';
        }

        Console.Clear(); // Clear the console screen
        PrintBoard(); // Print the final state of the game board

        // Display the result of the game
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

        // Save the updated results to the JSON file
        SaveResults(results);
        // Display the updated results
        DisplayResults(results);
    }

    // Method to print the current state of the game board
    static void PrintBoard()
    {
        for (int i = 0; i < board.Length; i++)
        {
            if (i % 3 == 0 && i != 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow; // Set color for the grid
                Console.WriteLine("\n---|---|---"); // Print the grid line
            }

            if (i % 3 != 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow; // Set color for the grid
                Console.Write("|"); // Print the grid separator
            }

            // Set color based on the player
            if (board[i] == 'X')
            {
                Console.ForegroundColor = ConsoleColor.Blue; // User's move color
            }
            else if (board[i] == 'O')
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow; // Computer's move color
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White; // Empty cell color
            }

            Console.Write($" {board[i]} "); // Print the cell value
        }
        Console.ResetColor(); // Reset the console color
        Console.WriteLine(); // Move to the next line
    }

    // Method to check if the game is won
    static bool CheckWin()
    {
        // Array of winning positions
        int[,] winPositions = {
            {0, 1, 2}, {3, 4, 5}, {6, 7, 8}, // Rows
            {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, // Columns
            {0, 4, 8}, {2, 4, 6}             // Diagonals
        };

        // Check each winning position
        for (int i = 0; i < winPositions.GetLength(0); i++)
        {
            if (board[winPositions[i, 0]] == board[winPositions[i, 1]] &&
                board[winPositions[i, 1]] == board[winPositions[i, 2]])
            {
                return true; // Return true if a winning position is found
            }
        }
        return false; // Return false if no winning position is found
    }

    // Method to load game results from a JSON file
    static GameResults LoadResults()
    {
        if (File.Exists(resultsFilePath))
        {
            string json = File.ReadAllText(resultsFilePath); // Read the JSON file
            GameResults? results = JsonSerializer.Deserialize<GameResults>(json); // Deserialize the JSON to a GameResults object
            return results ?? new GameResults(); // Return the deserialized object or a new GameResults object if null
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

    // Method to get the best move for the computer using the Minimax algorithm
    static int GetBestMove()
    {
        int bestMove = -1;
        int bestValue = int.MinValue;

        // Loop through all possible moves
        for (int i = 0; i < board.Length; i++)
        {
            // Check if the cell is empty
            if (board[i] != 'X' && board[i] != 'O')
            {
                char original = board[i]; // Store the original value
                board[i] = 'O'; // Make the move for the computer
                int moveValue = Minimax(board, 0, false); // Get the value of the move using Minimax
                board[i] = original; // Undo the move

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
    static int Minimax(char[] board, int depth, bool isMaximizing)
    {
        // Check if the game is won
        if (CheckWin())
        {
            return isMaximizing ? -1 : 1;
        }

        // Check if the game is a draw
        if (Array.IndexOf(board, '0') == -1 && Array.IndexOf(board, '1') == -1 && Array.IndexOf(board, '2') == -1 &&
            Array.IndexOf(board, '3') == -1 && Array.IndexOf(board, '4') == -1 && Array.IndexOf(board, '5') == -1 &&
            Array.IndexOf(board, '6') == -1 && Array.IndexOf(board, '7') == -1 && Array.IndexOf(board, '8') == -1)
        {
            return 0;
        }

        // If it's the computer's turn (maximizing player)
        if (isMaximizing)
        {
            int bestValue = int.MinValue;

            // Loop through all possible moves
            for (int i = 0; i < board.Length; i++)
            {
                // Check if the cell is empty
                if (board[i] != 'X' && board[i] != 'O')
                {
                    char original = board[i]; // Store the original value
                    board[i] = 'O'; // Make the move for the computer
                    int moveValue = Minimax(board, depth + 1, false); // Get the value of the move using Minimax
                    board[i] = original; // Undo the move
                    bestValue = Math.Max(bestValue, moveValue); // Update the best value
                }
            }

            return bestValue; // Return the best value
        }
        else // If it's the user's turn (minimizing player)
        {
            int bestValue = int.MaxValue;

            // Loop through all possible moves
            for (int i = 0; i < board.Length; i++)
            {
                // Check if the cell is empty
                if (board[i] != 'X' && board[i] != 'O')
                {
                    char original = board[i]; // Store the original value
                    board[i] = 'X'; // Make the move for the user
                    int moveValue = Minimax(board, depth + 1, true); // Get the value of the move using Minimax
                    board[i] = original; // Undo the move
                    bestValue = Math.Min(bestValue, moveValue); // Update the best value
                }
            }

            return bestValue; // Return the best value
        }
    }
}

// Class to represent the game results
class GameResults
{
    public int Wins { get; set; } // Property to store the number of wins
    public int Losses { get; set; } // Property to store the number of losses
    public int Ties { get; set; } // Property to store the number of ties
}