using ConsoleEngine.Engine;
using ConsoleEngine.Engine.Drawables;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleEngine
{
    class Program
    {
        static readonly Display display = new Display(20, 20, new Glyph('.'));
        static readonly Sprite box = new Sprite(2, 2, 1, 1, '#');
        static readonly Glyph glyph = new Glyph('#');

        static void Main(string[] args)
        {
            bool isRunning = true;
            Force up = new Force(.5, 0);
            Force down = new Force(.5, 180);
            Force left = new Force(.5, 270);
            Force right = new Force(.5, 90);
            glyph.ForegroundColor = ConsoleColor.DarkGreen;
            box.Fill(glyph);
            display.Sprites.Add(box);
            display.Show();

            do
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        box.Vector = up;
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        box.Vector = left;
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        box.Vector = down;
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        box.Vector = right;
                        break;
                    case ConsoleKey.Escape:
                        isRunning = false;
                        break;
                    default:
                        break;
                }
            } while (isRunning);

            display.Hide();
            Print("Bye!");
            Console.ReadLine();
        }


        private static void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}