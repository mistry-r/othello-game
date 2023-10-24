#nullable enable
using System;
using static System.Console;

namespace Bme121
{
    record Player( string Colour, string Symbol, string Name );
    
    static partial class Program
    {
        // Display common text for the top of the screen.
        
        static void Welcome( )
        {
            
        }
        
        // Collect a player name or default to form the player record.
        
        static Player NewPlayer( string colour, string symbol, string defaultName )
        {
            
        }
        
        // Determine which player goes first or default.
        
        static int GetFirstTurn( Player[ ] players, int defaultFirst )
        {
            
        }
        
        // Get a board size (between 4 and 26 and even) or default, for one direction.
        
        static int GetBoardSize( string direction, int defaultSize )
        {
            
        }
        
        // Get a move from a player.
        
        static string GetMove( Player player )
        {
            
        }
        
        // Try to make a move. Return true if it worked.
        
        static bool TryMove( string[ , ] board, Player player, string move )
        {
            
        }
        
        // Do the flips along a direction specified by the row and column delta for one step.
        
        static bool TryDirection( string[ , ] board, Player player,
            int moveRow, int deltaRow, int moveCol, int deltaCol )
        {
            
        }
        
        // Count the discs to find the score for a player.
        
        static int GetScore( string[ , ] board, Player player )
        {
            
        }
        
        // Display a line of scores for all players.
        
        static void DisplayScores( string[ , ] board, Player[ ] players )
        {
            
        }
        
        // Display winner(s) and categorize their win over the defeated player(s).
        
        static void DisplayWinners( string[ , ] board, Player[ ] players )
        {
            
        }
        
        static void Main( )
        {
            // Set up the players and game.
                               
            // Play the game.
                                    
            // Show fhe final results.
                        
        }
    }
}
