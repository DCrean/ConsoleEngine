using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEngine.Engine.Drawables
{
    class Snake : Sprite
    {
        private class SnakeSegment : Sprite
        {
            public SnakeSegment leadingSegment;

            public SnakeSegment(Point origin, Area area, Glyph fillGlyph) : base(origin, area, fillGlyph)
            {
                leadingSegment = null;
            }

            new public void Animate()
            {
                base.Animate();
                Vector = leadingSegment.Vector;
            }
        }

        private SnakeSegment head;
        private SnakeSegment tail;
        private Area tileSize;

        public int Length;
        public Glyph bodyGlyph;
        public Glyph headGliph;

        public Snake()
        {
            Length = 3;
            headGliph = new Glyph('@');
            bodyGlyph = new Glyph('#');
            tileSize = new Area(1, 1);

            Point start = new Point(0, 0);
           
            head = new SnakeSegment(start, tileSize, headGliph);
            tail = head;

            for (int i = 0; i < Length; i++) AddSegment();
        }

        public void AddSegment()
        {
            SnakeSegment newSegment = new SnakeSegment(tail.Origin, tileSize, bodyGlyph);
            newSegment.leadingSegment = tail;
            tail = newSegment;
        }
    }
}

