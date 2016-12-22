﻿namespace EtAlii.Servus.Windows
{
    using System;
    using System.Windows;
    using Fluent;

    /// <summary>
    /// Interaction logic for DataConnectionDialogWindow.xaml
    /// </summary>
    public partial class ConnectionDialogWindow : RibbonWindow
    {
        public ConnectionDialogWindow()
        {
            InitializeComponent();
        }

        private void ConnectionSettingsDialogWindow_OnLoaded(object sender, RoutedEventArgs e)
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
