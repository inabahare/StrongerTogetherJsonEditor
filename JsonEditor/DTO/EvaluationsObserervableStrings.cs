﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JsonEditor.OutputDTO;

namespace JsonEditor
{
    public class DisplayText
    {
        public ObservableCollection<Scenario> Scenes { get; set; }
        public List<Response> Responses { get; set; }

        public static DisplayText CreateEmpty()
        {
            return new DisplayText
            {
                Scenes = new ObservableCollection<Scenario>
                {
                    Scenario.CreateEmpty("Morality", 1)
                },
                Responses = new List<Response>
                {
                    Response.CreateEmpty("Morality", 1)
                }
            };
        }

        public static explicit operator DisplayTextString(DisplayText displayText)
        {
            return new DisplayTextString
            {
                Scenes = displayText.Scenes.Select(scene => (ScenarioString)scene).ToList(),
                Responses = displayText.Responses.Select(scene => (ResponseString)scene).ToList()
            };
        }
    }

    public class Scenario
    {
        public string Name { get; set; }
        public string Theme { get; set; }

        public ObservableCollection<ObservableString> Setup { get; set; }
        public Answers Questions { get; set; }

        public static Scenario CreateEmpty(string scenario, int number) =>
            new Scenario
            {
                Name = $"{scenario}_{number:D2}",
                Theme = "New theme",
                Setup = new ObservableCollection<ObservableString>
                {
                    "Add setup text here"
                },
                Questions = new Answers
                {
                    Good = "Insert good text here",
                    Neutral = "Insert neutral text here",
                    Bad = "Insert bad text here"
                }
            };
        
        public static explicit operator ScenarioString(Scenario scenario)
        {
            return new ScenarioString
            {
                Name = scenario.Name,
                Theme = scenario.Theme,
                Setup = scenario.Setup.Select(setup => setup.TheString).ToList(),
                Answers = (AnswersString)scenario.Questions
            };
        }
    }

    public class Answers
    {
        public ObservableString Good { get; set; }
        public ObservableString Bad { get; set; }
        public ObservableString Neutral { get; set; }

        public static explicit operator AnswersString(Answers displayText)
        {
            return new AnswersString
            {
                Good = displayText.Good.TheString,
                Neutral = displayText.Neutral.TheString,
                Bad = displayText.Neutral.TheString
            };
        }
    }

    public class Response
    {
        public string Title { get; set; }
        public ResponseType Responses { get; set; }

        public static Response CreateEmpty(string scenario, int number) =>
            new Response
            {
                Title = $"{scenario}_{number:D2}",
                Responses = new ResponseType
                {
                    Good = "Insert good text here",
                    Neutral = "Insert neutral text here",
                    Bad = "Insert bad text here"
                }
            };

        public static explicit operator ResponseString(Response response)
        {
            return new ResponseString
            {
                Title = response.Title,
                Responses = (ResponseTypeString)response.Responses
            };
        }
    }

    public class ResponseType
    {
        public ObservableString Good { get; set; }
        public ObservableString Bad { get; set; }
        public ObservableString Neutral { get; set; }

        public static explicit operator ResponseTypeString(ResponseType displayText)
        {
            return new ResponseTypeString
            {
                Good = displayText.Good.TheString,
                Neutral = displayText.Neutral.TheString,
                Bad = displayText.Neutral.TheString
            };
        }
    }
}
