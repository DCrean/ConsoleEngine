using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEngine.Engine
{
    class Force
    {
        /// <summary>
        /// Resolves DeltaX, DeltaY, Rotations, and the Quadrant of a given Vector. 
        /// 
        /// Quadrants represent 90 degree segments within a Rotation and are stored 
        /// as an int (0 - 3).
        /// 
        /// Rotations represent 360 degree segments and are stored as an int (0+).
        /// 
        /// DeltaY represents the change across the y-axis; assumes y-axis is toward
        /// the beginning of the Quadrant. ie. if the Quadrant is 1, it covers angles
        /// 90 - 180 and assumes the y-axis is towards 90 degrees.
        /// 
        /// DeltaX represents the change across the x-axis; assumes x-axis is toward
        /// the end of the Quadrant. ie. if the Quadrant is 1, it covers angles
        /// 90 - 180 and assumes the x-axis is towards 180 degrees.
        /// </summary>
        internal class QuadrantVector
        {
            private double _angle;

            private double Force { get; set; }
            private double Angle
            {
                get => _angle;
                set
                {
                    _angle = value % 90;
                    Quadrant = (int) value / 90 % 4;
                    Rotations = (int) value / 360;
                }
            }
            public int Rotations { get; private set; }
            public int Quadrant { get; private set; }
            public double DeltaX { get => Force * Math.Sin(Angle * (Math.PI / 180)); }
            public double DeltaY { get => Force * Math.Cos(Angle * (Math.PI / 180)); }

            public QuadrantVector(Force vector)
            {
                Force = vector._force;
                Angle = vector._angle;
            }
        }

        private double _angle = 0;
        private double _force = 0;
        private double _deltaX = 0;
        private double _deltaY = 0;

        public double DeltaX { get => _deltaX; }
        public double DeltaY { get => _deltaY; }

        public Force() { }

        public Force(double force, double angle)
        {
            _angle = angle;
            _force = force;
            ResolveDeltas();
        }

        public void add(Force forceToAdd)
        {
            this._deltaX += forceToAdd.DeltaX;
            this._deltaY += forceToAdd.DeltaY;
        }

        private void ResolveDeltas()
        {
            QuadrantVector subVector = new QuadrantVector(this);
            _deltaX = ResolveDeltaX(subVector);
            _deltaY = ResolveDeltaY(subVector);
        }

        private double ResolveDeltaX(QuadrantVector qVector)
        {
            if (qVector.Quadrant == 0) // Angles 0 - 90
                return qVector.DeltaX;
            if (qVector.Quadrant == 1) // Angles  90 - 180
                return qVector.DeltaY;
            if (qVector.Quadrant == 2) // Angles 180 - 270
                return -qVector.DeltaX;
            if (qVector.Quadrant == 3) // Angles 270 - 360
                return -qVector.DeltaY;

            throw new Exception("Invalid Quadrant");
        }

        private double ResolveDeltaY(QuadrantVector qVector)
        {
            if (qVector.Quadrant == 0) // Angles 0 - 90
                return -qVector.DeltaY;
            if (qVector.Quadrant == 1) // Angles  90 - 180
                return qVector.DeltaX;
            if (qVector.Quadrant == 2) // Angles 180 - 270
                return qVector.DeltaY;
            if (qVector.Quadrant == 3) // Angles 270 - 360
                return -qVector.DeltaX;

            throw new Exception("Invalid Quadrant");
        }
    }
}
