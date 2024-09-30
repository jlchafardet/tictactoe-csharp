using System;
using System.IO;
using System.Text.Json;

class Program
{
    static char[] board = { '0', '1', '2', '3', '4', '5', '6', '7', '8' };
    static char player = 'X';
    static string resultsFilePath = "gameResults.json";

    static void Main(string[] args)
    {
        int move;
        int turns = 0;
        bool gameWon = false;
        Random rand = new Random();

        GameResults results = LoadResults();

        while (!gameWon && turns < 9)
        {
            Console.Clear();
            PrintBoard();
            if (player == 'X')
            {
                Console.WriteLine("User, enter your move (0-8): ");
                // Input validation
                while (!int.TryParse(Console.ReadLine(), out move) || move < 0 || move > 8 || board[move] == 'X' || board[move] == 'O')
                {
                    Console.WriteLine("Invalid move, try again.");
                }
            }
            else
            {
                // Computer's move
                do
                {
                    move = rand.Next(0, 9);
                } while (board[move] == 'X' || board[move] == 'O');
                Console.WriteLine($"Computer chose position {move}");
            }

            board[move] = player;
            turns++;
            gameWon = CheckWin();
            player = (player == 'X') ? 'O' : 'X';
        }

        Console.Clear();
        PrintBoard();
        if (gameWon)
        {
            if (player == 'X')
            {
                Console.WriteLine("Computer wins!");
                results.Losses++;
            }
            else
            {
                Console.WriteLine("User wins!");
                results.Wins++;
            }
        }
        else
        {
            Console.WriteLine("It's a draw!");
            results.Ties++;
        }

        SaveResults(results);
        DisplayResults(results);
    }

    static void PrintBoard()
    {
        for (int i = 0; i < board.Length; i++)
        {
            if (i % 3 == 0 && i != 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n---|---|---");
            }

            if (i % 3 != 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("|");
            }

            if (board[i] == 'X')
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else if (board[i] == 'O')
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.Write($" {board[i]} ");
        }
        Console.ResetColor();
        Console.WriteLine();
    }

    static bool CheckWin()
    {
        int[,] winPositions = {
            {0, 1, 2}, {3, 4, 5}, {6, 7, 8}, // Rows
            {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, // Columns
            {0, 4, 8}, {2, 4, 6}             // Diagonals
        };

        for (int i = 0; i < winPositions.GetLength(0); i++)
        {
            if (board[winPositions[i, 0]] == board[winPositions[i, 1]] &&
                board[winPositions[i, 1]] == board[winPositions[i, 2]])
            {
                return true;
            }
        }
        return false;
    }

    static GameResults LoadResults()
    {
        if (File.Exists(resultsFilePath))
        {
            string json = File.ReadAllText(resultsFilePath);
            return JsonSerializer.Deserialize<GameResults>(json);
        }
        return new GameResults();
    }

    static void SaveResults(GameResults results)
    {
        string json = JsonSerializer.Serialize(results);
        File.WriteAllText(resultsFilePath, json);
    }

    static void DisplayResults(GameResults results)
    {
        Console.WriteLine("\nGame Results:");
        Console.WriteLine($"Wins: {results.Wins}");
        Console.WriteLine($"Losses: {results.Losses}");
        Console.WriteLine($"Ties: {results.Ties}");
    }
}

class GameResults
{
    public int Wins { get; set; }
    public int Losses { get; set; }
    public int Ties { get; set; }
}
