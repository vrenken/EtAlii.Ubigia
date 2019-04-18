namespace EtAlii.Ubigia.Windows.Management
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Security.Cryptography;
    using System.Text;
    using EtAlii.xTechnology.Mvvm;

    internal class ConnectionSettingsPersister : BindableBase
    {
        private readonly ConnectionDialogViewModel _viewModel;

        public ConnectionSettingsPersister(ConnectionDialogViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void Load(out string password, BinaryReader reader)
        {
            var formatter = new BinaryFormatter();

            var currentSettings = (ConnectionSettings)formatter.Deserialize(reader.BaseStream);
            _viewModel.Address = currentSettings.Address;
            _viewModel.Account = currentSettings.Account;
            _viewModel.Transport = (TransportType)Enum.Parse(typeof(TransportType), currentSettings.TransportType);
            password = currentSettings.Password;
            _viewModel.IsTested = reader.ReadBoolean();
            _viewModel.RememberPassword = reader.ReadBoolean();
            int totalServers = reader.ReadInt32();

            var previousSettings = new List<ConnectionSettings>();
            for (int i = 0; i < totalServers; i++)
            {
                var previousSetting = (ConnectionSettings)formatter.Deserialize(reader.BaseStream);
                previousSettings.Add(previousSetting);
            }
            _viewModel.PreviousSettings = previousSettings;
        }

        private void Save(string password, BinaryWriter writer)
        {
            var formatter = new BinaryFormatter();
            
            var currentSettings = DetermineCurrentSettings(password);
            formatter.Serialize(writer.BaseStream, currentSettings);
            writer.Write(_viewModel.IsTested);
            writer.Write(_viewModel.RememberPassword);
            var previousSettings = DeterminePreviousSettings(password);
            writer.Write(previousSettings.Count);
            foreach (var previousSetting in previousSettings)
            {
                formatter.Serialize(writer.BaseStream, previousSetting);
            }
        }

        private ConnectionSettings DetermineCurrentSettings(string password)
        {
            var currentSettings = new ConnectionSettings
            {
                Address = _viewModel.Address,
                Account = _viewModel.Account,
                Password = _viewModel.RememberPassword ? password : null,
                TransportType = _viewModel.Transport.ToString(),
            };
            return currentSettings;
        }

        private List<ConnectionSettings> DeterminePreviousSettings(string password)
        {
            var previousSettings = new List<ConnectionSettings>();
            if (_viewModel.PreviousSettings != null)
            {
                previousSettings.AddRange(_viewModel.PreviousSettings);
            }
            if (_viewModel.IsTested) 
            {
                var currentSettings = DetermineCurrentSettings(password);
                previousSettings.RemoveAll(s => s.Address == currentSettings.Address);
                previousSettings.Add(currentSettings);
            }
            return previousSettings;
        }

        public void Load(out string password)
        {
            password = String.Empty;
            try
            {
                using (var userStore = IsolatedStorageFile.GetUserStoreForAssembly())
                {
                    if (userStore.FileExists(_viewModel.ConfigurationFileName))
                    {
                        using (var fileStream = userStore.OpenFile(_viewModel.ConfigurationFileName, FileMode.Open, FileAccess.Read))
                        {
                            using (var stream = ToEncryptedStream(fileStream, CryptoStreamMode.Read))
                            {
                                using (var reader = new BinaryReader(stream))
                                {
                                    Load(out password, reader);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                // Catch all is on purpose.
                _viewModel.Address = String.Empty;
                _viewModel.Account = String.Empty;
                password = String.Empty;
                _viewModel.PreviousSettings = new ConnectionSettings[] { };
            }
        }

        public void Save(string password)
        {
            try
            {
                using (var userStore = IsolatedStorageFile.GetUserStoreForAssembly())
                {
                    using (var fileStream = userStore.OpenFile(_viewModel.ConfigurationFileName, FileMode.Create, FileAccess.Write))
                    {
                        using (var stream = ToEncryptedStream(fileStream, CryptoStreamMode.Write))
                        {
                            using (var writer = new BinaryWriter(stream))
                            {
                                Save(password, writer);
                            }
                        }
                    }
                }
            }
            catch
            {
                // Catch all is on purpose.
            }
        }

        private Stream ToEncryptedStream(Stream stream, CryptoStreamMode mode)
        {
            var cryptic = new AesCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes("baadfood"),
                IV = Encoding.ASCII.GetBytes("foodbaad"),
            };

            return new CryptoStream(stream, mode == CryptoStreamMode.Read ? cryptic.CreateDecryptor() : cryptic.CreateEncryptor(), mode);
        }
    }
}
