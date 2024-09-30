# Tic-Tac-Toe Game

This is a console-based Tic-Tac-Toe game implemented in C#. The game allows a user to play against a computer opponent. The user can choose to play against a random opponent or a smart opponent using the Minimax algorithm.

## Ways to Improve the Game Without Overcomplicating the Code

1. ~~Input Validation and Error Handling:~~
   - ~~Improve input validation to handle non-integer inputs gracefully.~~
   - ~~Provide more user-friendly error messages.~~
2. ~~Game Restart Option:~~
   - ~~Add an option to restart the game after it ends, without having to restart the application.~~
3. ~~Enhanced User Interface:~~
   - ~~Improve the visual representation of the game board, such as adding borders or better spacing.~~
   - ~~Use different colors for different game states (e.g., win, draw).~~
4. Player Names:
   - Allow users to enter their names and display them during the game and in the results.
5. Sound Effects:
   - Add simple sound effects for moves, wins, and draws to make the game more engaging.
6. Game Timer:
   - Add a timer to track how long each game takes and display it at the end.
7. Statistics:
   - Track and display additional statistics, such as the number of moves made by each player.
8. ~~Refactor Code for Readability:~~
    - ~~Break down large methods into smaller, more manageable methods.~~
    - ~~Use more descriptive variable names.~~

## Features

- Play against a computer opponent.
- Option to play against a smart opponent using the Minimax algorithm.
- Game results (wins, losses, ties) are stored in a JSON file.
- Color-coded game board for better visual representation.

## How to Run

1. **Clone the repository**:

   ```sh
   git clone https://github.com/jlchafardet/tictactoe-csharp.git
   cd tictactoe-csharp
   ```

2. **Build and run the application**:

   ```sh
   dotnet run
   ```

3. **Follow the on-screen instructions** to play the game.

## Game Instructions

- The game board is represented by a 3x3 grid with positions numbered from 0 to 8.
- The user plays as 'X' and the computer plays as 'O'.
- The user is prompted to enter a move by specifying a position number (0-8).
- The game alternates turns between the user and the computer.
- The game ends when there is a win or a draw.
- The game results are displayed and stored in a JSON file.

## Code Overview

### Main Program

The main program is implemented in `Program.cs`. It contains the game logic, input handling, and result storage.

#### Variables

- `board`: An array representing the game board.
- `player`: A character representing the current player ('X' for user, 'O' for computer).
- `resultsFilePath`: The file path where game results are stored.
- `smartOpponent`: A boolean flag indicating if the user wants to play against a smart opponent.

#### Methods

- `Main`: The entry point of the program. Handles the game loop and user input.
- `PrintBoard`: Prints the current state of the game board with color coding.
- `CheckWin`: Checks if the game is won by any player.
- `LoadResults`: Loads game results from a JSON file.
- `SaveResults`: Saves game results to a JSON file.
- `DisplayResults`: Displays the game results.
- `GetBestMove`: Gets the best move for the computer using the Minimax algorithm.
- `Minimax`: The Minimax algorithm to evaluate the best move.

### GameResults Class

The `GameResults` class represents the game results and is used for storing and retrieving results from a JSON file.

#### Properties

- `Wins`: The number of wins by the user.
- `Losses`: The number of losses by the user.
- `Ties`: The number of ties.

## Example Output

[https://i.imgur.com/mQ6sl1D.png]

```
User wins!

Game Results:
Wins: 1
Losses: 0
Ties: 0
```

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any improvements or bug fixes.

## Contact

For any questions or feedback, please contact [Jose Chafardet] at [jose.chafardet@icloud.com].
