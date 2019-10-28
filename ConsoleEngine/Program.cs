using ConsoleEngine.Engine;
using ConsoleEngine.Engine.Drawables;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleEngine
{
    class Program
    {
        static Display display = new Display(40, 20, new Glyph('.'));
        static Sprite box = new Sprite(2, 2, 5, 5, '#');
        static Glyph glyph = new Glyph('#');

        static void Main(string[] args)
        {
            glyph.ForegroundColor = ConsoleColor.DarkGreen;
            box.Fill(glyph);
            box.Vector = new Force(1,90);
            display.Sprites.Add(box);
            display.Show();

            Thread.Sleep(1000);
            box.Fill('*');
            box.Vector.add(new Force(.5, 200));

            Console.ReadLine();
        }

        private static void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}