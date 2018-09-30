using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using Medicos.PDF;
using QuickLook;
using UIKit;

namespace Medicos.iOS
{
    public class SaveIOS : ISave
    {
        public async Task Save(string filename, string contentType, MemoryStream stream)

        {

            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string filePath = Path.Combine(path, filename);

            try

            {

                FileStream fileStream = File.Open(filePath, FileMode.Create);

                stream.Position = 0;

                stream.CopyTo(fileStream);

                fileStream.Flush();

                fileStream.Close();

            }

            catch (Exception e)

            {

            }

            UIViewController currentController = UIApplication.SharedApplication.KeyWindow.RootViewController;

            while (currentController.PresentedViewController != null)

                currentController = currentController.PresentedViewController;

            UIView currentView = currentController.View;

            QLPreviewController preview = new QLPreviewController();

            QLPreviewItem item = new QLPreviewItemBundle(filename, filePath);

            preview.DataSource = new PreviewControllerDS(item);

            //UIViewController uiView = currentView as UIViewController;

            currentController.PresentViewController(preview, true, null);

        }

    }

    public class PreviewControllerDS : QLPreviewControllerDataSource
    {
        private QLPreviewItem _item;

        public PreviewControllerDS(QLPreviewItem item)
        {
            _item = item;
        }

        public override nint PreviewItemCount(QLPreviewController controller)
        {
            return (nint)1;
        }

        public override IQLPreviewItem GetPreviewItem(QLPreviewController controller, nint index)
        {
            return _item;
        }
    }


    public class QLPreviewItemFileSystem : QLPreviewItem
    {
        string _fileName, _filePath;

        public QLPreviewItemFileSystem(string fileName, string filePath)
        {
            _fileName = fileName;
            _filePath = filePath;
        }

        public override string ItemTitle
        {
            get
            {
                return _fileName;
            }
        }
        public override NSUrl ItemUrl
        {
            get
            {
                return NSUrl.FromFilename(_filePath);
            }
        }
    }

    public class QLPreviewItemBundle : QLPreviewItem
    {
        string _fileName, _filePath;
        public QLPreviewItemBundle(string fileName, string filePath)
        {
            _fileName = fileName;
            _filePath = filePath;
        }

        public override string ItemTitle
        {
            get
            {
                return _fileName;
            }
        }
        public override NSUrl ItemUrl
        {
            get
            {
                var documents = NSBundle.MainBundle.BundlePath;
                var lib = Path.Combine(documents, _filePath);
                var url = NSUrl.FromFilename(lib);
                return url;
            }
        }
    }
}