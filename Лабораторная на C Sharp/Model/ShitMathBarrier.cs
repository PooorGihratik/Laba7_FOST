using OxyPlot;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Laba7_FOST
{
    class MathBarrier : INotifyPropertyChanged
    {
        const float maxHeight = 1000;
        private float height;
        private float width;
        private float widthOfHole;
        private float xPositionOfBarrier;
        internal float Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value / 10;
                UpdatePoints();
            }
        }
        internal float Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value / 10;
                UpdatePoints();
            }
        }
        internal float WidthOfHole
        {
            get
            {
                return widthOfHole;
            }
            set
            {
                widthOfHole = value / 10;
                UpdatePoints();
            }
        }
        internal float XPositionOfBarrier
        {
            get
            {
                return xPositionOfBarrier;
            }
            set
            {
                xPositionOfBarrier = value / 10;
                UpdatePoints();
            }
        } 
        private List<DataPoint> upperBarrier;
        private List<DataPoint> bottomBarrier;
        public List<DataPoint> UpperBarrier
        {
            get => upperBarrier;
            private set
            {
                upperBarrier = value;
                OnPropertyChanged("UpperBarrier");
            }
        }
        public List<DataPoint> BottomBarrier
        {
            get => bottomBarrier;
            private set
            {
                bottomBarrier = value;
                OnPropertyChanged("BottomBarrier");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string name = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        internal MathBarrier(float height, float width, float widthOfHole, float xPositionOfBarrier)
        {
            Height = height;
            Width = width;
            WidthOfHole = widthOfHole;
            XPositionOfBarrier = xPositionOfBarrier;
            UpdatePoints();
        }

        private void UpdatePoints()
        {
            UpperBarrier = new List<DataPoint>();
            BottomBarrier = new List<DataPoint>();

            UpperBarrier.Add(new DataPoint(xPositionOfBarrier, maxHeight));
            UpperBarrier.Add(new DataPoint(xPositionOfBarrier, height + widthOfHole));
            UpperBarrier.Add(new DataPoint(xPositionOfBarrier + width, height + widthOfHole));
            UpperBarrier.Add(new DataPoint(xPositionOfBarrier + width, maxHeight));

            BottomBarrier.Add(new DataPoint(xPositionOfBarrier, 0));
            BottomBarrier.Add(new DataPoint(xPositionOfBarrier, height));
            BottomBarrier.Add(new DataPoint(xPositionOfBarrier + width, height));
            BottomBarrier.Add(new DataPoint(xPositionOfBarrier + width, 0));
        }
    }
}
