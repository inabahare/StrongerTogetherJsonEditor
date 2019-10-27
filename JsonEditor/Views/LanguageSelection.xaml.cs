using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Navigation;
using JsonEditor.Helpers;

namespace JsonEditor.Views
{
    /// <summary>
    /// Interaction logic for LanguageSelection.xaml
    /// </summary>
    public partial class LanguageSelection : Page
    {
        private List<string> Languages { get; set; }

        public LanguageSelection(List<string> languages)
        {
            InitializeComponent();

            Languages = languages;
            LanguageContainer.ItemsSource = Languages;
        }

        public void LoadExistingLanguage(object sender, EventArgs e)
        {
            var selectedLanguage =
                LanguageContainer?.SelectedItem as string;

            GoToEditingScreen(selectedLanguage);
        }

        public void AddLanguageAndGoToEditingScreen(object sender, EventArgs e)
        {
            var newLanguage = 
                CreateNewLanguage();

            if (newLanguage == "")
                return;

            GoToEditingScreen(newLanguage);
        }

        string CreateNewLanguage()
        {
            var newLanguage = NewLanguage.Text;

            if (newLanguage.Length == 0)
                return "";

            Languages.Add(newLanguage);

            var displayText =
                DisplayText.CreateEmpty();

            DataManager.SaveToFile(newLanguage, displayText);
            return newLanguage;
        }

        void GoToEditingScreen(string language)
        {
            var navigationService = 
                NavigationService.GetNavigationService(this);

            navigationService?.Navigate(new Editor(language));
        }
    }
}
