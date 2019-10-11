using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JsonEditor.Annotations;
using JsonEditor.Helpers;

namespace JsonEditor
{
    public partial class MainWindow : INotifyPropertyChanged
    {
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();

            var evaluations = Json.LoadFromFile();
            Responses = new ObservableCollection<Response>(evaluations.Responses);

            Sections.ItemsSource = Responses;
            
            // Sections.SelectionChanged += ChangeDialog;
            SelectedResponse = evaluations.Responses.First();
            ChangeMoralityOptions();
        }

        void ChangeMoralityOptions()
        {
            if (SelectedResponse == null)
                return; // I have no idea why I need this

            GoodMorality    = SelectedResponse.Responses.Find(r => r.Name == "Good");
            NeutralMorality = SelectedResponse.Responses.Find(r => r.Name == "Neutral");
            BadMorality     = SelectedResponse.Responses.Find(r => r.Name == "Bad");
        }

        void AddNewMorality(object sender, EventArgs e)
        {
            var moralityToAdd = ((ComboBoxItem)ToAdd.SelectedItem).Name as string;

            if (moralityToAdd == null)
                return;

            int newMoralityNumber = 1;

            var previousChoice =
                Responses.Where(response => response.Title.Contains(moralityToAdd));
            

            // If there already are choices
            if (previousChoice.Count() != 0)
            {
                var previousMoralityNumber =
                    previousChoice.Select(response => response.Title)
                                  .Last()
                                  .Remove(0, moralityToAdd.Length + 1); // + 1 to remove the underscore as well

                newMoralityNumber = int.Parse(previousMoralityNumber) + 1;
            }


            var newResponse = new Response
            {
                Title = $"{moralityToAdd}_{newMoralityNumber.ToString("D2")}",
                Theme = "New Theme",
                Responses = new List<MoralityResponse> {
                    new MoralityResponse {Name = "Good"},
                    new MoralityResponse {Name = "Neutral"},
                    new MoralityResponse {Name = "Bad"},
                }
            };

            SelectedResponse = newResponse;
            Responses.Add(newResponse);
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
