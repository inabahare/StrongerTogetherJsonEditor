using System.Windows.Controls;
using JsonEditor.Helpers;

namespace JsonEditor.Views
{
    /// <summary>
    /// Interaction logic for LanguageSelection.xaml
    /// </summary>
    public partial class LanguageSelection : Page
    {
        public LanguageSelection()
        {
            var strongerTogetherFinder = new StrongerTogetherFinder();
            var languages =
                strongerTogetherFinder.GetLanguagesThatAlreadyExist();

            InitializeComponent();

            LanguageContainer.ItemsSource = languages;
        }
    }
}
