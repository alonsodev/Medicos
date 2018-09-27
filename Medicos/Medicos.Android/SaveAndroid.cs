using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using Medicos.Droid;
using Medicos.PDF;
using Xamarin.Forms;
[assembly: Xamarin.Forms.Dependency(typeof(SaveAndroid))]
namespace Medicos.Droid
{
    
    public class SaveAndroid : ISave
    {
        public async Task Save(string fileName, String contentType, MemoryStream stream)

        {

            string root = null;

            if (Android.OS.Environment.IsExternalStorageEmulated)

            {

                root = Android.OS.Environment.ExternalStorageDirectory.ToString();

            }

            else

                root = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

            Java.IO.File myDir = new Java.IO.File(root + "/medicosapp");

            myDir.Mkdir();

            Java.IO.File file = new Java.IO.File(myDir, fileName);

            if (file.Exists()) file.Delete();

            try

            {

                FileOutputStream outs = new FileOutputStream(file);

                outs.Write(stream.ToArray());

                outs.Flush();

                outs.Close();

            }

            catch (Exception e)

            {

            }

            if (file.Exists())

            {

                Android.Net.Uri path = Android.Net.Uri.FromFile(file);

                string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(file).ToString());

                string mimeType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);

                Intent intent = new Intent(Intent.ActionView);

                intent.SetDataAndType(path, mimeType);
                
                Forms.Context.StartActivity(Intent.CreateChooser(intent, "Escoger App"));

            }

        }
    }
}