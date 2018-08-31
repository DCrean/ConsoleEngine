using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEngine.Engine
{
    class Point
    {
        private double _x;
        private double _y;
        public double X { get => _x; set => _x = value; }
        public double Y { get => _y; set => _y = value; }

        public Point(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public Point XCompareTo(Point point)
        {
            return _x < point.X ? point : this;
        }

        public Point YCompareTo(Point point)
        {
            return _y < point.Y ? point : this;
        }

        public void Translate(double deltaX, double deltaY)
        {
            _x += deltaX;
            _y += deltaY;
        }
    }
}
