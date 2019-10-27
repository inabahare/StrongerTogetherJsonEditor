using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using JsonEditor.OutputDTO;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace JsonEditor.Helpers
{
    public class DataManager
    {
        const string JsonPathInUnityProject = "Assets/Resources/Languages";

        public static string FullPathToJsonFiles { get; set; }

        public DataManager()
        {
            if (FullPathToJsonFiles == null)
                LoadPath();
        }

        void LoadPath()
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".json",
                Filter = "Stronger Together Project|*.sln"
            };

            dlg.ShowDialog();

            FullPathToJsonFiles = GetLanguageDir(dlg.FileName, dlg.SafeFileName);
        }

        public List<string> GetLanguagesThatAlreadyExist()
        {
            var files = Directory.GetFiles(FullPathToJsonFiles);

            var jsonFilesWithoutFullPath = 
                files.Where(file => Regex.Match(file, "^*.json$").Success)
                    .Select(file => file.Replace($"{FullPathToJsonFiles}\\", ""))
                     .ToList();

            return jsonFilesWithoutFullPath;
        }

        static string GetLanguageDir(string fullPath, string slnName)
        {
            var pathWithoutSln = fullPath.Replace(slnName, "");
            var pathToJson = Path.Combine(pathWithoutSln, JsonPathInUnityProject);
            return pathToJson;
        }

        public static void SaveToFile(string language, DisplayText evaluations)
        {
            var newFilePath =
                Path.Combine(FullPathToJsonFiles, $"{language}.json");

            var displayTextWithoutObservables =
                (DisplayTextString)evaluations;

            var text =
                JsonConvert.SerializeObject(displayTextWithoutObservables, Formatting.Indented);

            File.WriteAllText(newFilePath, text);
        }

        public static DisplayText LoadFromFile(string language)
        {
            var jsonPath =
                Path.Combine(FullPathToJsonFiles, $"{language}.json");
            var text = File.ReadAllText(jsonPath);
            var evaluations = JsonConvert.DeserializeObject<DisplayText>(text);
            return evaluations;
        }
    }
}
