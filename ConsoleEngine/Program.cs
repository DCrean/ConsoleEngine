using ConsoleEngine.Engine;
using System;
using System.Threading.Tasks;

namespace ConsoleEngine
{
    class Program
    {
        static Display display = new Display(40, 20, '.');
        static Sprite box = new Sprite(2, 2, 5, 5, '#');

        static void Main(string[] args)
        {
            box.Vector = new Force(1, 180);
            display.Sprites.Add(box);

            display.Show();
            Console.ReadLine();
        }

        private static void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}