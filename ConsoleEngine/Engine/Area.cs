using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEngine.Engine
{
    class Area
    {
        private int _width = 0;
        private int _height = 0;

        public int Width { get => _width; set => _width = value; }
        public int Height { get => _height; set => _height = value; }
        public int Size { get => _width * _height; }

        public Area() { }

        public Area(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public Area(double x1, double y1, double x2, double y2)
        {
            _width = (int)Math.Abs(x1 - x2);
            _height = (int)Math.Abs(y1 - y2);
        }

        public Area(Point first, Point second)
        {
            _width = (int)Math.Abs(first.X - second.X);
            _height = (int)Math.Abs(first.Y - second.Y);
        }
    }
}
