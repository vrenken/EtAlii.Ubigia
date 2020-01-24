namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Windows;
    using ICSharpCode.AvalonEdit;
    using ICSharpCode.AvalonEdit.Document;

    public sealed class GraphScriptLanguageEditor : TextEditor
    {
        private bool _updateDocumentFromCode = true;

        // ReSharper disable InconsistentNaming
        public string Source { get => (string)GetValue(SourceProperty); set => SetValue(SourceProperty, value); }
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(nameof(Source), typeof(string), typeof(GraphScriptLanguageEditor), new PropertyMetadata(string.Empty));
        // ReSharper restore InconsistentNaming

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
            else if (e.Property == SourceProperty && _updateDocumentFromCode && Document != null)
            {
                Document.TextChanged -= OnDocumentTextChanged;
                Document.Text = e.NewValue as string;
                Document.TextChanged += OnDocumentTextChanged;
            }
        }

        private void OnDocumentTextChanged(object sender, EventArgs e)
        {
            _updateDocumentFromCode = false;
            Source = Document.Text;
            _updateDocumentFromCode = true;
        }
    }
}
