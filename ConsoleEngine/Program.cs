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
        }

      

        private static void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}