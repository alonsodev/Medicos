using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.ViewModels
{
    using Medicos.Views;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Xamarin.Forms;

    public class BaseViewModel : INotifyPropertyChanged
    {
    
        public BaseViewModel()
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<string> _tiposDocumento;
        public ObservableCollection<string> TiposDocumento
        {
            get { return _tiposDocumento; }
            set
            {
                if (Equals(value, _tiposDocumento)) return;
                _tiposDocumento = value;
                OnPropertyChanged(nameof(TiposDocumento));
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
            {
                return;
            }

            backingField = value;
            OnPropertyChanged(propertyName);
        }

        
    }
}
