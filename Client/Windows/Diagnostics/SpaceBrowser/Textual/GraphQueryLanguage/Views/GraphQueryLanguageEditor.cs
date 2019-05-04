namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Windows;
    using ICSharpCode.AvalonEdit;
    using ICSharpCode.AvalonEdit.Document;

    public sealed class GraphQueryLanguageEditor : TextEditor
    {
        private bool _updateDocumentFromSource = true;

        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(nameof(Source), typeof(string), typeof(GraphQueryLanguageEditor), new PropertyMetadata(string.Empty));

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == DocumentProperty)
            {
                var oldDocument = e.OldValue as TextDocument;
                var newDocument = e.NewValue as TextDocument;

                if (oldDocument != null)
                {
                    oldDocument.TextChanged -= OnDocumentTextChanged;
                }

                if (newDocument != null)
                {
                    newDocument.TextChanged += OnDocumentTextChanged;
                }
            }
            else if (e.Property == SourceProperty && _updateDocumentFromSource)
            {
                if (Document != null)
                {
                    Document.TextChanged -= OnDocumentTextChanged;
                    Document.Text = e.NewValue as string ?? string.Empty;
                    Document.TextChanged += OnDocumentTextChanged;
                }
            }
        }

        private void OnDocumentTextChanged(object sender, EventArgs e)
        {
            _updateDocumentFromSource = false;
            Source = Document.Text;
            _updateDocumentFromSource = true;
        }
    }
}
