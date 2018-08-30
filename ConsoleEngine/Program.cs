using ConsoleEngine.Engine;
using System;

namespace ConsoleEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            Display display = new Display(40, 20 '#');
            display.Show();

            Sprite box = new Sprite();
            box.Resize(3, 3);
            box.Fill('*');


            Sprite box2 = new Sprite();
            box2.Resize(10, 3);
            box2.Fill('6');

            display.Sprites.Add(box);
            display.Sprites.Add(box2);
            

            Console.ReadLine();
        }
    }
}