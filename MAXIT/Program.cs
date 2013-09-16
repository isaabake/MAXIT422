using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAXIT
{
    class Program
    {
        static void Main(string[] args)
        {
            test t = new test();
            Console.Write("\nTests finished. Press any key to continue...");
            Console.ReadKey();
            Console.Clear();

            MAXIT maxit = new MAXIT();
            maxit.Run();

            
            



        }

        


    }
}
