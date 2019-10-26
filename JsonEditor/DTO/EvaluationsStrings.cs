using System.Collections.Generic;

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
        public AnswersString Answers { get; set; }
    }

    public class AnswersString
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
