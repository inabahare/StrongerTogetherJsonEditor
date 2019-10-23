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
        }

        void AddNewMorality(object sender, EventArgs e)
        {
            var moralityToAdd = ((ComboBoxItem)ToAdd.SelectedItem).Name;

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

            var newScene =
                Scenario.CreateEmpty(moralityToAdd, newMoralityNumber);

            var response =
                Responses.FirstOrDefault(r=> r.Title.ToLower() == newScene.Name.ToLower());

            if (response == null)
                response = Response.CreateEmpty(moralityToAdd, newMoralityNumber);

            SelectedScene = newScene;

            Scenarios.Add(newScene);
            Responses.Add(response);

            SortResponses();
        }

        void RemoveMorality(object sender, EventArgs e)
        {
            var indexOfSceneToRemove =
                Scenarios.IndexOf(SelectedScene);

            var newScene = 
                SelectedScene == Scenarios.Last() ? // If last element to be removed
                    Scenarios[indexOfSceneToRemove - 1] : // Pick the previous 
                    Scenarios[indexOfSceneToRemove + 1];  // Pick the next

            Scenarios.Remove(SelectedScene);

            SelectedScene = newScene;
        }

        void SortResponses()
        {
            var ordered = 
                (from r in Responses
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
                Scenes = new ObservableCollection<Scenario>(Scenarios),
                Responses = Responses.ToList()
            };

            Json.SaveToFile(newEvaluation);
        }

        void AddNewIntro(object sender, EventArgs e)
        {
            var newString = new ObservableString
            {
                TheString = "Change me!"
            };

            SelectedScene.Setup.Add(newString);
        }
    }
}
