using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace JsonEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Response> Responses { get; set; }

        Response selectedResponse { get; set; }

        public MainWindow()
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".json"
            };

            dlg.ShowDialog();
            var text = File.ReadAllText(dlg.FileName);
            var evaluations = JsonConvert.DeserializeObject<Evaluations>(text);

            Responses = new ObservableCollection<Response>(evaluations.Responses);

            InitializeComponent();
            DataContext = this;
            Sections.ItemsSource = Responses;
            
            // Sections.SelectionChanged += ChangeDialog;
            selectedResponse = evaluations.Responses.First();
        }

        void ChangeDialog(object sender, SelectionChangedEventArgs e)
        {
            var listBoxSender = sender as ListBox;
            //var response = listBoxSender.SelectedItem as Response;
            //selectedResponse = response;
        }
    }
}
