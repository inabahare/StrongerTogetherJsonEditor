using JsonEditor.Views;

namespace JsonEditor
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var languageSelectionPage = new LanguageSelection();
            Content.Content = languageSelectionPage;
        }
    }
}
