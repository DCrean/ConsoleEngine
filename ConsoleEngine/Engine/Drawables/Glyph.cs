using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEngine.Engine.Drawables
{
    /// <summary>
    /// Any character or symbol and its' display attributes.
    /// </summary>
    class Glyph
    {
        private ConsoleColor _backgroundColor = ConsoleColor.Black;
        private ConsoleColor _foregroundColor = ConsoleColor.White;
        private char _symbol;

        public char Symbol { get => _symbol; set => _symbol = value; }
        public ConsoleColor ForegroundColor { get => _foregroundColor; set => _foregroundColor = value; }
        public ConsoleColor BackgroundColor { get => _backgroundColor; set => _backgroundColor = value; }

        public Glyph(char symbol)
        {
            _symbol = symbol;
        }

        public override string ToString()
        {
            return _symbol + "";
        }
    }
}
