using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEngine.Engine
{
    abstract class Drawable
    {
        private char[] BufferData = new char[0];
        public char[] Data { get; private set; } = new char[0];
        public int Width { get; private set; } = 0;
        public int Height { get; private set; } = 0;

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
            Fill(0, 0, Width, Height, fillChar);
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
            x = x < Width ? x : Width;
            x2 = x2 < Width ? x2 : Width;
            y = y < Height ? y : Height;
            y2 = y2 < Height ? y2 : Height;

            for (int currentY = y; currentY < y2; currentY++)
            {
                for (int currentX = x; currentX < x2; currentX++)
                {
                    Data[currentY * Width + currentX] = fillChar;
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
            if(target.IsInFrame(x, y, Width, Height))
            {
                for (int currY = 0; currY < Height; currY++)
                {
                    for (int currX = 0; currX < Width; currX++)
                    {
                        int targetX = x + currX;
                        int targetY = y + currY;
                        if(targetX < target.Width && targetY < target.Height && targetX >= 0 && targetY >= 0)
                            target.BufferData[targetY * target.Width + targetX ] = Data[currY * Width + currX];
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

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Console.Write(BufferData[y * Width + x]);
                }
                Console.WriteLine();
            }

            ClearBuffer();
        }

        private void ClearBuffer()
        {
            BufferData = new char[Data.Length];
            Data.CopyTo(BufferData, 0);
        }
    }
}
