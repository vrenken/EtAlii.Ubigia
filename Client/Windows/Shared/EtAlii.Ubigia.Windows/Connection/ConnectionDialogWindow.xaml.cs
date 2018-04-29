namespace EtAlii.Ubigia.Windows
{
    using System;
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
            if (String.IsNullOrWhiteSpace(AddressComboBox.Text))
            {
                AddressComboBox.Focus();
            }
            else if (String.IsNullOrWhiteSpace(AccountTextBox.Text))
            {
                AccountTextBox.Focus();
            }
            else if (String.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                PasswordBox.Focus();
            }
            else if (String.IsNullOrWhiteSpace(SpaceTextBox.Text))
            {
                SpaceTextBox.Focus();
            }

            Activate();
        }
    }
}
