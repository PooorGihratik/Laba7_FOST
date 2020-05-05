using System;
using System.Collections.Generic;

namespace Laba7_FOST
{
    class MathObject
    {
        const int countOfPoints = 1000;
        const float pi = 3.141592f;
        const float acceleration = -9.80665f;
        private readonly float deltaTime;
        private float xspeed;
        private float yspeed;
        private float angle;
        internal float XPosition { get; private set; }
        internal float YPosition { get; private set; }

        internal MathObject(double speed, double angle, bool inRadian)
        {
            if (!inRadian) this.angle = ((float)angle * pi) / 180;
            else this.angle = (float)angle;
            (xspeed, yspeed) = ((float)(Math.Cos(this.angle) * speed), (float)(Math.Sin(this.angle) * speed));
            (XPosition, YPosition) = (0, 0);
            deltaTime =  (float)(2 * speed) / ((countOfPoints-1) * -acceleration);
        }

        internal void ChangePositionInDeltaTime()
        {
            XPosition += xspeed * deltaTime;
            YPosition += yspeed * deltaTime;
            yspeed += acceleration * deltaTime;
        }

        internal List<(float,float)> ThrowObject()
        {
            List<(float, float)> Results = new List<(float, float)>();
            for (int i=0;i<countOfPoints;i++)
            {
                ChangePositionInDeltaTime();
                Results.Add((XPosition, YPosition));
                if (YPosition <= 0) break;
            }
            return Results;
        }
    }
}
