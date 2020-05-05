using System;
using System.Collections.Generic;
using OxyPlot;
using System.IO;
using System.Text;

namespace Laba7_FOST
{
    class NewViewModel
    {
        string path = Path.Combine(Environment.CurrentDirectory, "Координаты.txt");
        StreamWriter writer;

        internal List<DataPoint> DataPoints { get; private set; }
        MathObject mathObject;
        internal string ListOfData { get; private set; }
        internal delegate void MessageHandler(string message, string title);
        internal event MessageHandler Show;

        internal NewViewModel()
        {
            DataPoints = new List<DataPoint>();
            ListOfData = "";
        }
        internal void ShowPicture(string speed, string angle, bool inRadians)
        {
            DataPoints = new List<DataPoint>();
            ListOfData = "";
            speed = speed.Replace(".", ",").Replace(" ", "");
            angle = angle.Replace(".", ",").Replace(" ", "");
            try
            {
                mathObject = new MathObject(Convert.ToDouble(speed), Convert.ToDouble(angle), inRadians);
                List<(float, float)> Points = mathObject.ThrowObject();
                foreach ((float, float) f in Points)
                {
                    DataPoints.Add(new DataPoint(f.Item1, f.Item2));
                    ListOfData += $"{f.Item1} {f.Item2}\n\0";
                }
            }
            catch
            {
                Show("Не удалось начать вычисления\nНекорректные данные", "Ошибка");
            }
        }
        internal void SaveInFile()
        {
            writer = new StreamWriter(path, false, Encoding.Default);
            writer.Write(ListOfData);
            writer.Close();
        }
    }
}
