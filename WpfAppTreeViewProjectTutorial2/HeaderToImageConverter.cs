using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WpfAppTreeViewProjectTutorial2
{

    //Converting a string to a image class
    //Not supporting converter back to string.
    //Converts a full path to a specific image type of drive, folder, or file

        
    
    [ValueConversion(typeof(string), typeof(BitmapImage))]//Does not work.
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Find the full path
            var path = (string)value;

            //Make sure the path is not null
            if (path == null)
                return null;
            

            //Getting the name of the file or folder
            var name = MainWindow.GetFileFolderName(path);

            var image = "Images/Drive-icon_image.png";

            if (string.IsNullOrEmpty(name))
                image = "Images/Drive-icon_image.png";
            else if (new FileInfo(path).Attributes.HasFlag(FileAttributes.Directory))
                image = "Images/Closed _Folder_image.png";

            //This accesses resources in WPF as URI's use the string = ($"pack://application:,,,/{Whatever resourse you want}") ) 
            //return new BitmapImage(new Uri($"pack://application:,,,/Images/Drive-icon_image.png"));

            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
