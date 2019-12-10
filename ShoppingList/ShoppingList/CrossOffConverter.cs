using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ShoppingList
{
    public class CrossOffConverter : IValueConverter
    {
        public object Convert(object? value, Type? targetType = null, object? 
            parameter = null, CultureInfo? culture = null)
        {
            if(value is true)
            {
                return TextDecorations.Strikethrough;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
