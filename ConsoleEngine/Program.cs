using ConsoleEngine.Engine;
using ConsoleEngine.Engine.Drawables;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleEngine
{
    class Program
    {

        static void Main(string[] args)
        {
            TicTacToeGame ttt = new TicTacToeGame();
            ttt.Play();
            Print("Thanks for playing!");
        }

      

        public static void Print(string message)
        {
            Console.WriteLine(message);
        }


        public static void PrintArray(int[] array)
        {
            foreach(int item in array)
            {
                Console.WriteLine(item);
            }
        }
    }
}