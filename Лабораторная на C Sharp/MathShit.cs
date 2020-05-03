using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba8_FOST
{
    class MathObject
    {
        const float pi = 3.141592f;
        const float acceleration = -9.8f;
        private readonly float deltaTime = 0.0005f;
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
        }

        internal string ChangePositionInDeltaTime()
        {
            XPosition += xspeed * deltaTime;
            YPosition += yspeed * deltaTime;
            yspeed += acceleration * deltaTime;
            return $"{XPosition} {YPosition}";
        }
    }
}
