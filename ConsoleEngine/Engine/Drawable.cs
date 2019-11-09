using ConsoleEngine.Engine.Drawables;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEngine.Engine
{
    abstract class Drawable
    {
        private Glyph[] _data = new Glyph[0];
        private Glyph[] _bufferData = new Glyph[0]; //All renders applied to buffer so as to keep _data clean
        private Area _displayArea = new Area();
        private Point _origin = new Point(0, 0);

        public double HeightToWidthRatio = 2;
        public Glyph[] Data { get => _data; private set => _data = value; }
        public int Width { get => DisplayArea.Width; protected set => DisplayArea.Width = value; }
        public int Height { get => DisplayArea.Height; protected set => DisplayArea.Height = value; }
        public Point Origin { get => _origin; set => _origin = value; }
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
            Glyph[] resizedData = new Glyph[width * height];
            Width = width;
            Height = height;

            _data = resizedData;

            ClearBuffer();
        }

        /// <summary>
        /// Fills entire Display with a default Glyph of <paramref name="fillChar"/>
        /// </summary>
        /// <param name="fillChar"></param>
        public void Fill(char fillChar)
        {
            Fill(new Point(0, 0), _displayArea, new Glyph(fillChar));
        }

        /// <summary>
        /// Fills entire Display with <paramref name="fillGlyph"/>
        /// </summary>
        /// <param name="fillGlyph"></param>
        public void Fill(Glyph fillGlyph)
        {
            Fill(new Point(0,0), _displayArea, fillGlyph);
        }

        /// <summary>
        /// Fills area between the <paramref name="first"/> 
        /// Point and the <paramref name="second"/> Point with <paramref name="fillGlyph"/>
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="fillGlyph"></param>
        public void Fill(Point first, Point second, Glyph fillGlyph)
        {
            Fill(CalculateOrigin(first, second), new Area(first, second), fillGlyph);
        }

        /// <summary>
        /// Fills an area defined by <paramref name="width"/> and <paramref name="height"/>,
        /// beginning at the <paramref name="origin"/>, with <paramref name="fillGlyph"/>
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="fillGlyph"></param>
        public void Fill(Point origin, int width, int height, Glyph fillGlyph)
        {
            Fill(origin, new Area(width, height), fillGlyph);
        }

        /// <summary>
        /// Fills defined <paramref name="area"/>, beginning at <paramref name="origin"/> with <paramref name="fillGlyph"/>
        /// </summary>
        /// <param name="area"></param>
        /// <param name="fillGlyph"></param>
        public void Fill(Point origin, Area area, Glyph fillGlyph)
        {
            if (!IsInFrame(origin, area)) return;

            for (int y = (int)origin.Y; y < area.Height; y++)
            {
                for (int x = (int)origin.X; x < area.Width; x++)
                {
                    SafeSet(fillGlyph, x, y);
                }
            }

            ClearBuffer();
        }

        /// <summary>
        /// Renders this over <paramref name="target"/>
        /// </summary>
        /// <param name="target"></param>
        public void RenderOn(Drawable target)
        {
            if (!target.IsInFrame(_origin, _displayArea)) return;

            for (int x = 0; x < Height; x++)
                for (int y = 0; y < Width; y++)
                    target.SafeRender(GetGlyphAt(x, y), _origin.X + x, _origin.Y + y);
        }

        public void SetDrawableArea(Point first, Point second)
        {
            _origin = CalculateOrigin(first, second);
            _displayArea = new Area(first, second);
        }

        private Point CalculateOrigin(Point first, Point second)
        {
            int x = (int) first.XCompareTo(second).X;
            int y = (int) first.YCompareTo(second).Y;
            return new Point(x, y);
        }

        private bool IsInFrame(Point origin, Area area)
        {
            if (origin.X > Width) return false;
            if (origin.Y > Height) return false;
            if (origin.X + area.Width < 0) return false;
            if (origin.Y + area.Height < 0) return false;
            return true;
        }

        private bool IsInFrame(double x, double y)
        {
            if (x >= Width || x < 0) return false;
            if (y >= Height || y < 0) return false;
            return true;
        }

        private void SafeSet(Glyph glyph, double x, double y)
        {
            int resolvedX = (int) x;
            int resolvedY = (int) y;

            if (IsInFrame(x, y))
            {
                int index = (resolvedY * Width + resolvedX);
                _data[index] = glyph;
            }
        }

        private void SafeRender(Glyph glyph, double x, double y)
        {
            int resolvedX = (int)x;
            int resolvedY = (int)y;

            if (IsInFrame(x, y))
            {
                int index = (resolvedY * Width + resolvedX);
                _bufferData[index] = glyph;
            }
        }

        private Glyph GetGlyphAt(int x, int y)
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
            Glyph glyph;

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    glyph = _bufferData[y * Width + x];
                    Console.BackgroundColor = glyph.BackgroundColor;
                    Console.ForegroundColor = glyph.ForegroundColor;
                    Console.Write(glyph);
                }
                Console.WriteLine();
            }

            ClearBuffer();
        }

        private void ClearBuffer()
        {
            _bufferData = new Glyph[_data.Length];
            _data.CopyTo(_bufferData, 0);
        }
    }
}
