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
    using Syncfusion.Pdf;
    using Medicos.PDF;

    public class InformeConsolidadoViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        private bool isBusy;
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
        public bool IsBusy
        {
            get { return this.isBusy; }
            set { SetValue(ref this.isBusy, value); }
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

            var response = await this.apiService.GetCantidadesPacientesConsolidado(DateTime.Now.Year.ToString());

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
            this.IsBusy = true;
            this.IsRunning = true;
            var response = await this.apiService.GetPaciente("", "0");

            if (response == null)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Algo estuvo mal, por favor intente después.",
                    "Aceptar");
                this.IsBusy = false;
                this.IsRunning = false;
                return;
            }

            List<PacienteView> vListPacientes = new List<PacienteView>(response);
            int maxByPage = 36;

            decimal dpages = vListPacientes.Count / (maxByPage + 0.0m);
            int pages = Decimal.ToInt32(Math.Truncate(dpages)) + (vListPacientes.Count % (maxByPage + 0.0) == 0 ? 0 : 1);

            var vMainViewModel = MainViewModel.GetInstance();
            Stream pdfConsolidado = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream("Medicos.Assets.consolidado_a4.pdf");

            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(pdfConsolidado);

            PdfLoadedPage loadedPage = loadedDocument.Pages[0] as PdfLoadedPage;
            
            //Create the template from the page.

            PdfTemplate template = loadedPage.CreateTemplate();
            PdfDocument document = new PdfDocument();
            
            int xRows = 0;

            for (int xPage=1;xPage<=pages; xPage++)
            {
                document.PageSettings.Size = new SizeF(1044f, 616f);
                document.PageSettings.Orientation = PdfPageOrientation.Landscape;


                PdfPage page = document.Pages.Add();
                PdfGraphics graphics = page.Graphics;

                //Draw the template

                graphics.DrawPdfTemplate(template, PointF.Empty, loadedPage.Size);

                /*
                PdfPageBase oPdfPage = loadedDocument.Pages[0];
                PdfTemplate template = new PdfTemplate(1, 1);

                PdfPageBase oPdfPageBase = loadedDocument.Pages.Add();
                PdfTemplate oPefTemlate = loadedDocument.Pages[0].CreateTemplate();
                oPdfPageBase.Graphics.DrawPdfTemplate(oPefTemlate, PointF.Empty, oPefTemlate.Size);

                PdfGraphics graphics = loadedDocument.Pages[0].Graphics;*/

                PdfBrush brush = new PdfSolidBrush(Syncfusion.Drawing.Color.Black);

                //Set the font

                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 8);
                PdfFont fontNombre = new PdfStandardFont(PdfFontFamily.Helvetica, 5);

                //Draw the text
                //Line 1
                graphics.DrawString("N/A", font, brush, new PointF(538, 55));//333

                //line 2
                graphics.DrawString(vMainViewModel.Token.NombreApellidos, font, brush, new PointF(538, 64));

                //line 3
                graphics.DrawString(vMainViewModel.Token.Ciudad, font, brush, new PointF(538, 75));

                int yPos = 160;
                int xPos = 52;

                int rowOfPage = 1;
                int counter = 1;
                bool yPosHandler = true;
                for (int xP=0; xP < maxByPage;xP++)
                {
                    var paciente = vListPacientes[xRows];
                    graphics.DrawString(paciente.Nombres, font, brush, new PointF(xPos, yPos));
                    graphics.DrawString(paciente.Eps, font, brush, new PointF(xPos + 216, yPos));
                    graphics.DrawString(paciente.Medicamento, font, brush, new PointF(xPos + 392, yPos));//218
                    graphics.DrawString(paciente.Fecha_Ingreso_PSP, font, brush, new PointF(xPos + 484, yPos));//277
                    graphics.DrawString(paciente.Fecha_Educacion_Tramites_Admin, font, brush, new PointF(xPos + 547, yPos));//353 
                    graphics.DrawString(paciente.Fecha_Entrega_Muestra_Med, font, brush, new PointF(xPos + 635, yPos));//436
                    graphics.DrawString(paciente.Fecha_Ultimo_Seguimiento_Telefonico, font, brush, new PointF(xPos + 710, yPos));//532 
                    graphics.DrawString(paciente.Fecha_Ultimo_Seguimient_Presencial, font, brush, new PointF(xPos + 776, yPos));//601 
                    graphics.DrawString(paciente.Fecha_Inicio_Tratamiento, font, brush, new PointF(xPos + 852, yPos));//673

                    if(counter == 3)
                    {
                        yPosHandler = !yPosHandler;
                        
                        counter = 0;
                    }

                    if (yPosHandler)
                    {
                        yPos = yPos + 10;
                    } else
                    {
                        yPos = yPos + 9;
                    }
                    
                    xRows++;
                    rowOfPage++;
                    counter++;
                    if (xRows == vListPacientes.Count) break;
                    //if (rowOfPage == maxByPage) break;
                }
            }

            MemoryStream memoryStream = new MemoryStream();

            document.Save(memoryStream);
            document.Close(true);
            byte[] m_Bytes = ReadToEnd(memoryStream);
            string base64pdf = Convert.ToBase64String(m_Bytes);
            //como grabar https://help.syncfusion.com/file-formats/pdf/working-with-xamarin

            ISave isave = DependencyService.Get<ISave>();
            await isave.Save("Output.pdf", "application/pdf", memoryStream);

            this.IsBusy = false;
            this.IsRunning = false;
            /*
            vMainViewModel.PdfViewer = new PdfViewerViewModel();
            vMainViewModel.PdfViewer.PdfDocumentStream = memoryStream;
            */
            //await Application.Current.MainPage.Navigation.PushAsync(new PdfViewerPage());
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
