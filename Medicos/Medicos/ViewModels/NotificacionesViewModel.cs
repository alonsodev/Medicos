using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Views;
    using System.Linq;
    using Medicos.Services;
    using System.Collections.ObjectModel;
    using Medicos.Models;

    public class NotificacionesViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        private bool isRefreshing;
        private ObservableCollection<NotificacionItemViewModel> notificaciones;
        #endregion

        #region Properties
        public ObservableCollection<NotificacionItemViewModel> Notificaciones
        {
            get { return this.notificaciones; }
            set { SetValue(ref this.notificaciones, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        #endregion

        #region Constructors
        public NotificacionesViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.LoadNotificaciones();
        }
        #endregion

        #region Methods
        private async void LoadNotificaciones()
        {
            this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Aceptar");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var response = await this.apiService.GetNotificaciones();

            if (response == null)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Algo estuvo mal, por favor intente después.",
                    "Aceptar");
                return;
            }

            MainViewModel.GetInstance().NotificacionesList = (List<Notificacion>)response;
            this.Notificaciones = new ObservableCollection<NotificacionItemViewModel>(
                this.ToNotificacionItemViewModel());
            this.IsRefreshing = false;
        }

        private IEnumerable<NotificacionItemViewModel> ToNotificacionItemViewModel()
        {
            return MainViewModel.GetInstance().NotificacionesList.Select(l => new NotificacionItemViewModel
            {
                Fecha = l.Fecha,
                Mensaje = l.Mensaje
            });
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadNotificaciones);
            }
        }

        #endregion
    }
}
