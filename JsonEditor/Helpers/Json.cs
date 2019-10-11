using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace JsonEditor.Helpers
{
    static class Json
    {
        static string Path { get; set; }

        public static Evaluations LoadFromFile()
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".json"
            };

            dlg.ShowDialog();
            Path = dlg.FileName;

            var text = File.ReadAllText(dlg.FileName);
            var evaluations = JsonConvert.DeserializeObject<Evaluations>(text);
            return evaluations;
        }

        public static void SaveToFile(Evaluations evaluations)
        {
            var text = JsonConvert.SerializeObject(evaluations, Formatting.Indented);
            File.Copy(Path, $"{Path}.backup.{DateTime.Now}");
            File.WriteAllText($"{Path}.b", text);
            MessageBox.Show("File saved");
        }
    }
}
