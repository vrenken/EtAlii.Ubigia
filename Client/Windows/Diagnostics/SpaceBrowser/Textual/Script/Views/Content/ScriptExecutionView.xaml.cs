namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser.Views
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Windows;
    using System.Windows.Controls;
    using EtAlii.Ubigia.Client.Windows.Diagnostics;

    public partial class ScriptExecutionView : UserControl
    {
        public ObservableCollection<string> ExecutionStatus
        {
            get { return (ObservableCollection<string>)GetValue(ExecutionStatusProperty); }
            set { SetValue(ExecutionStatusProperty, value); }
        }
        public static readonly DependencyProperty ExecutionStatusProperty = DependencyProperty.Register("ExecutionStatus", typeof(ObservableCollection<string>), typeof(ScriptExecutionView), new PropertyMetadata(new ObservableCollection<string>(), OnExecutionStatusChanged));

        public object Source
        {
            get { return (object)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(object), typeof(ScriptExecutionView), new PropertyMetadata(null, OnSourceChanged));
        
        public ScriptExecutionView()
        {
            InitializeComponent();
        }

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is IScriptViewModel)
            {
                ((ScriptExecutionView)d).DataContext = e.NewValue;
            }
        }

        private static void OnExecutionStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scriptExecutionView = (ScriptExecutionView) d;

            var oldValue = e.OldValue as ObservableCollection<string>;
            if (oldValue != null)
            {
                oldValue.CollectionChanged -= scriptExecutionView.UpdateExecutionStatusTextBox;
            }
            var newValue = e.NewValue as ObservableCollection<string>;
            if (newValue != null)
            {
                newValue.CollectionChanged += scriptExecutionView.UpdateExecutionStatusTextBox;
            }
            scriptExecutionView.ExecutionStatusTextBox.Text = String.Empty;
        }

        private void UpdateExecutionStatusTextBox(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:

                    var isAtEnd = Math.Abs(ExecutionStatusTextBox.VerticalOffset - (ExecutionStatusTextBox.ExtentHeight - ExecutionStatusTextBox.ViewportHeight)) < 10;
                    foreach (string line in e.NewItems)
                    {
                        if (ExecutionStatusTextBox.Text == String.Empty)
                        {
                            ExecutionStatusTextBox.Text = line;
                        }
                        else
                        {
                            ExecutionStatusTextBox.Text += Environment.NewLine + line;
                        }
                    }
                    if (isAtEnd)
                    {
                        ExecutionStatusTextBox.ScrollToEnd();
                        ExecutionStatusTextBox.SelectionStart = ExecutionStatusTextBox.Text.Length;
                    }
                    break;
                default:
                    ExecutionStatusTextBox.Text = String.Empty;
                    break;
            }
        }
    }
}
