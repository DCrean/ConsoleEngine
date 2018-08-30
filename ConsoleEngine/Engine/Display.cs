using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.Engine
{
    class Display : Drawable
    {
        public bool ShowFPS = true;
        public string Name = "Engine";
        public List<Sprite> Sprites = new List<Sprite>();

        private bool IsDrawing = false;
        private int TicksPerSecond = 10;
        private int FrameCounter = 0;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Display() { }

        /// <summary>
        /// Returns a new Display with <paramref name="width"/> and <paramref name="height"/>, filled with <paramref name="fillChar"/>
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="fillChar"></param>
        public Display(int width, int height, char fillChar)
        {
            _width = width;
            _height = Height;
            Fill(fillChar);
        }

        /// <summary>
        /// Begin Displaying
        /// </summary>
        public void Show()
        {
            Console.Title = Name;
            IsDrawing = true;
            StartDraw();
            if(ShowFPS) StartFPSCounter();
            StartAnimationController();
        }

        private async void StartDraw()
        {
            while (IsDrawing)
            {
                await Task.Delay(1);
                RenderFrame();
                Draw();
                FrameCounter++;
            }
        }

        private async void StartFPSCounter()
        {
            DateTime FrameCounterTime = DateTime.Now;
            while (IsDrawing)
            {
                await Task.Delay(1000);
                TimeSpan offset = DateTime.Now.Subtract(FrameCounterTime);
                double currentFPS = FrameCounter / offset.TotalMilliseconds * 1000;
                FrameCounterTime = DateTime.Now;
                FrameCounter = 0;
                Console.Title = Name + "[" + currentFPS + "]";
            }
        }

        private async void StartAnimationController()
        {
            while (IsDrawing)
            {
                await Task.Delay(1000 / TicksPerSecond);
                foreach (Sprite sprite in Sprites)
                    sprite.Animate();
            }
        }

        private void RenderFrame()
        {
            RenderSprites();
        }

        private void RenderSprites()
        {
            foreach(Sprite sprite in Sprites)
                sprite.RenderOn(this, (int)sprite.X, (int)sprite.Y);
        }

        /// <summary>
        /// Stop Displaying
        /// </summary>
        public void Hide()
        {
            IsDrawing = false;
        }
    }
}
