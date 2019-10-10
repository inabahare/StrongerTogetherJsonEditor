using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JsonEditor.Annotations;

namespace JsonEditor
{
    public class Evaluations
    {
        public List<Response> Responses { get; set; }
    }

    public class Response
    {
        public string Title { get; set; }

        public string Theme { get; set; }

        public List<MoralityResponse> Responses { get; set; }
    }

    public class MoralityResponse
    {
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
