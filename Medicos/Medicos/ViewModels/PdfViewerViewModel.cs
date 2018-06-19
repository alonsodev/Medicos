using System;
using System.Collections.Generic;

using System.Text;

namespace Medicos.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.ComponentModel;
    using System.IO;
    using System.Reflection;
    using System.Windows.Input;

    public class PdfViewerViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private Stream m_pdfDocumentStream;

        /// <summary>
        /// An event to detect the change in the value of a property.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public Stream pPdfDocStream;

        /// <summary>
        /// The PDF document stream that is loaded into the instance of the PDF viewer. 
        /// </summary>
        public Stream PdfDocumentStream
        {
            get
            {
                return m_pdfDocumentStream;
            }
            set
            {
                m_pdfDocumentStream = value;
                NotifyPropertyChanged("PdfDocumentStream");
            }
        }

        /// <summary>
        /// Constructor of the view model class
        /// </summary>
        public PdfViewerViewModel()
        {
            //Accessing the PDF document that is added as embedded resource as stream.
            m_pdfDocumentStream = pPdfDocStream;// typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream("Medicos.Assets.consentimiento.pdf");
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Commands
        public ICommand OnUnloadCommand
        {
            get
            {
                return new RelayCommand(OnUnload);
            }
        }

        private async void OnUnload()
        {
       
        }
        #endregion
    }
}
