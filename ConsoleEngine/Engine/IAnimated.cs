using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEngine.Engine
{
    interface IAnimated
    {
        double X { get; set; }
        double Y { get; set; }
        double DeltaX { get; set; }
        double DeltaY { get; set; }

        /// <summary>
        /// Handles how the object will animate each tick
        /// </summary>
        void Animate();
    }
}
