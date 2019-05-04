namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Text;
    using System.Windows;

    public partial class ScriptExecutionView
    {
        public ObservableCollection<string> ExecutionStatus
        {
            get => (ObservableCollection<string>)GetValue(ExecutionStatusProperty);
            set => SetValue(ExecutionStatusProperty, value);
        }
        public static readonly DependencyProperty ExecutionStatusProperty = DependencyProperty.Register("ExecutionStatus", typeof(ObservableCollection<string>), typeof(ScriptExecutionView), new PropertyMetadata(new ObservableCollection<string>(), OnExecutionStatusChanged));

        public object Source
        {
            get => GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(object), typeof(ScriptExecutionView), new PropertyMetadata(null, OnSourceChanged));
        
        public ScriptExecutionView()
        {
            InitializeComponent();
        }

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is IGraphScriptLanguageViewModel)
            {
                ((ScriptExecutionView)d).DataContext = e.NewValue;
            }
        }

        private static void OnExecutionStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scriptExecutionView = (ScriptExecutionView) d;

            if (e.OldValue is ObservableCollection<string> oldValue)
            {
                oldValue.CollectionChanged -= scriptExecutionView.UpdateExecutionStatusTextBox;
            }

            if (e.NewValue is ObservableCollection<string> newValue)
            {
                newValue.CollectionChanged += scriptExecutionView.UpdateExecutionStatusTextBox;
            }
            scriptExecutionView.ExecutionStatusTextBox.Text = string.Empty;
        }

        private void UpdateExecutionStatusTextBox(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:

                    var sb = new StringBuilder();
                    foreach (string line in e.NewItems)
                    {
                        sb.AppendLine(line);
                    }
                    ExecutionStatusTextBox.Text = sb.ToString();

                    var isAtEnd = Math.Abs(ExecutionStatusTextBox.VerticalOffset - (ExecutionStatusTextBox.ExtentHeight - ExecutionStatusTextBox.ViewportHeight)) < 10;
                    if (isAtEnd)
                    {
                        ExecutionStatusTextBox.ScrollToEnd();
                        ExecutionStatusTextBox.SelectionStart = ExecutionStatusTextBox.Text.Length;
                    }
                    break;
                default:
                    ExecutionStatusTextBox.Text = string.Empty;
                    break;
            }
        }
    }
}
