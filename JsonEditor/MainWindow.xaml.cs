using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using JsonEditor.Helpers;

namespace JsonEditor
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();

            var evaluations = Json.LoadFromFile();
            Responses = new ObservableCollection<Response>(evaluations.Responses);
            Scenarios = new ObservableCollection<Scenario>(evaluations.Scenes);

            
            // Sections.SelectionChanged += ChangeDialog;
            SelectedScene = evaluations.Scenes.First();
            SelectedResponse = evaluations.Responses.First(response => response.Title.ToLower() == SelectedScene.Name.ToLower());

            Sections.ItemsSource = Scenarios;
            ChangeMoralityOptions();
        }

        void ChangeMoralityOptions()
        {
            if (SelectedResponse == null)
                return; // I have no idea why I need this

            GoodOption = SelectedResponse.Responses.Good;
            NeutralOption = SelectedResponse.Responses.Neutral;
            BadOption = SelectedResponse.Responses.Bad;
        }

        void AddNewMorality(object sender, EventArgs e)
        {
            var moralityToAdd = ((ComboBoxItem)ToAdd.SelectedItem).Name as string;

            if (moralityToAdd == null)
                return;

            int newMoralityNumber = 1; // Set it to one in case there aren't any

            var previousChoice =
                Scenarios
                    .Where(scenario => scenario.Name.Contains(moralityToAdd))
                    .ToList();
            

            // If there already are choices
            if (previousChoice.Count() != 0)
            {
                var previousMoralityNumber =
                    previousChoice
                        .Select(scenario=> scenario.Name)
                        .Last()
                        .Remove(0, moralityToAdd.Length + 1); // + 1 to remove the underscore as well

                newMoralityNumber = int.Parse(previousMoralityNumber) + 1;
            }

            var newScene = new Scenario
            {
                Name = $"{moralityToAdd}_{newMoralityNumber:D2}",
                Theme = "New theme",
                Setup = new List<string>(),
                Questions = new Questions
                {
                    Good = "Insert good text here",
                    Neutral = "Insert neutral text here",
                    Bad = "Insert bad text here"
                }
            };


            var response =
                Responses.FirstOrDefault(r=> r.Title.ToLower() == newScene.Name.ToLower());

            if (response == null)
                response = new Response
                {
                    Title = $"{moralityToAdd}_{newMoralityNumber:D2}",
                    Responses = new ResponseType
                    {
                        Good = "Insert good text here",
                        Neutral = "Insert neutral text here",
                        Bad = "Insert bad text here"
                    }
                };

            SelectedScene = newScene;
            SelectedResponse = response;

            Scenarios.Add(newScene);
            Responses.Add(response);

            ChangeMoralityOptions();
            SortResponses();
        }

        void SortResponses()
        {
            var ordered = (from r in Responses
                           orderby r.Title
                           select r).ToList();

            Responses.Clear();
            foreach (var r in ordered)
                Responses.Add(r);
        }

        void Save(object sender, EventArgs e)
        {
            var newEvaluation = new DisplayText
            {
                Scenes = Scenarios.ToList(),
                Responses = Responses.ToList()
            };

            Json.SaveToFile(newEvaluation);
        }
    }
}
