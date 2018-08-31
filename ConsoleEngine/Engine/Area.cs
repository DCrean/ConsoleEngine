using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEngine.Engine
{
    class Area
    {
        private Point _origin = new Point(0, 0);
        private int _width = 0;
        private int _height = 0;

        public int Width { get => _width; set => _width = value; }
        public int Height { get => _height; set => _height = value; }
        public Point Origin { get => _origin; set => _origin = value; }
        public int Size { get => _width * _height; }

        public Area() { }

        public Area(int x, int y, int width, int height)
        {
            _origin = new Point(x, y);
            _width = width;
            _height = height;
        }

        public Area(Point origin, int width, int height)
        {
            _origin = origin;
            _width = width;
            _height = height;
        }

        public Area(double x1, double y1, double x2, double y2)
        {
            _origin = CalculateOrigin(new Point(x1, y1), new Point(x2, y2));
            _width = (int)Math.Abs(x1 - x2);
            _height = (int)Math.Abs(y1 - y2);
        }

        public Area(Point first, Point second)
        {
            _origin = CalculateOrigin(first, second);
            _width = (int)Math.Abs(first.X - second.X);
            _height = (int)Math.Abs(first.Y - second.Y);
        }

        private Point CalculateOrigin(Point first, Point second)
        {
            int x = (int)first.XCompareTo(second).X;
            int y = (int)first.YCompareTo(second).Y;
            return new Point(x, y);
        }
    }
}
