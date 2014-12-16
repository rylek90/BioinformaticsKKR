using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace BioinformaticsKKR.Core.ViewModel.Converters
{
    public class SequenceColorConverter : IValueConverter
    {
        public static Dictionary<char, Brush> BrushesDictionary = new Dictionary<char, Brush>()
        {
            {'A', Brushes.Red},
            {'G', Brushes.Green},
            {'C', Brushes.Blue},
            {'T', Brushes.MediumOrchid},
            {'U', Brushes.Purple},
            {'*', Brushes.Black},
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is char) || !BrushesDictionary.ContainsKey((char)value))
                return BrushesDictionary['*'];

            return BrushesDictionary[(char)value];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}