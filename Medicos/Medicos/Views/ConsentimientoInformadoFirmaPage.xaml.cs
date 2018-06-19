using Medicos.ViewModels;
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
	public partial class ConsentimientoInformadoFirmaPage : ContentPage
	{
		public ConsentimientoInformadoFirmaPage ()
		{
			InitializeComponent ();
            

        }

        async void btnTerminarOnClick(object sender, EventArgs args)
        {
            if (signature.IsBlank)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes firmar el consentemiento.",
                    "Aceptar");
                return;
            }

            var vm = (MainViewModel)BindingContext; // Warning, the BindingContext View <-> ViewModel is already set

            vm.ConsentimientoInformadoFirma.SignatureFromStream = async () =>
            {
                if (signature.Points.Count() > 0)
                {
                    using (var stream = await signature.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png, 0.075F))
                    {
                        return await ImageConverter.ReadFully(stream);
                    }
                }

                return await Task.Run(() => (byte[])null);
            };

            vm.ConsentimientoInformadoFirma.Terminar();
        }            

        public static class ImageConverter
        {
            public static async Task<byte[]> ReadFully(Stream input)
            {
                byte[] buffer = new byte[16 * 1024];
                using (var ms = new MemoryStream())
                {
                    int read;
                    while ((read = await input.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                    return ms.ToArray();
                }
            }
        }

        void swtFirmaPacienteToogled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                swtFirmaFamiliar.IsToggled = false;
            }
        }

        void swtFirmaFamiliarToogled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                swtFirmaPaciente.IsToggled = false;
            }
        }
    }
}