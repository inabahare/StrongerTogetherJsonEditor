using System.Collections.Generic;
using System.Collections.ObjectModel;

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

        public ObservableCollection<ObservableString> Setup { get; set; }
        public Questions Questions { get; set; }

        public static Scenario CreateEmpty(string scenario, int number) =>
            new Scenario
            {
                Name = $"{scenario}_{number:D2}",
                Theme = "New theme",
                Setup = new ObservableCollection<ObservableString>(),
                Questions = new Questions
                {
                    Good = "Insert good text here",
                    Neutral = "Insert neutral text here",
                    Bad = "Insert bad text here"
                }
            };
    }

    public class Questions
    {
        public ObservableString Good { get; set; }
        public ObservableString Bad { get; set; }
        public ObservableString Neutral { get; set; }
    }

    public class Response
    {
        public string Title { get; set; }
        public ResponseType Responses { get; set; }
    }

    public class ResponseType
    {
        public ObservableString Good { get; set; }
        public ObservableString Bad { get; set; }
        public ObservableString Neutral { get; set; }
    }
}
