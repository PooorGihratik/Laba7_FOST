using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Laba7_FOST
{
    class MathObject : INotifyPropertyChanged
    {
        const float pi = 3.141592f;
        const float acceleration = -9.80665f;

        private readonly float deltaTime;
        private readonly float startSpeed;
        private readonly float angle;
        private (float, float) position;
        private (float, float) speed;
        private List<DataPoint> trajectory;

        public string Id { get; }
        public string Angle { get; }
        public string StartSpeed { get; }
        internal string ListOfData { get; private set; }
        public List<DataPoint> Trajectory
        {
            get => trajectory;
            private set
            {
                trajectory = value;
                OnPropertyChanged("Trajectory");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string name = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        internal MathObject(float speed, float angle, bool inRadian, int id)
        {
            Angle = Convert.ToString(angle);
            if (!inRadian)
            {
                angle = (angle * pi) / 180;
                Angle += " Deg";
            }
            else Angle += " Rad";
            this.angle = angle;
            StartSpeed = Convert.ToString(speed) + " M/s";
            startSpeed = speed;
            this.speed = ((float)(Math.Cos(angle) * startSpeed), (float)(Math.Sin(angle) * startSpeed));
            position = (0, 0);
            deltaTime = 0.01f;
            Id = "Тело " + $"{id + 1}";
        }
        private void AddPoint((float, float) point)
        {
            ListOfData += $"{point.Item1} {point.Item2}\n\0";
        }

        private void ChangePositionInDeltaTime()
        {
            position = (position.Item1 + (speed.Item1 * deltaTime), position.Item2 + (speed.Item2 * deltaTime));
            speed = (speed.Item1, speed.Item2 + (acceleration * deltaTime));
            AddPoint(position);
        }

        private void ChangePositionInDeltaTime(MathBarrier mathBarrier)
        {
            //Определение координат следующей точки
            (float, float) newPos = (position.Item1 + speed.Item1 * deltaTime, position.Item2 + speed.Item2 * deltaTime);

            //Определение координат сторон щели
            float x1Barrier = mathBarrier.XPositionOfBarrier;
            float x2Barrier = mathBarrier.XPositionOfBarrier + mathBarrier.Width;
            float y1Barrier = mathBarrier.Height;
            float y2Barrier = mathBarrier.Height + mathBarrier.WidthOfHole;

            //Разделение по блокам нужно для оптимизации вычислений
            bool horisontalCollision = (x1Barrier < position.Item1 && x2Barrier > position.Item1) || (x1Barrier < newPos.Item1 && x2Barrier > newPos.Item1);
            bool bottomHorizontalCollision = false;
            bool upperHorizontalCollision = false;
            if (horisontalCollision)
            {
                bottomHorizontalCollision = horisontalCollision && (FindX(y1Barrier, position, newPos) > x1Barrier && FindX(y1Barrier, position, newPos) < x2Barrier && newPos.Item2 < y1Barrier);
                upperHorizontalCollision = horisontalCollision && (FindX(y2Barrier, position, newPos) > x1Barrier && FindX(y2Barrier, position, newPos) < x2Barrier && newPos.Item2 > y2Barrier);
            }

            bool verticalCollision = false;
            if (position.Item1 <= x1Barrier && newPos.Item1 >= x1Barrier)
            {
                verticalCollision = (FindY(x1Barrier, position, newPos) < y1Barrier || FindY(x1Barrier, position, newPos) > y2Barrier);
            }

            //Изменение скорости в соответствии с видом столкновения
            if (verticalCollision)
            {
                position = (x1Barrier, FindY(x1Barrier, position, newPos));
                speed = (-speed.Item1, speed.Item2);
            }
            else if (bottomHorizontalCollision)
            {
                speed = (speed.Item1, -speed.Item2);
                position = (FindX(y1Barrier, position, newPos), y1Barrier);

            }
            else if (upperHorizontalCollision)
            {
                speed = (speed.Item1, -speed.Item2);
                position = (FindX(y2Barrier, position, newPos), y2Barrier);
            }
            else ChangePositionInDeltaTime();
        }

        internal void ThrowObject(MathBarrier mathBarrier)
        {
            Trajectory = new List<DataPoint>() { new DataPoint(0, 0) };
            position = (0, 0);
            speed = ((float)(Math.Cos(angle) * startSpeed), (float)(Math.Sin(angle) * startSpeed));

            while (position.Item2 >= 0 && position.Item1 >= 0)
            {
                if (mathBarrier != null) ChangePositionInDeltaTime(mathBarrier);
                else ChangePositionInDeltaTime();
                AddPoint(position);
                Trajectory.Add(new DataPoint(position.Item1, position.Item2));
            }
        }

        //Методы для нахождения точки из уравнения прямой, построенной на координатах двух точек
        private static float FindX(float y, (float, float) point1, (float, float) point2)
        {
            return ((y - point1.Item2) * (point2.Item1 - point1.Item1)) / (point2.Item2 - point1.Item2) + point2.Item1;
        }
        private static float FindY(float x, (float, float) point1, (float, float) point2)
        {
            return ((x - point1.Item1) * (point2.Item2 - point1.Item2)) / (point2.Item1 - point1.Item1) + point2.Item2;
        }
    }
}
