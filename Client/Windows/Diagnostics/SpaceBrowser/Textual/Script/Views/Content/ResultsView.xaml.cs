﻿namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser.Views
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using EtAlii.Ubigia.Client.Windows.Diagnostics;

    public partial class ResultsView : UserControl
    {
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value ?? new object[0]); }
        }
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(ResultsView), new PropertyMetadata(new object[0], OnItemsSourceChanged));

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(ResultsView), new PropertyMetadata(null));

        public object Source
        {
            get { return (object)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(object), typeof(ResultsView), new PropertyMetadata(null, OnSourceChanged));

        public ResultsView()
        {
            InitializeComponent();
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var resultView = (ResultsView) d;
            resultView.SelectedItem = null;
        }

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is IScriptViewModel)
            {
                ((ResultsView) d).DataContext = e.NewValue;
            }
        }
    }
}
