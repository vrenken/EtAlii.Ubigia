namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Windows;
    using System.Windows.Media;

    public partial class GraphViewBase
    {
        private DependencyObject _watermark;
        private IDisposable _watermarkFinder;

        private void InitializeWatermarkRemoval()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                Opacity = 0.0f;

                _watermarkFinder = Observable.Interval(TimeSpan.FromSeconds(1))
                    .ObserveOnDispatcher()
                    .Subscribe(i =>
                    {
                        var watermark = FindVisualChildren(this, "DefaultLayer").FirstOrDefault();
                        if (watermark != _watermark)
                        {
                            RemoveVisibilityHandler();
                            _watermark = watermark;
                            ClearWatermark();
                            AddVisibilityHandler();
                            Opacity = 1.0f;
                            _watermarkFinder.Dispose();
                        }
                    });
            }
        }

        private void RemoveVisibilityHandler()
        {
            if (_watermark != null)
            {
                var descriptor = DependencyPropertyDescriptor.FromProperty(VisibilityProperty, typeof(UIElement));
                descriptor.RemoveValueChanged(_watermark, _watermark_IsVisibleChanged);
            }
        }

        private void AddVisibilityHandler()
        {
            if (_watermark != null)
            {
                var descriptor = DependencyPropertyDescriptor.FromProperty(VisibilityProperty, typeof(UIElement));
                descriptor.AddValueChanged(_watermark, _watermark_IsVisibleChanged);
            }
        }

        private void ClearWatermark()
        {
            if (_watermark != null)
            {
                var descriptor = DependencyPropertyDescriptor.FromProperty(VisibilityProperty, typeof(UIElement));
                descriptor.SetValue(_watermark, Visibility.Collapsed);
            }
        }

        void _watermark_IsVisibleChanged(object sender, EventArgs e)
        {
            RemoveVisibilityHandler();
            ClearWatermark();
            AddVisibilityHandler();
        }

        private IEnumerable<DependencyObject> FindVisualChildren(DependencyObject depObj, string typeName)
        {
            if (depObj != null)
            {
                for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    var child = VisualTreeHelper.GetChild(depObj, i);
                    if (child.GetType().Name == typeName)
                    {
                        yield return child;
                    }

                    foreach (var childOfChild in FindVisualChildren(child, typeName))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
