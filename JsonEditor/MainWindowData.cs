using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using JsonEditor.Annotations;

namespace JsonEditor
{
    public partial class MainWindow : INotifyPropertyChanged
    {
        ObservableCollection<Scenario> Scenarios { get; set; }
        ObservableCollection<Response> Responses { get; set; }

        ObservableCollection<string> _setupData;

        Response _selectedResponse;
        Scenario _selectedScene;

        ResponseType _responseTypes;
        private Questions _selectedQuestios;


        public ObservableCollection<string> SetupData
        {
            get => _setupData;
            set
            {
                if (_setupData == value)
                    return;

                _setupData = value;
                OnPropertyChanged(nameof(SetupData));
            }
        }
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

        public Scenario SelectedScene
        {
            get => _selectedScene;
            set
            {
                if (_selectedScene == value)
                    return;

                _selectedScene = value;
                SelectedQuestions = value.Questions;

                SelectedResponse = Responses.FirstOrDefault(response => response.Title.ToLower() == value.Name.ToLower());

                Sections.UpdateLayout();
                OnPropertyChanged(nameof(SelectedScene));
            }
        }

        public ResponseType ResponseTypes
        {
            get => _responseTypes;
            set
            {
                if (_responseTypes == value)
                    return;
                _responseTypes = value;
                OnPropertyChanged(nameof(ResponseTypes));
            }
        }

        public Questions SelectedQuestions
        {
            get => _selectedQuestios;
            set
            {
                _selectedQuestios = value;
                OnPropertyChanged(nameof(SelectedQuestions));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
