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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JsonEditor.Annotations;

namespace JsonEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        ObservableCollection<Response> Responses { get; set; }

        private Response _selectedResponse;

        public Response SelectedResponse
        {
            get => _selectedResponse;
            set
            {
                if (_selectedResponse == value)
                    return;
                _selectedResponse = value;
                OnPropertyChanged(nameof(SelectedResponse));
            }
    }

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

            DataContext = this;
            InitializeComponent();
            Sections.ItemsSource = Responses;
            
            // Sections.SelectionChanged += ChangeDialog;
            SelectedResponse = evaluations.Responses.First();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
