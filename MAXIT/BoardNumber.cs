using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAXIT
{
    public static class ValGen
    {
        private static Random random = new Random();
        public static int randomNumber
        {
            get
            {
                return random.Next(-9, 15);
            }
        }

        private static int id = 0;
        public static int ID
        {
            get 
            { 
                return ++id;
            }
        }
        
    }

    public class BoardNumber
    {
        public int Value;
        int ID;
        public bool consumed;
        public bool selected;
        public int X;
        public int Y;

        public BoardNumber(int x, int y)
        {
            ID = ValGen.ID;
            Value = ValGen.randomNumber;
            consumed = false;
            selected = false;
            X = x;
            Y = y;
        }
    }
}
