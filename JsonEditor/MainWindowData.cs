using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
using JsonEditor.Annotations;

namespace JsonEditor
{
    public partial class MainWindow : INotifyPropertyChanged
    {
        ObservableCollection<Scenario> Scenarios { get; set; }
        ObservableCollection<Response> Responses { get; set; }

        Response _selectedResponse;
        Scenario _selectedScene;

        Questions _selectedQuestios;

        int _introTextIndex;
        ObservableString _introText;

        public ObservableString IntroText 
        { 
            get => _introText;
            set 
            {
                if (_introText == value)
                    return;

                _introText = value;
                OnPropertyChanged(nameof(IntroText));
            } 
        }

        int IntroTextIndex // TODO: Fix this!
        {
            get => _introTextIndex;
            set
            {
                if (_introTextIndex == value)
                    return;

                // Prevent stack over and underflow
                if (value == SelectedScene.Setup.Count)
                    value--;
                else if (value == -1)
                    value++;


                IntroText = SelectedScene.Setup[value];
                _introTextIndex = value;

                SelectedIntroIndex.Text =   // I couldn't get two way binding to work here 
                    value.ToString(); // so this is the fix
            }
        }

        int IntroTextCount
        {
            set
            {
                IntroCount.Text = value.ToString();
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
                if (value == null)
                    return;

                if (_selectedScene == value)
                    return;

                _selectedScene = value;
                
                SelectedQuestions = value.Questions;
                IntroText = value.Setup.First();
                IntroTextCount = value.Setup.Count();
                IntroTextIndex = 0;
                
                SelectedResponse = Responses.FirstOrDefault(response => response.Title.ToLower() == value.Name.ToLower());

                Sections.UpdateLayout();
                OnPropertyChanged(nameof(SelectedScene));
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
