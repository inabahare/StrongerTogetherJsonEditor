using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JsonEditor
{
    public class ObservableString : INotifyPropertyChanged
    {
        private string _theString;

        public string TheString
        {
            get => _theString; set
            {
                if (_theString == value) return;
                _theString = value;
                NotifyOfPropertyChange();
            }
        }

        public static implicit operator string(ObservableString stringHolder) => 
            stringHolder.TheString;

        public static implicit operator ObservableString(string s) => 
            new ObservableString { TheString = s };

        public void NotifyOfPropertyChange([CallerMemberName] string propertyName = "") => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
