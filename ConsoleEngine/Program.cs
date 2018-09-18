using ConsoleEngine.Engine;
using ConsoleEngine.Engine.Drawables;
using System;
using System.Threading.Tasks;

namespace ConsoleEngine
{
    class Program
    {
        static Display display = new Display(40, 20, '.');
        static Sprite box = new Sprite(2, 2, 5, 5, '#');
        static Glyph glyph = new Glyph('#');

        static void Main(string[] args)
        {
            glyph.ForegroundColor = ConsoleColor.DarkGreen;
            box.Fill(glyph);

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