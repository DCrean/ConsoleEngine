using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.Engine
{
    class Display : Drawable
    {
        public string Name = "Engine";
        public List<Sprite> Sprites = new List<Sprite>();

        private bool IsDrawing = false;
        private int TicksPerSecond = 10;
        private int FrameCounter = 0;

        public void Show()
        {
            Console.Title = Name;
            IsDrawing = true;
            StartDraw();
            StartFPSCounter();
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

        public void Hide()
        {
            IsDrawing = false;
        }
    }
}
