using System;
using System.Collections.Generic;
using System.Text;
using static ConsoleEngine.Engine.Drawable;

namespace ConsoleEngine.Engine.Drawables
{
    interface IAnimated
    {
        Force Vector { get; set; }

        /// <summary>
        /// Handles how the object will animate each tick
        /// </summary>
        void Animate();
    }
}
