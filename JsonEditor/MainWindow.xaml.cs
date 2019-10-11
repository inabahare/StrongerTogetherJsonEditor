using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JsonEditor.Annotations;
using JsonEditor.Helpers;

namespace JsonEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        ObservableCollection<Response> Responses { get; set; }

        private Response _selectedResponse;

        private MoralityResponse _goodMorality;
        private MoralityResponse _neutralMorality;
        private MoralityResponse _badMorality;

        public Response SelectedResponse
        {
            get => _selectedResponse;
            set
            {
                if (_selectedResponse == value)
                    return;
                _selectedResponse = value;

                ChangeMoralityOptions();
                Sections.UpdateLayout();
                OnPropertyChanged(nameof(SelectedResponse));
            }
        }
        public MoralityResponse GoodMorality
        {
            get => _goodMorality;
            set
            {
                if (_goodMorality == value)
                    return;
                _goodMorality = value;
                OnPropertyChanged(nameof(GoodMorality));
            }
        }
        public MoralityResponse NeutralMorality
        {
            get => _neutralMorality;
            set
            {
                if (_neutralMorality == value)
                    return;
                _neutralMorality = value;
                OnPropertyChanged(nameof(NeutralMorality));
            }
        }
        public MoralityResponse BadMorality
        {
            get => _badMorality;
            set
            {
                if (_badMorality == value)
                    return;
                _badMorality = value;
                OnPropertyChanged(nameof(BadMorality));
            }
        }

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
