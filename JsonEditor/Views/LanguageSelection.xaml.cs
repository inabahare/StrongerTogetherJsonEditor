using System;
using System.Collections.Generic;
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

            navigationService.Navigate(new Editor(language));
        }
    }
}
