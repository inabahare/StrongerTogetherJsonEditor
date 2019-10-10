using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace JsonEditor.Helpers
{
    class Json
    {
        public static Evaluations LoadFromFile()
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".json"
            };

            dlg.ShowDialog();
            var text = File.ReadAllText(dlg.FileName);
            var evaluations = JsonConvert.DeserializeObject<Evaluations>(text);
            return evaluations;

        }
    }
}
