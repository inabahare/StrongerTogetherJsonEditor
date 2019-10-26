using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace JsonEditor.Helpers
{

    public class StrongerTogetherFinder
    {
        const string JsonPathInUnityProject = "Assets/Resources/Languages";

        public static string JsonPath { get; set; }

        public StrongerTogetherFinder()
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".json",
                Filter = "Stronger Together Project|*.sln"
            };

            dlg.ShowDialog();
            JsonPath = GetPathOfEvaluationJson(dlg.FileName, dlg.SafeFileName);
        }

        public List<string> GetLanguagesThatAlreadyExist()
        {
            var files = Directory.GetFiles(JsonPath);

            var jsonFilesWithoutFullPath = 
                files.Where(file => Regex.Match(file, "^*.json$").Success)
                    .Select(file => file.Replace($"{JsonPath}\\", ""))
                     .ToList();

            return jsonFilesWithoutFullPath;
        }

        static string GetPathOfEvaluationJson(string fullPath, string slnName)
        {
            var pathWithoutSln = fullPath.Replace(slnName, "");
            var pathToJson = Path.Combine(pathWithoutSln, JsonPathInUnityProject);
            return pathToJson;
        }
    }
}
