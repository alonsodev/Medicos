using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Views;
    using Medicos.Services;
    using Medicos.Models;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using System.IO;
    using System.Reflection;
    using Syncfusion.Pdf.Parsing;
    using Syncfusion.Pdf.Graphics;
    using Syncfusion.Drawing;


    public class ConsentimientoInformadoFirmaViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        private bool firmaPaciente;
        private bool firmaFamiliar;

        #endregion

        #region Properties
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

        public bool FirmaPaciente
        {
            get { return this.firmaPaciente; }
            set { SetValue(ref this.firmaPaciente, value); }
        }

        public bool FirmaFamiliar
        {
            get { return this.firmaFamiliar; }
            set { SetValue(ref this.firmaFamiliar, value); }
        }

        #endregion

        #region Constructors
        public ConsentimientoInformadoFirmaViewModel()
        {
            this.apiService = new ApiService();
            this.FirmaPaciente = true;
            this.IsEnabled = true;
            this.loadData();
        }
        #endregion

        #region Methods


        private async void loadData()
        {
            
        }

        public Func<Task<byte[]>> SignatureFromStream { get; set; }
        public byte[] Signature { get; set; }

        public ICommand MyCommand => new Command(async () =>
        {
            Signature = await SignatureFromStream();
           
            // Signature should be != null
        });
        #endregion

        #region Commands
        public ICommand TerminarCommand
        {
            get
            {
                return new RelayCommand(Terminar);
            }
        }

        public ICommand OpenConsentimientoPdfCommand
        {
            get
            {
                return new RelayCommand(OpenConsentimientoPdf);
            }
        }
        
        private async void OpenConsentimientoPdf()
        {
            /*MainViewModel.GetInstance().PdfViewer = new PdfViewerViewModel();
            MainViewModel.GetInstance().PdfViewer.PdfDocumentStream = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream("Medicos.Assets.consentimiento.pdf");
            await Application.Current.MainPage.Navigation.PushAsync(new PdfViewerPage());*/
            await Task.Run(() => { Device.OpenUri(new Uri("http://www.pdf995.com/samples/pdf.pdf")); });
        }

        

        public async void Terminar()
        {
            var vMainViewModel = MainViewModel.GetInstance();
            Signature = await SignatureFromStream();
            FillPacienteEntity();

            var paciente = await this.apiService.ReportarPaciente(vMainViewModel.Paciente);
            Int32 intPacienteId = 0;
            if (paciente.Id.Contains("id:"))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Éxito",
                    "El paciente fue reportado satisfactoriamente.",
                    "Aceptar");

                vMainViewModel.MainMenu = new MainMenuViewModel();

                await Application.Current.MainPage.Navigation.PushAsync(new MainMenuPage());
            }
            else
            {
                if (paciente != null)
                {
                    await Application.Current.MainPage.DisplayAlert(
                   "Error",
                   paciente.Id,
                   "Aceptar");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(
                   "Error",
                   "Algo estuvo mal, por favor intente después.",
                   "Aceptar");
                }
            }
        }

        private void FillPacienteEntity()
        {
            /*var vMainViewModel = MainViewModel.GetInstance();
            
            string base64pdf = Convert.ToBase64String(Signature);
            Paciente oPaciente = vMainViewModel.Paciente;
            oPaciente.f = base64pdf;*/

            CreatePDF();
            //oPaciente.


        }

        private void CreatePDF()
        {
            var vMainViewModel = MainViewModel.GetInstance();
            Paciente oPaciente = vMainViewModel.Paciente;

            try
            {
                Stream docStream = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream("Medicos.Assets.consentimiento.pdf");

                PdfLoadedDocument loadedDocument = new PdfLoadedDocument(docStream);
                PdfGraphics graphics = loadedDocument.Pages[0].Graphics;

                PdfBrush brush = new PdfSolidBrush(Syncfusion.Drawing.Color.Black);

                //Set the font

                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);

                //Draw the text
                //Line 1
                graphics.DrawString(DateTime.Now.Day.ToString(), font, brush, new PointF(243, 69));
                graphics.DrawString(DateTime.Now.Month.ToString(), font, brush, new PointF(270, 69));
                graphics.DrawString(DateTime.Now.Year.ToString(), font, brush, new PointF(297, 69));

                //line 2
                graphics.DrawString(oPaciente.CiudadStr, font, brush, new PointF(370, 69));

                graphics.DrawString(oPaciente.Nombre, font, brush, new PointF(112, 114));

                graphics.DrawString("X", font, brush, new PointF(381, 114));

                graphics.DrawString(oPaciente.Documento, font, brush, new PointF(516, 114));

                //line 3
                graphics.DrawString(oPaciente.Fecha_nacimiento.Split('-')[2], font, brush, new PointF(112, 133));
                graphics.DrawString(oPaciente.Fecha_nacimiento.Split('-')[1], font, brush, new PointF(142, 133));
                graphics.DrawString(oPaciente.Fecha_nacimiento.Split('-')[0], font, brush, new PointF(162, 133));

                graphics.DrawString(oPaciente.Telefono_celular, font, brush, new PointF(222, 133));

                graphics.DrawString(oPaciente.Telefono, font, brush, new PointF(370, 133));

                graphics.DrawString(oPaciente.Correo, font, brush, new PointF(477, 133));

                //line4
                graphics.DrawString(oPaciente.PatologiaStr, font, brush, new PointF(354, 150));

                graphics.DrawString(oPaciente.EpsStr, font, brush, new PointF(52, 150));

                //line 5
                if (oPaciente.MedicamentoStr.ToLower().Contains("zoladex"))
                {
                    graphics.DrawString("X", font, brush, new PointF(169, 168));
                }
                else if (oPaciente.MedicamentoStr.ToLower().Contains("faslodex"))
                {
                    graphics.DrawString("X", font, brush, new PointF(231, 168));
                }
                else if (oPaciente.MedicamentoStr.ToLower().Contains("iressa"))
                {
                    graphics.DrawString("X", font, brush, new PointF(279, 168));
                }
                else if (oPaciente.MedicamentoStr.ToLower().Contains("foxiga"))
                {
                    graphics.DrawString("X", font, brush, new PointF(332, 168));
                }
                else if (oPaciente.MedicamentoStr.ToLower().Contains("xigduo"))
                {
                    graphics.DrawString("X", font, brush, new PointF(395, 168));
                }
                else if (oPaciente.MedicamentoStr.ToLower().Contains("brilinta"))
                {
                    graphics.DrawString("X", font, brush, new PointF(451, 168));
                }
                else if (oPaciente.MedicamentoStr.ToLower().Contains("symbicort"))
                {
                    graphics.DrawString("X", font, brush, new PointF(513, 168));
                }
                else if (oPaciente.MedicamentoStr.ToLower().Contains("bydureon"))
                {
                    graphics.DrawString("X", font, brush, new PointF(575, 168));
                }

                //line 6
                graphics.DrawString(oPaciente.Cuidador, font, brush, new PointF(110, 210));

                if (oPaciente.Cuidador != "" && oPaciente.Cuidador != "N/A")
                {
                    graphics.DrawString("X", font, brush, new PointF(381, 210));
                }

                graphics.DrawString(oPaciente.Documento_cuidador, font, brush, new PointF(516, 210));

                //line 7
                graphics.DrawString(oPaciente.Parentesco, font, brush, new PointF(78, 228));
                graphics.DrawString(oPaciente.Telefono_cuidador, font, brush, new PointF(349, 228));

                //line 8
                graphics.DrawString(vMainViewModel.Token.NombreApellidos, font, brush, new PointF(110, 269));
                graphics.DrawString(vMainViewModel.Token.UserName, font, brush, new PointF(338, 269));

                //line 9
                graphics.DrawString(vMainViewModel.Token.Especialidad, font, brush, new PointF(82, 288));
                graphics.DrawString(oPaciente.Centro_atencion, font, brush, new PointF(386, 288));

                //line 10
                graphics.DrawString(vMainViewModel.Token.NombreApellidos, font, brush, new PointF(70, 459));

                //line 11
                if (FirmaPaciente)
                {
                    graphics.DrawString(oPaciente.Documento, font, brush, new PointF(128, 762));
                }


                if (FirmaFamiliar)
                {
                    graphics.DrawString(oPaciente.Documento_cuidador, font, brush, new PointF(400, 762));
                }

                Stream stream = new MemoryStream(Signature);
                PdfBitmap image = new PdfBitmap(stream);

                if (FirmaPaciente)
                {
                    graphics.DrawImage(image, new PointF(128, 710));
                }

                if (FirmaFamiliar)
                {
                    graphics.DrawImage(image, new PointF(400, 710));
                }


                MemoryStream memoryStream = new MemoryStream();

                loadedDocument.Save(memoryStream);
                byte[] m_Bytes = ReadToEnd(memoryStream);
                string base64pdf = Convert.ToBase64String(m_Bytes);
                oPaciente.Consentimiento_pdf = base64pdf;
                loadedDocument.Close(true);
            }
            catch (Exception ex)
            {
                oPaciente.Consentimiento_pdf = "N/A";
            }
            

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
