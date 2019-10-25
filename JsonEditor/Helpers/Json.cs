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
        static string JsonPath { get; set; }
        const string JsonPathInUnityProject = "Assets/Resources/Languages/English.json"; // TODO: Make availible for other languages 

        public static DisplayText LoadFromFile()
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".json",
                Filter = "Stronger Together Project|*.sln"
            };

            dlg.ShowDialog();
            JsonPath = GetPathOfEvaluationJson(dlg.FileName, dlg.SafeFileName);

            var text = File.ReadAllText(JsonPath);
            var evaluations = JsonConvert.DeserializeObject<DisplayText>(text);
            return evaluations;
        }

        static string GetPathOfEvaluationJson(string fullPath, string slnName)
        {
            var pathWithoutSln = fullPath.Replace(slnName, "");
            var pathToJson = Path.Combine(pathWithoutSln, JsonPathInUnityProject);
            return pathToJson;
        }

        public static void SaveToFile(DisplayText evaluations)
        {

            var displayTextWithoutObservables =
                (DisplayTextString)evaluations;

            var text = 
                JsonConvert.SerializeObject(evaluations, Formatting.Indented);
            
            var newPath = $"{JsonPath}.backup.{DateTime.Now.ToString("yyyyMMddHHmm")}";
            File.Copy(JsonPath, newPath);
            File.WriteAllText(JsonPath, text); // TODO: Convert the DTO's to a format where I don't get "TheList"
            MessageBox.Show("File saved");
        }
    }
}
