using JsonEditor.Helpers;
using JsonEditor.Views;

namespace JsonEditor
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();


            var strongerTogetherFinder = new DataManager();

            var languages =
                strongerTogetherFinder.GetLanguagesThatAlreadyExist();

            var languageSelectionPage = new LanguageSelection(languages);

            Content.Content = languageSelectionPage;
        }
    }
}
