using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using Bio.Phylogenetics;

namespace BioinformaticsKKR.Core.ViewModel.Converters
{
    public class TreeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bioTree = value as Tree;

            if (bioTree == null)
            {
                return null;
            }


            return bioTree.Root.Children.Select(children => new TreeViewItem
            {
                Header = string.Format("{0} - {1}", children.Key, children.Value),
            }).ToList();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}