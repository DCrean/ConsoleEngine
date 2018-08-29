using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEngine.Engine
{
    class Sprite : Drawable
    {
        public double X = 0;
        public double Y = 0;
        public double XSpeed = 0;
        public double YSpeed = 0;

        public void Animate()
        {
            X += XSpeed;
            Y += YSpeed;
        }
    }
}
