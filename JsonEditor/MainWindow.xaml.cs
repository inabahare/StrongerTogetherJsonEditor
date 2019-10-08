﻿using System;
using System.Collections.Generic;
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
        Evaluations evaluations;

        public MainWindow()
        {
            InitializeComponent();
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".json"
            };

            dlg.ShowDialog();
            var text = File.ReadAllText(dlg.FileName);
            evaluations = JsonConvert.DeserializeObject<Evaluations>(text);

            Sections.ItemsSource = evaluations.Responses;
            Sections.SelectionChanged += ChangeDialog;
        }

        void ChangeDialog(object sender, SelectionChangedEventArgs e)
        {
            var listBoxSender = sender as ListBox;
            var response = listBoxSender.SelectedItem as Response;
            Console.WriteLine(response);
        }
    }
}