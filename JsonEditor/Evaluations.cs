using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonEditor
{
    class Evaluations
    {
        public List<Response> Responses { get; set; }
    }

    class Response
    {
        public string Title { get; set; }
        public string Theme { get; set; }
        public List<Responses> Responses { get; set; }

        public override string ToString()
        {
            return $"{Title} - {Theme}";
        }
    }

    class Responses
    {
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
