using System.Collections.ObjectModel;
using System.ComponentModel;

namespace JsonEditor
{
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
    }
}
