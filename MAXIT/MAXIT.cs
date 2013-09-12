using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MAXIT
{
    public class MAXIT
    {
        BoardNumber[,] board;
        BoardNumber selectedNode;
        int BoardDimension;
        int PlayerScore = 0;
        int ComputerScore = 0;

        public void PrintBoard(BoardNumber[,] board)
        {
            int dimX = board.GetLength(0);
            int dimY = board.GetLength(1);


            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Write("\t");
            for (int len = 0; len < board.GetLength(1); len += 1)
            {
                Console.Write("\t");
            }
            Console.WriteLine();

            for (int y = 0; y < board.GetLength(0); y += 1)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.Write("\t");
                for (int x = 0; x < board.GetLength(1); x += 1)
                {
                    if (board[x, y].selected)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("{0}", board[x, y].Value);
                        Console.ResetColor();
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.Write("\t");
                        Console.ResetColor();
                    }
                    else if (board[x, y].consumed)
                    {

                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("{0}", board[x, y].Value);
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.Write("\t");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("{0}\t", board[x, y].Value);
                    }
                }
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine();
                Console.Write("\t");
                for (int len = 0; len < board.GetLength(1); len += 1)
                {
                    Console.Write("\t");
                }
                Console.WriteLine();
                Console.ResetColor();
            }
        }

        public void PopulateBoard(BoardNumber[,] board)
        {
            for (int x = 0; x < board.GetLength(0); x += 1)
            {
                for (int y = 0; y < board.GetLength(1); y += 1)
                {
                    board[x, y] = new BoardNumber(x, y);
                }
            }
        }

        //bug here
        public BoardNumber FindLargestAvailable(BoardNumber[,] board)
        {
            BoardNumber LargestNodeAvailable = board[selectedNode.X, 0];

            for (int i = 0; i < BoardDimension; i++)
            {
                BoardNumber temp = board[selectedNode.X, i];
                if (!temp.selected && !temp.consumed && (temp.Value > LargestNodeAvailable.Value || LargestNodeAvailable.consumed))
                {
                    LargestNodeAvailable = temp;
                }

                temp = board[i, selectedNode.Y];
                if (!temp.selected && !temp.consumed && (temp.Value > LargestNodeAvailable.Value || LargestNodeAvailable.consumed))
                {
                    LargestNodeAvailable = temp;
                }
            }

            if (!LargestNodeAvailable.consumed && !LargestNodeAvailable.selected)
            {
                return LargestNodeAvailable;
            }
            else
            {
                return null;
            }
        }

        public bool isGameOver(BoardNumber[,] board)
        {
            bool tf = true;
            for (int i = 0; i < BoardDimension; i++)
            {
                if (!board[selectedNode.X, i].consumed || !board[i, selectedNode.Y].consumed)
                {
                    tf = false;
                }
            }
            return tf;
        }

        public void Run()
        {
            char cDim = '\0';

            Random randomNumber = new Random();

            Console.WriteLine("Welcome to MAXIT\n");
            Console.Write("Please enter board dimension: ");

            while (!int.TryParse(cDim.ToString(), out BoardDimension))
            {
                cDim = Console.ReadKey().KeyChar;
            }
            board = new BoardNumber[BoardDimension, BoardDimension];
            PopulateBoard(board);

            selectedNode = board[randomNumber.Next(0, BoardDimension - 1), randomNumber.Next(0, BoardDimension - 1)];
            selectedNode.selected = true;

            while (!isGameOver(board))
            {
                Console.Clear();

                PrintBoard(board);
                Console.WriteLine("Player: {0}    Computer: {1}\n", PlayerScore, ComputerScore);

                Console.Write("Traverse X or Y? ");
                string direction = string.Empty;
                direction = Console.ReadLine();
                direction = direction.ToLower();
                if (direction != "x" && direction != "y")
                {
                    Console.WriteLine("\nPlease enter X or Y.");
                    Thread.Sleep(1500);
                    continue;
                }

                Console.Write("\nHow far? ");
                int distance = 0;
                string distanceString = string.Empty;
                distanceString = Console.ReadLine();
                if (!int.TryParse(distanceString, out distance))
                {
                    Console.WriteLine("\nPlease enter a valid number.");
                    Thread.Sleep(1500);
                    continue;
                }
                else if (direction == "x" && (selectedNode.X + distance > BoardDimension - 1 || selectedNode.X + distance < 0))
                {
                    Console.WriteLine("\nDistance out of bounds.");
                    Thread.Sleep(1500);
                    continue;
                }
                else if (direction == "y" && (selectedNode.Y + distance > BoardDimension - 1 || selectedNode.Y + distance < 0))
                {
                    Console.WriteLine("\nDistance out of bounds.");
                    Thread.Sleep(1500);
                    continue;
                }
                ////////


                BoardNumber temp;
                if (direction == "x")
                {
                    temp = board[selectedNode.X + distance, selectedNode.Y];
                }
                else
                {
                    temp = board[selectedNode.X, selectedNode.Y + distance];
                }

                if (temp.selected == true)
                {
                    Console.WriteLine("\nThis node is already selected.");
                    Thread.Sleep(1500);
                    continue;
                }
                else if (temp.consumed == true)
                {
                    Console.WriteLine("\nThis node is already consumed.");
                    Thread.Sleep(1500);
                    continue;
                }


                selectedNode.selected = false;
                selectedNode = temp;
                selectedNode.selected = true;
                selectedNode.consumed = true;
                PlayerScore += selectedNode.Value;

                BoardNumber nextMove = FindLargestAvailable(board);
                if (nextMove != null)
                {
                    Console.WriteLine("Computer Player's turn...");
                    selectedNode.selected = false;
                    selectedNode = nextMove;
                    selectedNode.selected = true;
                    selectedNode.consumed = true;
                    ComputerScore += selectedNode.Value;
                }
            }

            Console.WriteLine("****GAME OVER****");
            if (ComputerScore > PlayerScore)
            {
                Console.WriteLine("Computer player wins with {0} points!", ComputerScore);
            }

            else if (PlayerScore > ComputerScore)
            {
                Console.WriteLine("You with {0} points!", PlayerScore);
            }

            else
            {
                Console.WriteLine("Tie game at {0} points!", PlayerScore);
            }
        }
    }
}
