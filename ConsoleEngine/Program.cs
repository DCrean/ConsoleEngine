using ConsoleEngine.Engine;
using System;

namespace ConsoleEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            Display display = new Display();
            display.Resize(40, 20);
            display.Fill('#');
            display.Show();

            Sprite box = new Sprite();
            box.Resize(3, 3);
            box.Fill('*');
            box.X = 10;
            box.Y = 3;
            box.XSpeed = -.4;
            box.YSpeed = .2;


            Sprite box2 = new Sprite();
            box2.Resize(10, 3);
            box2.Fill('6');
            box2.X = display.Width/2 - box2.Width / 2;
            box2.Y = display.Height/2 - box2.Height / 2;
            box2.XSpeed = .5;
            box2.YSpeed = .05;

            display.Sprites.Add(box);
            display.Sprites.Add(box2);
            

            Console.ReadLine();
        }
    }
}