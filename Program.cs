using System;

class Program
{
    static char[] board = { '0', '1', '2', '3', '4', '5', '6', '7', '8' };
    static char player = 'X';

    static void Main(string[] args)
    {
        int move;
        int turns = 0;
        bool gameWon = false;

        while (!gameWon && turns < 9)
        {
            Console.Clear();
            PrintBoard();
            Console.WriteLine($"Player {player}, enter your move (0-8): ");
            
            // Input validation
            while (!int.TryParse(Console.ReadLine(), out move) || move < 0 || move > 8 || board[move] == 'X' || board[move] == 'O')
            {
                Console.WriteLine("Invalid move, try again.");
            }

            board[move] = player;
            turns++;
            gameWon = CheckWin();
            player = (player == 'X') ? 'O' : 'X';
        }

        Console.Clear();
        PrintBoard();
        Console.WriteLine(gameWon ? $"Player {(player == 'X' ? 'O' : 'X')} wins!" : "It's a draw!");
    }

    static void PrintBoard()
    {
        Console.WriteLine(" {0} | {1} | {2} ", board[0], board[1], board[2]);
        Console.WriteLine("---|---|---");
        Console.WriteLine(" {0} | {1} | {2} ", board[3], board[4], board[5]);
        Console.WriteLine("---|---|---");
        Console.WriteLine(" {0} | {1} | {2} ", board[6], board[7], board[8]);
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
}
