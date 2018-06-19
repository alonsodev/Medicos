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
    using System.Reflection;
    using System.IO;
    using Syncfusion.Pdf.Parsing;
    using Syncfusion.Pdf.Graphics;
    using Syncfusion.Drawing;

    public class InformeConsolidadoViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        private bool isRefreshing;

        private ObservableCollection<PacienteCantidad> pacientesCantidad;
        #endregion

        #region Properties
        public ObservableCollection<PacienteCantidad> PacientesCantidad
        {
            get { return this.pacientesCantidad; }
            set { SetValue(ref this.pacientesCantidad, value); }
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
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        #endregion

        #region Constructors
        public InformeConsolidadoViewModel()
        {
            this.apiService = new ApiService();

            //this.IsRemembered = true;
            this.IsEnabled = true;
            this.LoadPacientes();
           
        }

        private async void LoadPacientes()
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

            var response = await this.apiService.GetCantidadesPacientesConsolidado();

            if (response == null)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Algo estuvo mal, por favor intente después.",
                    "Aceptar");
                return;
            }

    
            this.PacientesCantidad = new ObservableCollection<PacienteCantidad>(response);
            this.IsRefreshing = false;
        }
        #endregion

        #region Commands

        public ICommand OpenConsolidadoPdfRowCommand
        {
            get
            {
                return new RelayCommand<object>(OpenConsolidadoPdfRow);
            }
        }

        
        private async void OpenConsolidadoPdfRow(object obj)
        {
            string v = "entro";
        }

        public ICommand OpenConsolidadoPdfCommand
        {
            get
            {
                return new RelayCommand(OpenConsolidadoPdf);
            }
        }

        private async void OpenConsolidadoPdf()
        {
            var response = await this.apiService.GetPaciente("", "0");

            if (response == null)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Algo estuvo mal, por favor intente después.",
                    "Aceptar");
                return;
            }

            List<PacienteView> vListPacientes = new List<PacienteView>(response);

            var vMainViewModel = MainViewModel.GetInstance();
            Stream pdfConsolidado = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream("Medicos.Assets.consolidado.pdf");

            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(pdfConsolidado);
            PdfGraphics graphics = loadedDocument.Pages[0].Graphics;

            PdfBrush brush = new PdfSolidBrush(Syncfusion.Drawing.Color.Black);

            //Set the font

            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 8);
            PdfFont fontNombre = new PdfStandardFont(PdfFontFamily.Helvetica, 5);

            //Draw the text
            //Line 1
            graphics.DrawString("N/A", font, brush, new PointF(333, 55));

            //line 2
            graphics.DrawString(vMainViewModel.Token.NombreApellidos, font, brush, new PointF(333, 64));

            //line 3
            graphics.DrawString(vMainViewModel.Token.Ciudad, font, brush, new PointF(333, 75));

            int yPos = 166;

            foreach (var paciente in vListPacientes)
            {
                graphics.DrawString(paciente.Nombres, fontNombre, brush, new PointF(52, yPos+2));
                graphics.DrawString(paciente.Eps, font, brush, new PointF(170, yPos));
                graphics.DrawString(paciente.Medicamento, font, brush, new PointF(219, yPos));
                graphics.DrawString(paciente.Fecha_Ingreso_PSP, font, brush, new PointF(277, yPos));
                graphics.DrawString(paciente.Fecha_Educacion_Tramites_Admin, font, brush, new PointF(353, yPos));
                graphics.DrawString(paciente.Fecha_Entrega_Muestra_Med, font, brush, new PointF(436, yPos));
                graphics.DrawString(paciente.Fecha_Ultimo_Seguimiento_Telefonico, font, brush, new PointF(532, yPos));
                graphics.DrawString(paciente.Fecha_Ultimo_Seguimient_Presencial, font, brush, new PointF(601, yPos));
                graphics.DrawString(paciente.Fecha_Inicio_Tratamiento, font, brush, new PointF(673, yPos));

                yPos = yPos + 10;
            }

            


            MemoryStream memoryStream = new MemoryStream();

            loadedDocument.Save(memoryStream);
            byte[] m_Bytes = ReadToEnd(memoryStream);
            string base64pdf = Convert.ToBase64String(m_Bytes);

            vMainViewModel.PdfViewer = new PdfViewerViewModel();
            vMainViewModel.PdfViewer.PdfDocumentStream = memoryStream;
            await Application.Current.MainPage.Navigation.PushAsync(new PdfViewerPage());
        }

        static byte[] ReadToEnd(Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
        #endregion
    }
}
