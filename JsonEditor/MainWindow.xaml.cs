using System;
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
    public enum ScenarioChoice
    {
        Good = 2,
        Neutral = 1,
        Bad = 0
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        ObservableCollection<Response> Responses { get; set; }

        private Response _selectedResponse;
        private MoralityResponse _selectedMorality;


        public Response SelectedResponse
        {
            get => _selectedResponse;
            set
            {
                if (_selectedResponse == value)
                    return;
                _selectedResponse = value;
                Sections.UpdateLayout();
                OnPropertyChanged(nameof(SelectedResponse));
            }
        }
        public MoralityResponse SelectedMorality
        {
            get => _selectedMorality;
            set
            {
                if (_selectedMorality == value)
                    return;
                _selectedMorality = value;
                OnPropertyChanged(nameof(SelectedMorality));
            }
        }



        public MainWindow()
        {

            var evaluations = Json.LoadFromFile();
            Responses = new ObservableCollection<Response>(evaluations.Responses);

            DataContext = this;
            InitializeComponent();
            Sections.ItemsSource = Responses;
            
            // Sections.SelectionChanged += ChangeDialog;
            SelectedResponse = evaluations.Responses.First();
        }

        void ChangeMorality(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var morality = button.Content as string;
            SelectedMorality = SelectedResponse.Responses.Find(m => m.Name == morality);
            Console.WriteLine(SelectedMorality);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
