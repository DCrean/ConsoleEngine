using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEngine.Engine.Drawables
{
    interface IFocused
    {
        bool HasFocus { get; set; }

        void GainedFocus();
        void LostFocus();
    }
}
