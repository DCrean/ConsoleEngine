using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEngine.Engine
{
    abstract class Drawable
    {
        private char[] _data = new char[0];
        private char[] _bufferData = new char[0]; //All renders applied to buffer so as to keep _data clean
        private Area _displayArea = new Area();

        public char[] Data { get => _data; private set => _data = value; }
        public int Width { get => DisplayArea.Width; protected set => DisplayArea.Width = value; }
        public int Height { get => DisplayArea.Height; protected set => DisplayArea.Height = value; }

        public Area DisplayArea
        {
            get => _displayArea;
            set
            {
                _displayArea = value;
                Resize(_displayArea.Width, _displayArea.Height);
            }
        }

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
            
            _data = resizedData;

            ClearBuffer();
        }

        /// <summary>
        /// Fills entire Display with <paramref name="fillChar"/>
        /// </summary>
        /// <param name="fillChar"></param>
        public void Fill(char fillChar)
        {
            Area drawableArea = new Area(0, 0, Width, Height);
            Fill(drawableArea, fillChar);
        }

        /// <summary>
        /// Fills area between the <paramref name="first"/> 
        /// Point and the <paramref name="second"/> Point with <paramref name="fillChar"/>
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="fillChar"></param>
        public void Fill(Point first, Point second, char fillChar)
        {
            Fill( new Area(first, second), fillChar);
        }

        /// <summary>
        /// Fills defined <paramref name="area"/> with <paramref name="fillChar"/>
        /// </summary>
        /// <param name="area"></param>
        /// <param name="fillChar"></param>
        public void Fill(Area area, char fillChar)
        {
            if (!IsInFrame(area)) return;

            for (int y = (int) area.Origin.Y; y < area.Height; y++)
            {
                for (int x = (int) area.Origin.X; x < area.Width; x++)
                {
                    SafeSet(fillChar, x, y);
                }
            }

            ClearBuffer();
        }

        /// <summary>
        /// Renders this over <paramref name="target"/> at specified <paramref name="position"/>
        /// </summary>
        /// <param name="target"></param>
        /// <param name="position"></param>
        public void RenderOn(Drawable target)
        {
            if (!target.IsInFrame(DisplayArea)) return;

            for (int x = 0; x < Height; x++)
                for (int y = 0; y < Width; y++)
                    target.SafeRender(GetCharAt(x, y), DisplayArea.Origin.X + x, DisplayArea.Origin.Y + y);
        }

        private bool IsInFrame(Area area)
        {
            if (area.Origin.X > Width) return false;
            if (area.Origin.Y > Height) return false;
            if (area.Origin.X + area.Width < 0) return false;
            if (area.Origin.Y + area.Height < 0) return false;
            return true;
        }

        private bool IsInFrame(double x, double y)
        {
            if (x >= Width || x < 0) return false;
            if (y >= Height || y < 0) return false;
            return true;
        }

        private void SafeSet(char glyph, double x, double y)
        {
            int resolvedX = (int) x;
            int resolvedY = (int) y;

            if (IsInFrame(x, y))
            {
                int index = (int) (resolvedY * Width + resolvedX);
                _data[index] = glyph;
            }
        }

        private void SafeRender(char glyph, double x, double y)
        {
            int resolvedX = (int)x;
            int resolvedY = (int)y;

            if (IsInFrame(x, y))
            {
                int index = (int) (resolvedY * Width + resolvedX);
                _bufferData[index] = glyph;
            }
        }

        private char GetCharAt(int x, int y)
        {
            return _data[x * Width + y];
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
                    Console.Write(_bufferData[y * Width + x]);
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
