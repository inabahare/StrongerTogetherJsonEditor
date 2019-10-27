using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using JsonEditor.OutputDTO;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace JsonEditor.Helpers
{
    static class Json
    {
        public static void SaveToFile(string path, DisplayText evaluations)
        {

            var displayTextWithoutObservables =
                (DisplayTextString)evaluations;

            var text = 
                JsonConvert.SerializeObject(displayTextWithoutObservables, Formatting.Indented);
            
            File.WriteAllText(path, text);
        }
    }
}
