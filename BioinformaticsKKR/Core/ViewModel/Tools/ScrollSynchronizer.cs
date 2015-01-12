using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BioinformaticsKKR.Core.ViewModel.Tools
{
    public class ScrollSynchronizer : DependencyObject
    {
        public static readonly DependencyProperty ScrollGroupProperty =
            DependencyProperty.RegisterAttached("ScrollGroup", typeof (string), typeof (ScrollSynchronizer),
                new PropertyMetadata(OnScrollGroupChanged));

        private static readonly Dictionary<ScrollViewer, string> ScrollViewers = new Dictionary<ScrollViewer, string>();

        private static readonly Dictionary<string, double> HorizontalScrollOffsets = new Dictionary<string, double>();

        private static readonly Dictionary<string, double> VerticalScrollOffsets = new Dictionary<string, double>();

        public static void SetScrollGroup(DependencyObject obj, string scrollGroup)
        {
            obj.SetValue(ScrollGroupProperty, scrollGroup);
        }

        public static string GetScrollGroup(DependencyObject obj)
        {
            return (string) obj.GetValue(ScrollGroupProperty);
        }

        private static void OnScrollGroupChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = d as ScrollViewer;
            if (scrollViewer != null)
            {
                if (!string.IsNullOrEmpty((string) e.OldValue))
                {
                    if (ScrollViewers.ContainsKey(scrollViewer))
                    {
                        ScrollViewers.Remove(scrollViewer);
                    }
                }

                if (!string.IsNullOrEmpty((string) e.NewValue))
                {
                    if (HorizontalScrollOffsets.ContainsKey((string) e.NewValue))
                    {
                        scrollViewer.ScrollToHorizontalOffset(HorizontalScrollOffsets[(string) e.NewValue]);
                    }
                    else
                    {
                        HorizontalScrollOffsets.Add((string) e.NewValue, scrollViewer.HorizontalOffset);
                    }

                    if (VerticalScrollOffsets.ContainsKey((string) e.NewValue))
                    {
                        scrollViewer.ScrollToVerticalOffset(VerticalScrollOffsets[(string) e.NewValue]);
                    }
                    else
                    {
                        VerticalScrollOffsets.Add((string) e.NewValue, scrollViewer.VerticalOffset);
                    }

                    ScrollViewers.Add(scrollViewer, (string) e.NewValue);
                    scrollViewer.ScrollChanged += ScrollViewer_ScrollChanged;
                }
            }
        }


        private static void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.VerticalChange != 0 || e.HorizontalChange != 0)
            {
                var changedScrollViewer = sender as ScrollViewer;
                Scroll(changedScrollViewer);
            }
        }


        private static void Scroll(ScrollViewer changedScrollViewer)
        {
            var group = ScrollViewers[changedScrollViewer];
            VerticalScrollOffsets[group] = changedScrollViewer.VerticalOffset;
            HorizontalScrollOffsets[group] = changedScrollViewer.HorizontalOffset;

            foreach (var scrollViewer in ScrollViewers.Where(s => s.Value == group && s.Key != changedScrollViewer))
            {
                if (scrollViewer.Key.VerticalOffset != changedScrollViewer.VerticalOffset)
                {
                    scrollViewer.Key.ScrollToVerticalOffset(changedScrollViewer.VerticalOffset);
                }

                if (scrollViewer.Key.HorizontalOffset != changedScrollViewer.HorizontalOffset)
                {
                    scrollViewer.Key.ScrollToHorizontalOffset(changedScrollViewer.HorizontalOffset);
                }
            }
        }
    }
}