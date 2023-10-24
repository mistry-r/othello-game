# Othello Game

This is a simple implementation of the classic game Othello (also known as Reversi) in C#.

## How to Play

Othello is a two-player strategy board game played on an 8x8 grid. Players take turns placing their discs on the board. A disc is placed on an empty cell, and any opponent's discs that are trapped between the newly placed disc and another disc of the current player's color are flipped to the current player's color.

The game continues until there are no more legal moves left, and the player with the most discs of their color on the board wins.

## Code Overview

The code is structured as a C# console application and is organized into the following key components:

- `Player`: A record that represents a player in the game. It includes properties for the player's color, symbol, and name.

- `Welcome()`: A function that clears the console and displays a welcome message at the top of the screen.

- `NewPlayer()`: Collects a player's name or uses a default name to create a player record.

- `GetFirstTurn()`: Determines which player goes first or defaults to the first player.

- `GetBoardSize()`: Gets the board size (between 4 and 26, even) for one direction.

- `GetMove()`: Prompts a player to enter their move (cell location or special commands like "skip" or "quit").

- `TryMove()`: Attempts to make a move on the board and returns `true` if it's a valid move.

- `TryDirection()`: Handles the logic for flipping discs in a specified direction.

- `GetScore()`: Counts the number of discs of a player's color to calculate their score.

- `DisplayScores()`: Displays the scores for all players.

- `DisplayWinners()`: Determines and displays the winner(s) and the nature of their win over the defeated player(s).

- `Main()`: The entry point of the program, where the game is set up and played, and the final results are displayed.

## How to Run

To run the game, simply compile and execute the C# program. Follow the prompts to set up the game and make your moves. The game will continue until it's over, and the winner(s) and the nature of their victory will be displayed.

Have fun playing Othello!
