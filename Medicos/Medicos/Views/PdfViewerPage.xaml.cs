using Medicos.PDF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Medicos.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PdfViewerPage : ContentPage
	{
		public PdfViewerPage ()
		{
			InitializeComponent ();
            pdfViewerControl.DocumentSaveInitiated += (s, e) => {
                System.IO.Stream st = e.SaveStream;
                ISave isave = DependencyService.Get<ISave>();
                isave.Save("Output.pdf", "application/pdf", (MemoryStream)st);
            };
        }
    }
}