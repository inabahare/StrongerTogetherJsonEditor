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

        Response _selectedResponse;
        Scenario _selectedScene;

        ResponseType _responseTypes;

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

        public Scenario SelectedScene
        {
            get => _selectedScene;
            set
            {
                if (_selectedScene == value)
                    return;

                _selectedScene = value;

                SelectedResponse = Responses.First(response => response.Title.ToLower() == value.Name.ToLower());

                ChangeMoralityOptions();
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

        public string GoodOption
        {
            get => _selectedResponse?.Responses?.Good;
            set
            {
                _selectedResponse.Responses.Good = value;
                OnPropertyChanged(nameof(GoodOption));
            }
        }

        public string NeutralOption
        {
            get => _selectedResponse?.Responses?.Neutral;
            set
            {
                _selectedResponse.Responses.Neutral= value;
                OnPropertyChanged(nameof(NeutralOption));
            }
        }

        public string BadOption
        {
            get => _selectedResponse?.Responses?.Bad;
            set
            {
                _selectedResponse.Responses.Bad = value;
                OnPropertyChanged(nameof(BadOption));
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
