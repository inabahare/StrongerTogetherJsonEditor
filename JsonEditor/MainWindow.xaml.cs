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
            GoodMorality = SelectedResponse.Responses.Find(r => r.Name == "Good");
            NeutralMorality = SelectedResponse.Responses.Find(r => r.Name == "Neutral");
            BadMorality = SelectedResponse.Responses.Find(r => r.Name == "Bad");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
