using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEngine.Engine
{
    class Sprite : Drawable, IAnimated
    {
        private double _x = 0;
        private double _y = 0;
        private double _deltaX = 0;
        private double _deltaY = 0;

        double IAnimated.X { get => _x; set => _x = value; }
        double IAnimated.Y { get => _y; set => _y = value; }
        double IAnimated.DeltaX { get => _deltaX; set => _deltaX = value; }
        double IAnimated.DeltaY { get => _deltaY; set => _deltaY = value; }

        public void Animate()
        {
            _x += _deltaX;
            _y += _deltaY;
        }
    }
}
