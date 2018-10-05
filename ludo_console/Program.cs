using System;
using System.Collections.Generic;
using static System.Console;

namespace ludo_console
{
    class Program
    {
        protected static Random rng = new Random();
        protected static BoardSpace[] board = new BoardSpace[52];
        protected static List<team> Teams = new List<team>();

        static void Main(string[] args)
        {
            //The teams
            List<string> test = new List<string> { "red", "blue","green", "yellow" };

            for (int i = 0; i < 52; i++)
            {
                if(i%13 == 2 || i%13 == 10)//If globe
                {
                    board[i] = new BoardSpace(i, "globus");
                }
                else if (i%13 == 7 || i%13 == 0)//If star
                {
                    board[i] = new BoardSpace(i, "star");
                }
                else //Normal
                {
                    board[i] = new BoardSpace(i,"none");
                }
            }

            //Get numbers
            WriteLine("Please write the number of human players");


            Int32.TryParse(ReadLine(), out int players);
            while(players > 4 || players < 0)
            {
                WriteLine("Please write a number between 1 and 4");
                Int32.TryParse(ReadLine(), out players);

            }

            for (int i = 0; i < players; i++)
            {
                WriteLine("Player {0}, please select your color", i + 1);
                foreach (string a in test)
                {
                    Console.WriteLine(a);
                }
                int.TryParse(Console.ReadLine(), out int temp);
                Teams[i] = new team(test[temp - 1], true,i);
                test.RemoveAt(temp - 1);
            }
            WriteLine("How many ai's do you want? you can chose between {0} and {1}", 0, 4 - players);

            Int32.TryParse(ReadLine(), out int AIPlayers);
            while (AIPlayers > 4-players || AIPlayers < 0)
            {
                WriteLine("Please write a number between 1 and {0}", 4 - players);
                Int32.TryParse(ReadLine(), out AIPlayers);
            }

            for (int i = 0; i < AIPlayers; i++)
            {
                Teams[players] = new team(test[0], false, players);
                test.RemoveAt(0);
                players++;
            }

            int counter = 0;
            int points = 0;
            while (points < 4)
            {
                
                if(Teams[counter].Human)//Seee if it's a player
                {
                    Turn(Teams[counter], counter);
                    if(Teams[counter].Points == 4)
                    {
                        Teams.RemoveAt(counter);
                        points++;
                        players--;
                    }
                }
                else
                {
                    //AITurn(pieces[k]);
                }
                counter= ++counter % players;
                

                //Check if it's the players turn or the ai
            }
        }

        /*
        public static bool count(int[] points)
        {
            foreach(int a in points)
            {
                if(a == 4)
                {
                    return false;
                }
            }
            return true;
        }
        */

        //Used to spawn pieces at the start
        public static bool StartPiece()
        {
            WriteLine("Roll three times to spawn a piece");
            for (int b = 0; b < 3; b++)
            {
                if (rng.Next(1, 7) == 5)
                {
                    WriteLine("You rolled a globe. You can now spawn a piece");
                    return false;
                }
                else
                {
                    WriteLine("Trying again");
                    //Sleep due to random using system clock and making sure it's updated
                    System.Threading.Thread.Sleep(30);
                }

            }
            return true;
        }

        static void Turn(team player, int PlayerIndex)
        {
            if (player.AllHome())//Check if there are no pieces on the board
            {
                if (StartPiece())//No globe rolled, stops the function
                {
                    return;
                }
                else
                {
                    player.Spawn();
                }
            }

            int roll = rng.Next(1, 7);
            //Globe 5, stjerne 3
            //Star move
            if(roll == 3)
            {
                WriteLine("Please choose the piece you want to move to a star");
                PrintPieces(player);
                int.TryParse(ReadLine(), out int id);

                int PieceIndex = player.Pieces[id].Index;

                if (PieceIndex % 13 <= 7 && PieceIndex > 0)
                {
                    Move(player.Pieces[id], 7 - (PlayerIndex % 13));
                }
                else
                {
                    Move(player.Pieces[id], 13 - (PlayerIndex % 13));
                }   
                //Star every 13. step. offset for first is +8
                // (i % 13 == 2 || i % 13 == 10)globe

                // (i % 13 == 7 || i % 13 == 0) star
            }
            //Globe move
            else if(roll == 5)
            {
               WriteLine("choose to spawn a new piece or move a piece to a globe");
                if (ReadLine() == "spawn")//Checks if player wants to spawn a piece
                {
                    player.Spawn();
                } 
                else
                {
                    WriteLine("Move a piece to a globe");
                    PrintPieces(player);
                    //Mangler defination på globe
                    int.TryParse(ReadLine(), out int id);
                    int PieceIndex = player.Pieces[id].Index;
                    if(PieceIndex%13 <=10 && PieceIndex > 2)
                    {
                        Move(player.Pieces[id], 10 - (PlayerIndex%13));
                    }
                    else if(PieceIndex % 13 > 10)
                    {
                        Move(player.Pieces[id], 15 - (PlayerIndex % 13));
                    }
                    else
                    {
                        Move(player.Pieces[id], 2 - (PlayerIndex % 13));
                    }

                }

            }
                  //Normal move
            else
            {
                WriteLine("Please write the piece you want to move " + roll + " steps");
                PrintPieces(player);
                int.TryParse(ReadLine(), out int id);

                Move(player.Pieces[id], roll);
            }
            
        }


        public static void Move(piece piece,int roll)
        {
            int index = piece.Index+roll;
            //Removes the piece on the board.
            board[index].removePiece();
            //Checks if the board is empty
            if(board[index].Count > 0 || board[index].BoardPiece.Color == piece.Color)
            {
                board[index].AddPiece(piece);
            }
            else
            {
                if(board[index].Special == "globe")
                {
                    piece.Home = true;
                }
                else if(board[index].Count >= 2)
                {
                    piece.Home = true;
                }
                else
                {
                    board[index].BoardPiece.Home = true;
                }
            }
        }

        public static void PrintPieces(team Team)
        {
            foreach(piece a in Team.Pieces)
            {
                if (a.Home == false)
                {
                    WriteLine(a.Id);
                }
            }
        }

    }
}
