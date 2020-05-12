using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Laba7_FOST
{
    class ViewModel : INotifyPropertyChanged
    {
        //Основная модель
        private MathBarrier mathBarrier;
        public MathBarrier MathBarrier
        {
            get => mathBarrier;
            set
            {
                mathBarrier = value;
                if (SelectedMathObject != null) SelectedMathObject.ThrowObject(MathBarrier);
                OnPropertyChanged("MathBarrier");
            }
        }
        
        private MathObject selectedMathObject;
        public MathObject SelectedMathObject
        {
            get => selectedMathObject;
            set
            {
                selectedMathObject = value;
                if (SelectedMathObject != null)
                {
                    IsSelected = true;
                    SelectedMathObject.ThrowObject(MathBarrier);
                }
                else IsSelected = false;
                OnPropertyChanged("SelectedMathObject");
            }
        }
        public ObservableCollection<MathObject> MathObjects { get; private set; }
        internal void AddNewMathObject(float speed, float angle, bool inRadian)
        {
            MathObjects.Add(new MathObject(speed, angle, inRadian, MathObjects.Count));
            SelectedMathObject = MathObjects[MathObjects.Count - 1];
        }

        private FileWriter writer = new FileWriter();
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string name = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        public ViewModel()
        {
            MathObjects = new ObservableCollection<MathObject>();
        }

        //Команды для кнопок
        private ObjectsCommand addCommand;
        private ObjectsCommand removeCommand;
        private ObjectsCommand saveCommand;
        public ObjectsCommand AddCommand
        {
            get
            {
                return addCommand ??
                (addCommand = new ObjectsCommand(obj =>
                {
                    AddNewMathObject(float.Parse(Speed), float.Parse(Angle), InRadian);
                }));
            }
        }
        public ObjectsCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new ObjectsCommand(obj =>
                    {
                        if (selectedMathObject != null)
                        {
                            MathObjects.Remove(selectedMathObject);
                            SelectedMathObject = null;
                        }
                    }));
            }
        }
        public ObjectsCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new ObjectsCommand(obj =>
                    {
                        if (SelectedMathObject != null)
                        {
                            writer.SaveInTextFileAsync(SelectedMathObject);
                        }
                    }));
            }
        }
        
        //Интерфейс и привязка
        //Параметры барьера
        private int height = 200;
        private int width = 200;
        private int xPosition = 200;
        private int widthOfHole = 200;
        public int Height
        {
            get => height;
            set
            {
                height = value;
                if (MathBarrier != null)
                {
                    SelectedMathObject = null;
                    MathBarrier.Height = Height;
                }
            }
        }
        public int Width
        {
            get => width;
            set
            {
                width = value;
                if (MathBarrier != null)
                {
                    SelectedMathObject = null;
                    MathBarrier.Width = Width;
                }
            }
        }
        public int XPosition
        {
            get => xPosition;
            set
            {
                xPosition = value;
                if (MathBarrier != null)
                {
                    SelectedMathObject = null;
                    MathBarrier.XPositionOfBarrier = XPosition;
                }
            }
        }
        public int WidthOfHole
        {
            get => widthOfHole;
            set
            {
                widthOfHole = value;
                if (MathBarrier != null)
                {
                    SelectedMathObject = null;
                    MathBarrier.WidthOfHole = WidthOfHole;
                }
            }
        }

        //Параметры тела
        private string speed;
        private string angle;
        public string Speed
        {
            get => speed;
            set
            {
                speed = value.Replace(".", ",").Replace(" ", "");
                IsStartButtonEnabled = float.TryParse(Speed, out _) && float.TryParse(Angle, out _);
            }
        }
        public string Angle
        {
            get => angle;
            set
            {
                angle = value.Replace(".", ",").Replace(" ", "");
                IsStartButtonEnabled = float.TryParse(Speed, out _) && float.TryParse(Angle, out _);
            }
        }
        public bool InRadian { get; set; }
        
        //Прочие свойства элементов управления
        private bool isExpanded = false;
        private bool isButtonEnabled = false;
        private bool isStartButtonEnabled = false;
        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                isExpanded = value;
                if (isExpanded) MathBarrier = new MathBarrier(Height, Width, WidthOfHole, XPosition);
                else MathBarrier = null;
                if (SelectedMathObject != null)
                {
                    SelectedMathObject.ThrowObject(MathBarrier);
                }
            }
        }
        public bool IsSelected
        {
            get => isButtonEnabled;
            set
            {
                isButtonEnabled = value;
                OnPropertyChanged("IsSelected");
            }
        }
        public bool IsStartButtonEnabled
        {
            get => isStartButtonEnabled;
            set
            {
                isStartButtonEnabled = value;
                OnPropertyChanged("IsStartButtonEnabled");
            }
        }
    }
}
