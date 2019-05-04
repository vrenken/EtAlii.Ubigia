namespace EtAlii.Ubigia.Windows
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for DataConnectionDialogWindow.xaml
    /// </summary>
    public partial class ConnectionDialogWindow
    {
        public ConnectionDialogWindow()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AddressComboBox.Text))
            {
                AddressComboBox.Focus();
            }
            else if (string.IsNullOrWhiteSpace(AccountTextBox.Text))
            {
                AccountTextBox.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                PasswordBox.Focus();
            }
            else if (string.IsNullOrWhiteSpace(SpaceTextBox.Text))
            {
                SpaceTextBox.Focus();
            }

            Activate();
        }
    }
}
