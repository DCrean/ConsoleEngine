using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleEngine.Engine.Drawables
{
    class Display : Drawable
    {
        public bool ShowFPS = true;
        public string Name = "Engine";
        public List<Sprite> Sprites = new List<Sprite>();
        public Glyph Background = new Glyph('.');

        private bool IsDrawing = false;
        private readonly int TicksPerSecond = 10;
        private int FrameCounter = 0;
        private readonly Thread DrawThread;
        private readonly Thread AnimationThread;

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
        public Display(int width, int height, Glyph background)
        {
            height = (int) (height / HeightToWidthRatio);
            DisplayArea = new Area(width, height);
            this.Background = background;
            Fill(background);
            DrawThread = new Thread(new ThreadStart(StartDraw));
            AnimationThread = new Thread(new ThreadStart(StartAnimationController));
        }

        /// <summary>
        /// Begin Displaying
        /// </summary>
        public void Show()
        {
            Console.Title = Name;
            IsDrawing = true;
            if(ShowFPS) StartFPSCounter();
            AnimationThread.Start();
            DrawThread.Start();
        }

        private void StartDraw()
        {
            while (IsDrawing)
            {
                RenderFrame();
                Draw();
                FrameCounter++;
                Thread.Sleep(0);
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

        private void StartAnimationController()
        {
            while (IsDrawing)
            {
                Thread.Sleep(1000 / TicksPerSecond);
                foreach (Sprite sprite in Sprites)
                {
                    sprite.Animate();
                    Thread.Sleep(0);
                }
            }
        }

        public void RenderFrame()
        {
            RenderSprites();
        }

        private void RenderSprites()
        {
            foreach(Sprite sprite in Sprites)
                sprite.RenderOn(this);
        }

        /// <summary>
        /// Stop Displaying
        /// </summary>
        public void Hide()
        {
            IsDrawing = false;
            if(DrawThread.IsAlive) DrawThread.Join();
            if(AnimationThread.IsAlive) AnimationThread.Join();
            Fill(' ');
        }
    }
}
