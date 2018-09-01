using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEngine.Engine
{
    class Sprite : Drawable, IAnimated
    {
        public Force Vector { get; set; }

        public Sprite() { }

        public Sprite(int x, int y, int width, int height, char fillChar)
        {
            DisplayArea = new Area(x, y, width, height);
            Fill(fillChar);
        }

        public Sprite(Point origin, int width, int height, char fillChar)
        {
            DisplayArea = new Area(origin, width, height);
            Fill(fillChar);
        }

        public Sprite(Area area, char fillChar)
        {
            DisplayArea = area;
            Fill(fillChar);
        }

        public void Animate()
        {
            DisplayArea.Origin.Translate(Vector.DeltaX, Vector.DeltaY);
        }
    }
}
