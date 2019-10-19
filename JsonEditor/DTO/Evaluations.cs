using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JsonEditor.Annotations;

namespace JsonEditor
{
    public class DisplayText
    {
        public ObservableCollection<Scenario> Scenes { get; set; }
        public List<Response> Responses { get; set; }
    }

    public class Scenario
    {
        public string Name { get; set; }
        public string Theme { get; set; }

        public ObservableCollection<string> Setup { get; set; }
        public Questions Questions { get; set; }
    }

    public class Questions
    {
        public string Good { get; set; }
        public string Bad { get; set; }
        public string Neutral { get; set; }
    }

    public class Response
    {
        public string Title { get; set; }
        public ResponseType Responses { get; set; }
    }

    public class ResponseType
    {
        public string Good { get; set; }
        public string Bad { get; set; }
        public string Neutral { get; set; }
    }

    //public class ResponsesType
    //{
    //    public string Name { get; set; }
    //    public string Text { get; set; }
    //}
}
