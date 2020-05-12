using System;
using System.IO;
using System.Text;

namespace Laba7_FOST
{
    class FileWriter
    {
        string path = Path.Combine(Environment.CurrentDirectory, "Координаты.txt");
        StreamWriter writer;
        
        internal async void SaveInTextFileAsync(MathObject mathObject)
        {
            writer = new StreamWriter(path, false, Encoding.Default);
            await writer.WriteAsync(mathObject.ListOfData);
            writer.Close();
        }
    }
}
