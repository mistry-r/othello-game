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
            Console.Clear( );
            WriteLine( );
            WriteLine( " Welcome to Othello!" );
            WriteLine( );
        }
        
        // Collect a player name or default to form the player record.
        
        static Player NewPlayer( string colour, string symbol, string defaultName )
        {
            Write( " Type the {0} disc ({1}) player name [or <Enter> for '{2}']: ", 
                colour, symbol, defaultName );
            string name = ReadLine( ) !;
            if( name.Length == 0 ) name = defaultName;
            return new Player( colour, symbol, name );
        }
        
        // Determine which player goes first or default.
        
        static int GetFirstTurn( Player[ ] players, int defaultFirst )
        {
            while( true )
            {
                Write( " Choose who will play first [or <Enter> for {0}/{1}/{2}]: ", 
                    players[ defaultFirst ].Colour,
                    players[ defaultFirst ].Symbol,
                    players[ defaultFirst ].Name );
                string response = ReadLine( ) !;
                if( response.Length  == 0 ) return defaultFirst;
                for( int i = 0; i < players.Length; i ++ )
                {
                    if( players[ i ].Colour == response ) return i;
                    if( players[ i ].Symbol == response ) return i;
                    if( players[ i ].Name   == response ) return i;
                }
                WriteLine( " Invalid response, please try again." );
            }
        }
        
        // Get a board size (between 4 and 26 and even) or default, for one direction.
        
        static int GetBoardSize( string direction, int defaultSize )
        {
            while( true )
            {
                Write( " Enter board {0} (4 to 26, even) [or <Enter> for {1}]: ", 
                    direction, defaultSize );
                string response = ReadLine( ) !;
                if( response.Length == 0 ) return defaultSize;
                int size = int.Parse( response );
                if( size >= 4 && size <= 26 && size % 2 == 0 ) return size;
                WriteLine( " Invalid response, please try again." );
            }
        }
        
        // Get a move from a player.
        
        static string GetMove( Player player )
        {
            WriteLine( );
            WriteLine( " Turn is {0} disc ({1}) player: {2}", 
                player.Colour, player.Symbol, player.Name );
            WriteLine( " Pick a cell by its row then column names (like 'bc') to play there." );
            WriteLine( " Use 'skip' to give up your turn. Use 'quit' to end the game." );
            Write( " Enter your choice: " );
            return ReadLine( ) !;
        }
        
        // Try to make a move. Return true if it worked.
        
        static bool TryMove( string[ , ] board, Player player, string move )
        {
            const string empty = " "; // board content for an empty cell
            
            if( move == "skip" ) return true; // do nothing so turn changes
            
            if( move.Length != 2 )
            {
                WriteLine( );
                Write( " The move should be two characters," );
                WriteLine( " one for the row and one for the column. " );
                return false;
            }
            
            int row = IndexAtLetter( move.Substring( 0, 1 ) );
            if( row < 0 || row >= board.GetLength( 0 ) )
            {
                WriteLine( );
                WriteLine( " The first character must be a row in the game board." );
                return false;
            }
            
            int col = IndexAtLetter( move.Substring( 1, 1 ) );
            if( col < 0 || col >= board.GetLength( 1 ) )
            {
                WriteLine( );
                WriteLine( " The second character must be a column in the game board." );
                return false;
            }
            
            if( board[ row, col ] != empty )
            {
                WriteLine( );
                WriteLine( " The cell you chose is occupied." );
                return false;
            }
            
            // Try to make the move. It's valid if something flips in any direction.
            
            bool valid = false;
            
            valid = TryDirection( board, player, row, -1, col,  0 ) || valid; // N
            valid = TryDirection( board, player, row, -1, col,  1 ) || valid; // NE
            valid = TryDirection( board, player, row,  0, col,  1 ) || valid; // E
            valid = TryDirection( board, player, row,  1, col,  1 ) || valid; // SE
            valid = TryDirection( board, player, row,  1, col,  0 ) || valid; // S
            valid = TryDirection( board, player, row,  1, col, -1 ) || valid; // SW
            valid = TryDirection( board, player, row,  0, col, -1 ) || valid; // W
            valid = TryDirection( board, player, row, -1, col, -1 ) || valid; // NW

            System.Threading.Thread.Sleep( 1000 /* ms */ );
            return valid;
        }
        
        // Do the flips along a direction specified by the row and column delta for one step.
        
        static bool TryDirection( string[ , ] board, Player player,
            int moveRow, int deltaRow, int moveCol, int deltaCol )
        {
            const string empty = " "; // board content for an empty cell
            
            // Check that the first neighbouring cell is still on the board.
            
            int row = moveRow + deltaRow;
            int col = moveCol + deltaCol;
            
            if( row < 0 || row >= board.GetLength( 0 ) ) return false;
            if( col < 0 || col >= board.GetLength( 1 ) ) return false;
            
            // Check that the first neighbouring cell is not empty.
            
            if( board[ row, col ] == empty ) return false;
            
            // Check that the first neighbouring cell is an opponent's colour.
            
            if( board[ row, col ] == player.Symbol ) return false;
            
            // Check that the line continues to the player's colour.
            // Count the discs which will be changed.
            
            int count = 1; // if we got this far, there's one opponent disc
            bool found = false; // set true if we find the player's colour
            
            while( ! found  )
            {
                // Check that the next cell is still on the board.
                
                row = row + deltaRow;
                col = col + deltaCol;
                
                if( row < 0 || row >= board.GetLength( 0 ) ) return false;
                if( col < 0 || col >= board.GetLength( 1 ) ) return false;
                
                // Check that the next cell is not empty.
                
                if( board[ row, col ] == empty ) return false;
                
                // Check that the next cell holds the player's colour.
                
                if( board[ row, col ] == player.Symbol ) found = true;
                else count ++;
            }
            
            // Do the flips.
            
            board[ moveRow, moveCol ] = player.Symbol;
            Welcome( );
            DisplayBoard( board );
            
            row = moveRow;
            col = moveCol;
            
            for( int i = 0; i < count; i ++ )
            {
                row = row + deltaRow;
                col = col + deltaCol;
                System.Threading.Thread.Sleep( 1000 /* ms */ );
                board[ row, col ] = player.Symbol;
                Welcome( );
                DisplayBoard( board );
            }
            
            return true;
        }
        
        // Count the discs to find the score for a player.
        
        static int GetScore( string[ , ] board, Player player )
        {
            int result = 0;
            foreach( string s in board )
            {
                if( s == player.Symbol ) result ++;
            }
            return result;
        }
        
        // Display a line of scores for all players.
        
        static void DisplayScores( string[ , ] board, Player[ ] players )
        {
            WriteLine( );
            Write( " Scores:" );
            bool first = true;
            foreach( Player p in players )
            {
                if( ! first ) Write( "," );
                else first = false;
                Write( " {0} {1}", p.Name, GetScore( board, p ) );
            }
            WriteLine( );
        }
        
        // Display winner(s) and categorize their win over the defeated player(s).
        
        static void DisplayWinners( string[ , ] board, Player[ ] players )
        {
            int[ ] scores = new int[ players.Length ];
            for( int i = 0; i < players.Length; i ++ )
            {
                scores[ i ] = GetScore( board, players[ i ] );
            }
            
            int maxScore = scores[ 0 ];
            foreach( int score in scores )
            {
                if( score > maxScore ) maxScore = score;
            }
            
            bool isTie = true;
            foreach( int score in scores)
            {
                if( score < maxScore ) isTie = false;
            }
            
            WriteLine( );
            if( isTie ) WriteLine( " Game is a tie!" );
            else
            {
                Write( " Winner(s):" );
                bool first = true;
                for( int i = 0; i < players.Length; i ++ )
                {
                    if( scores[ i ] == maxScore ) 
                    {
                        if( ! first ) Write( "," );
                        else first = false;
                        Write( " {0}", players[ i ].Name );
                    }
                }
                WriteLine( );
                
                for( int i = 0; i < players.Length; i ++ )
                {
                    int diff = maxScore - scores[ i ];
                    if( diff > 0 )
                    {
                        Write( " Defeated {0} by {1}", players[ i ].Name, diff );
                        switch( diff * 64d / board.Length ) // relative to 8 x 8 game
                        {
                            case >  1 and <= 11:
                                WriteLine( " in a close game." );
                                break;
                            case > 11 and <= 25:
                                WriteLine( " in a hot game." );
                                break;
                            case > 25 and <= 39:
                                WriteLine( " in a fight game." );
                                break;
                            case > 39 and <= 53:
                                WriteLine( " in a walkaway game." );
                                break;
                            case > 53 and <= 64:
                                WriteLine( " in a perfect game." );
                                break;
                            default:
                                WriteLine( "." );
                                break;
                        }
                    }
                }
            }
        }
        
        static void Main( )
        {
            // Set up the players and game.
            
            Welcome( );
            
            Player[ ] players = new Player[ ] 
            {
                NewPlayer( colour: "black", symbol: "X", defaultName: "Black" ),
                NewPlayer( colour: "white", symbol: "O", defaultName: "White" ),
            };
            
            int turn = GetFirstTurn( players, defaultFirst: 0 );
           
            int rows = GetBoardSize( direction: "rows",    defaultSize: 8 );
            int cols = GetBoardSize( direction: "columns", defaultSize: 8 );
            
            string[ , ] game = NewBoard( rows, cols );
            
            // Play the game.
            
            bool gameOver = false;
            while( ! gameOver )
            {
                Welcome( );
                DisplayBoard( game ); 
                DisplayScores( game, players );
                
                string move = GetMove( players[ turn ] );
                if( move == "quit" ) gameOver = true;
                else
                {
                    bool madeMove = TryMove( game, players[ turn ], move );
                    if( madeMove ) turn = ( turn + 1 ) % players.Length;
                    else 
                    {
                        Write( " Your choice didn't work!" );
                        Write( " Press <Enter> to try again." );
                        ReadLine( ); 
                    }
                }
            }
            
            // Show fhe final results.
            
            DisplayWinners( game, players );
            WriteLine( );
        }
    }
}
