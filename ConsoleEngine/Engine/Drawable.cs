using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEngine.Engine
{
    abstract class Drawable
    {
        private char[] _bufferData = new char[0];
        protected int _width = 0;
        protected int _height = 0;
        private char[] _data = new char[0];

        public char[] Data { get => _data; private set => _data = value; }
        public int Width { get => _width; private set => _width = value; }
        public int Height { get => _height; private set => _height = value; }
        /// <summary>
        /// Resizes the Display to the new <paramref name="width"/> and <paramref name="height"/>
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void Resize(int width, int height)
        {
            char[] resizedData = new char[width * height];
            Width = width;
            Height = height;

            Data.CopyTo(resizedData, 0);
            Data = resizedData;

            ClearBuffer();
        }

        /// <summary>
        /// Fills entire Display with <paramref name="fillChar"/>
        /// </summary>
        /// <param name="fillChar"></param>
        public void Fill(char fillChar)
        {
            Fill(0, 0, _width, _height, fillChar);
        }

        /// <summary>
        /// Fills an area of the Display between (<paramref name="x"/>,<paramref name="y"/>) and  
        /// (<paramref name="x2"/>,<paramref name="y2"/>) with <paramref name="fillChar"/>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="fillChar"></param>
        public void Fill(int x, int y, int x2, int y2, char fillChar)
        {
            x = x < _width ? x : _width;
            x2 = x2 < _width ? x2 : _width;
            y = y < _height ? y : _height;
            y2 = y2 < _height ? y2 : _height;

            for (int currentY = y; currentY < y2; currentY++)
            {
                for (int currentX = x; currentX < x2; currentX++)
                {
                    Data[currentY * _width + currentX] = fillChar;
                }
            }

            ClearBuffer();
        }

        /// <summary>
        /// Renders this on top of <paramref name="target"/> Display at (<paramref name="x"/>, <paramref name="y"/>)
        /// </summary>
        /// <param name="target"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void RenderOn(Drawable target, int x, int y)
        {
            if (target.IsInFrame(x, y, _width, _height))
            {
                for (int currY = 0; currY < _height; currY++)
                {
                    for (int currX = 0; currX < _width; currX++)
                    {
                        int targetX = x + currX;
                        int targetY = y + currY;
                        if (targetX < target.Width && targetY < target.Height && targetX >= 0 && targetY >= 0)
                            target._bufferData[targetY * target.Width + targetX] = _data[currY * _width + currX];
                    }
                }
            }
        }

        private bool IsInFrame(int x, int y, int width, int height)
        {
            if (x > Width) return false;
            if (y > Height) return false;
            if (x + width < 0) return false;
            if (y + height < 0) return false;
            return true;
        }

        /// <summary>
        /// Draws this Display to the console at (0,0)
        /// </summary>
        public void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    Console.Write(_bufferData[y * _width + x]);
                }
                Console.WriteLine();
            }

            ClearBuffer();
        }

        private void ClearBuffer()
        {
            _bufferData = new char[_data.Length];
            _data.CopyTo(_bufferData, 0);
        }
    }
}
