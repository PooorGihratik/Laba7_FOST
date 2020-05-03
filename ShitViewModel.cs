using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba8_FOST
{
    class ShitViewModel
    {
        MathObject obj;
        string path = Path.Combine(Environment.CurrentDirectory, "output.txt");
        StreamWriter writer;
        internal delegate void MessageHandler(string message,string title);
        internal event MessageHandler Show;
        internal string GetResults(string speed, string angle, bool inRadians)
        {
            //Отчистка файла от прошлых данных
            writer = new StreamWriter(path, false, Encoding.Default);
            writer.WriteLine("");
            writer.Close();
            writer = new StreamWriter(path, true, Encoding.Default);
            //Форматирование входных данных(Удаление пробелов и замена на запятые)
            speed = speed.Replace(".", ",").Replace(" ", "");
            angle = angle.Replace(".", ",").Replace(" ", "");
            try
            {
                obj = new MathObject(Convert.ToDouble(speed), Convert.ToDouble(angle), inRadians);
                string result = "";
                for (int i = 0; i < 500; i++)
                {
                    string line = obj.ChangePositionInDeltaTime();
                    writer.WriteLine(line);
                    result += line + "\n";
                    if (obj.YPosition <= 0) break;
                }
                writer.Close();
                return result;
            }
            catch
            {
                string message,title;
                if (speed == "" || angle == "")
                {
                    message = "Ну и чо ты тут пустыми поля оставил, умный шоль?";
                    title = "Умный слишком, да?";
                }
                else if (speed == "Жмышенко" && angle =="Валерий")
                {
                    message = "Не, ну это тупо бан";
                    title = "Ты зачем сьел ПАЖЕЛОВО ДЕДА?";
                }
                else
                {
                    title = "И-ДИ-ОООТ";
                    message = "НЕ СМЕТЬ ПИСАТЬ ХУЙНЮ В ПОЛЯХ!!! СКАЗАНО ПИСАТЬ СКОРОСТЬ - ПИШИ СКОРОСТЬ, СКАЗАНО ПИСАТЬ УГОЛ - ПИШИ УГОЛ!!";
                }
                Show(message,title);
                writer.Close();
                return "";
            }
        }
    }
}
