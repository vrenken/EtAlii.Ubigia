namespace EtAlii.Ubigia.Windows
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using EtAlii.Ubigia.Windows.Mvvm;

    internal class ConnectionSettingsPersister : BindableBase
    {
        private readonly ConnectionDialogViewModel _viewModel;

        public ConnectionSettingsPersister(ConnectionDialogViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void Load(out string password, BinaryReader reader)
        {
            _viewModel.Address = reader.ReadString();
            _viewModel.Account = reader.ReadString();
            _viewModel.Space = reader.ReadString();
            _viewModel.Transport = (TransportType)Enum.Parse(typeof(TransportType), reader.ReadString());
            password = reader.ReadString();
            _viewModel.IsTested = reader.ReadBoolean();
            _viewModel.RememberPassword = reader.ReadBoolean();
            var totalServers = reader.ReadInt32();

            var previousSettings = new List<ConnectionSettings>();
            for (var i = 0; i < totalServers; i++)
            {
                var previousSetting = new ConnectionSettings
                {
                    Address = reader.ReadString(),
                    Account = reader.ReadString(),
                    Space = reader.ReadString(),
                    Password = reader.ReadString(),
                    TransportType = reader.ReadString()
                };
                
                previousSettings.Add(previousSetting);
            }
            _viewModel.PreviousSettings = previousSettings;
        }

        private void Save(string password, BinaryWriter writer)
        {
            var currentSettings = DetermineCurrentSettings(password);
            
            writer.Write(currentSettings.Address);
            writer.Write(currentSettings.Account);
            writer.Write(currentSettings.Space);
            writer.Write(currentSettings.TransportType);
            writer.Write(currentSettings.Password);
            writer.Write(_viewModel.IsTested);
            writer.Write(_viewModel.RememberPassword);
            var previousSettings = DeterminePreviousSettings(password);
            writer.Write(previousSettings.Count);
            foreach (var previousSetting in previousSettings)
            {
                writer.Write(previousSetting.Address);
                writer.Write(previousSetting.Account);
                writer.Write(previousSetting.Space);
                writer.Write(previousSetting.Password);
                writer.Write(previousSetting.TransportType);
            }
        }

        private ConnectionSettings DetermineCurrentSettings(string password)
        {
            var currentSettings = new ConnectionSettings
            {
                Address = _viewModel.Address,
                Account = _viewModel.Account,
                Password = _viewModel.RememberPassword ? password : null,
                Space = _viewModel.Space,
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
            password = string.Empty;
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
                _viewModel.Address = string.Empty;
                _viewModel.Account = string.Empty;
                password = string.Empty;
                _viewModel.Space = string.Empty;
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
            var keyLength = 128;
            var key = Encoding.ASCII.GetBytes("BaaDFoodFromDeadBeef").Take(keyLength / 8).ToArray();
            var iv = Encoding.ASCII.GetBytes("FoodBaaDFromBeefDead").Take(keyLength / 8).ToArray();
            var cryptic = new AesCryptoServiceProvider {KeySize = keyLength};

            return new CryptoStream(stream, mode == CryptoStreamMode.Read ? cryptic.CreateDecryptor(key, iv) : cryptic.CreateEncryptor(key, iv), mode);
        }
    }
}
