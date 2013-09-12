using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAXIT
{
    public class test
    {
        MAXIT maxit = new MAXIT();

        public test()
        {
            test1();
            test2();
            test3();
            test4();

        }

        void test1()
        {
            BoardNumber[,] board = new BoardNumber[2,2];
            board[0, 0] = new BoardNumber(0, 0);           
            board[0, 1] = new BoardNumber(0, 1);
            board[1, 0] = new BoardNumber(0, 0);
            board[1, 1] = new BoardNumber(0, 0);

            board[0, 1].selected = true;
            board[0, 0].consumed = true;

            maxit.PrintBoard(board);

            Console.WriteLine("test1 finished successfully.");
        }

        void test2()
        {
            BoardNumber[,] board = new BoardNumber[2,2];

            maxit.PopulateBoard(board);

            if (board[0, 0] == null)
            {
                Console.WriteLine("test2 failed at node [0,0] == null.");
                return;
            }
            else if (board[0, 1] == null)
            {
                Console.WriteLine("test2 failed at node [0,0] == null.");
                return;
            }
            else if (board[1, 0] == null)
            {
                Console.WriteLine("test2 failed at node [0,0] == null.");
                return;
            }
            else if (board[1, 1] == null)
            {
                Console.WriteLine("test2 failed at node [0,0] == null.");
                return;
            }
            else
            {
                Console.WriteLine("test2 finished successfully.");
            }
        }

        void test3()
        {
            BoardNumber[,] board = new BoardNumber[2, 2];
            maxit.BoardDimension = 2;
            board[0, 0] = new BoardNumber(0, 0);
            board[0, 1] = new BoardNumber(0, 1);
            board[1, 0] = new BoardNumber(1, 0);
            board[1, 1] = new BoardNumber(1, 1);

            maxit.selectedNode = board[0, 0];
            board[0, 0].selected = true;

            board[0, 0].Value = 0;
            board[0, 1].Value = 1;
            board[1, 0].Value = 2;
            board[1, 1].Value = 0;

            maxit.PrintBoard(board);

            BoardNumber b = maxit.FindLargestAvailable(board);

            if (b.Value != 2)
            {
                Console.WriteLine("test3 failed finding node.value = 2.");
            }

            board[0, 0].consumed = true;
            board[0, 1].consumed = true;
            board[1, 0].consumed = true;
            board[1, 1].consumed = true;

            b = maxit.FindLargestAvailable(board);

            if (b != null)
            {
                Console.WriteLine("test3 failed. FindLargestAvailable should have returned null.");
            }

            Console.WriteLine("test3 finished successfully.");
        }

        void test4()
        {
            BoardNumber[,] board = new BoardNumber[2, 2];
            maxit.BoardDimension = 2;
            board[0, 0] = new BoardNumber(0, 0);
            board[0, 1] = new BoardNumber(0, 1);
            board[1, 0] = new BoardNumber(1, 0);
            board[1, 1] = new BoardNumber(1, 1);

            maxit.selectedNode = board[0, 0];
            board[0, 0].selected = true;

            board[0, 1].consumed = true;
            board[1, 0].consumed = true;

            maxit.PrintBoard(board);

            if (!maxit.isGameOver(board))
            {
                Console.WriteLine("test4 failed, isGameOver returned false with no moves left.");
            }

            board[0, 1].consumed = false;

            maxit.PrintBoard(board);

            if (maxit.isGameOver(board))
            {
                Console.WriteLine("test4 failed, isGameOver returned true with one move left.");
            }

            Console.WriteLine("test4 finished successfully.");

        }



    }
}
