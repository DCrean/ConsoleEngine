using ConsoleEngine.Engine;
using System;

namespace ConsoleEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            Display display = new Display(40, 20, '.');

            Sprite box = new Sprite(2,2,5,5,'#');

            display.Sprites.Add(box);

            display.Show();

            Console.ReadLine();
        }
    }
}