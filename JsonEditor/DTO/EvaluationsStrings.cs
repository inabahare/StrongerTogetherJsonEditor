using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonEditor.OutputDTO
{
    public class DisplayTextString
    {
        public List<ScenarioString> Scenes { get; set; }
        public List<ResponseString> Responses { get; set; }
    }

    public class ScenarioString
    {
        public string Name { get; set; }
        public string Theme { get; set; }

        public List<string> Setup { get; set; }
        public QuestionsString Questions { get; set; }
    }

    public class QuestionsString
    {
        public string Good { get; set; }
        public string Bad { get; set; }
        public string Neutral { get; set; }
    }

    public class ResponseString
    {
        public string Title { get; set; }
        public ResponseTypeString Responses { get; set; }
    }

    public class ResponseTypeString
    {
        public string Good { get; set; }
        public string Bad { get; set; }
        public string Neutral { get; set; }
    }
}
