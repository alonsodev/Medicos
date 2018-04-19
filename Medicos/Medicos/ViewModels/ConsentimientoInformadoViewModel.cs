using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Views;
    public class ConsentimientoInformadoViewModel
    {
        public ConsentimientoInformadoViewModel()
        {
        }

        #region Commands
        public ICommand SiguienteCommand
        {
            get
            {
                return new RelayCommand(Siguiente);
            }
        }

        private async void Siguiente()
        {
            MainViewModel.GetInstance().ConsentimientoInformadoFirma = new ConsentimientoInformadoFirmaViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ConsentimientoInformadoFirmaPage());
        }

        #endregion
    }


}
