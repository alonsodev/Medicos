using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.Models
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using System.Windows.Input;
    using Views;
    using Xamarin.Forms;

    public class NotificacionItemViewModel : Notificacion
    {

        #region Commands
        public ICommand SelectNotificacionCommand
        {
            get
            {
                return new RelayCommand(SelectNotificacion);
            }
        }

        private async void SelectNotificacion()
        {
            /*MainViewModel.GetInstance().Land = new LandViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new LandTabbedPage());*/
        }
        #endregion
    }
}
