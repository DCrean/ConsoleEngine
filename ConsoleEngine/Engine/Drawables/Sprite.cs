using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEngine.Engine.Drawables
{
    class Sprite : Drawable, IAnimated
    {
        public Force Vector { get; set; } = new Force();

        public Sprite() { }

        public Sprite(int x, int y, int width, int height, char fillChar)
        {
            Origin = new Point(x, y);
            DisplayArea = new Area(width, height);
            Fill(fillChar);
        }

        public Sprite(Point origin, int width, int height, char fillChar)
        {
            Origin = origin;
            DisplayArea = new Area(width, height);
            Fill(fillChar);
        }

        public Sprite(Point origin, Area area, char fillChar)
        {
            Origin = origin;
            DisplayArea = area;
            Fill(fillChar);
        }

        public void Animate()
        {
            Origin.Translate(Vector.DeltaX, Vector.DeltaY);
        }
    }
}
