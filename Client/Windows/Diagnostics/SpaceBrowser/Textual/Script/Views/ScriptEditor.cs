namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using ICSharpCode.AvalonEdit;
    using ICSharpCode.AvalonEdit.Document;
    using System;
    using System.Windows;

    public sealed class ScriptEditor : TextEditor
    {
        private bool _updateDocumentFromCode = true;

        public string Code
        {
            get { return (string)GetValue(CodeProperty); }
            set { SetValue(CodeProperty, value); }
        }
        public static readonly DependencyProperty CodeProperty = DependencyProperty.Register("Code", typeof(string), typeof(ScriptEditor), new PropertyMetadata(String.Empty));

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
            else if (e.Property == CodeProperty && _updateDocumentFromCode)
            {
                if (Document != null)
                {
                    Document.TextChanged -= OnDocumentTextChanged;
                    Document.Text = e.NewValue as string;
                    Document.TextChanged += OnDocumentTextChanged;
                }
            }
        }

        private void OnDocumentTextChanged(object sender, EventArgs e)
        {
            _updateDocumentFromCode = false;
            Code = Document.Text;
            _updateDocumentFromCode = true;
        }
    }
}
