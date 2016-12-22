namespace EtAlii.Servus.Windows.Diagnostics.SpaceBrowser.Views
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using EtAlii.Servus.Client.Windows.Diagnostics;
    using Syncfusion.Windows.Shared;
    using Syncfusion.Windows.Tools.Controls;

    public partial class ScriptErrorsView : UserControl
    {
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value ?? new object[0]); }
        }
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(ScriptErrorsView), new PropertyMetadata(new object[0]));

        public object Source
        {
            get { return (object)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(object), typeof(ScriptErrorsView), new PropertyMetadata(null, OnSourceChanged));

        public ScriptErrorsView()
        {
            InitializeComponent();
        }

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is ScriptViewModel)
            {
                ((ScriptErrorsView)d).DataContext = e.NewValue;
            }
        }
    }
}
