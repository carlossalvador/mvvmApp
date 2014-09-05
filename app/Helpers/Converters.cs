using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace app.Helper
{
    [ValueConversion(typeof(object), typeof(List<object>))]
    public class SelectedItemToItemsSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            return new List<object>() { value };
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(string), typeof(BitmapFrame))]
    public class ImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType == typeof(ImageSource))
            {
                if (value is string)
                {
                    string str = (string)value;
                    //Se retorna un BitmapFrame para poder dejar libre al archivo que contiene la imagen
                    try
                    {
                        BitmapFrame res = BitmapFrame.Create(new Uri(str, UriKind.RelativeOrAbsolute), BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                        return res;
                    }
                    catch
                    {
                        BitmapFrame res = BitmapFrame.Create(new Uri(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\gimnasio\\appResources\\img\\imgError.jpg", UriKind.RelativeOrAbsolute), BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                        return res;
                    }
                }

                //creo que no se ocupara.. es para image source = URI
                //else if (value is Uri)
                //{
                //    Uri uri = (Uri)value;
                //    return new BitmapImage(uri);
                //}
            }
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(bool), typeof(bool))]
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    [ValueConversion(typeof(List<object>), typeof(Visibility))]
    public class EmptyListVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
                throw new InvalidOperationException("The target must be Visibility");

            if (value == null)
                return Visibility.Collapsed;
            else
            {
                ICollection list = value as ICollection;
                if (list != null)
                {
                    if (list.Count == 0)
                        return Visibility.Collapsed;
                    else
                        return Visibility.Visible;
                }
                else
                    return Visibility.Visible;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    [ValueConversion(typeof(ListViewItem), typeof(int))]
    public class IndexConverter : IValueConverter
    {
        public object Convert(object value, Type TargetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ListViewItem item = (ListViewItem)value;
            ListView listView = ItemsControl.ItemsControlFromItemContainer(item) as ListView;
            int index = listView.ItemContainerGenerator.IndexFromContainer(item);
            return index;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(int), typeof(bool))]
    public class NegativeToTrueConverter : IValueConverter
    {
        public object Convert(object value, Type TargetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (TargetType != typeof(bool))
                throw new InvalidOperationException("The target must be boolean");

            if ((int)value < 0)
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(int), typeof(SolidColorBrush))]
    public class DaysToColorConverter : IValueConverter
    {
        public object Convert(object value, Type TargetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //if ((int)value < 1)
            //    return new SolidColorBrush(Color.FromRgb(0xff, 0x22, 0x00));

            //if ((int)value < 8)
            //    return new SolidColorBrush(Color.FromRgb(0xff, 0xa5, 0x00));

            //return new SolidColorBrush(Color.FromRgb(0x22, 0x8b, 0x22));

            int daysToPay = ((DateTime)value).Subtract(DateTime.Today).Days;

            if (daysToPay < 1)
                return new SolidColorBrush(Color.FromRgb(0xff, 0x22, 0x00));

            if (daysToPay < 8)
                return new SolidColorBrush(Color.FromRgb(0xff, 0xa5, 0x00));

            return new SolidColorBrush(Color.FromRgb(0x22, 0x8b, 0x22));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(int), typeof(string))]
    public class DaysToSpeakConverter : IValueConverter
    {
        public object Convert(object value, Type TargetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((int)value < 0)
                return (((int)value)*(-1)).ToString() + " días de retraso";
                
            if ((int)value == 0)
                return "Hoy";

            return value.ToString() + " días";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
