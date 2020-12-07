namespace EtAlii.Ubigia.Windows.Settings
{
    using System;
    using System.Runtime.CompilerServices;
    using EtAlii.Ubigia.Windows.Mvvm;
    using Microsoft.Win32;

    public abstract class BindableSettingsBase : BindableBase
    {
        private readonly string _registryKey;

        protected BindableSettingsBase()
            : this(string.Empty)
        {
        }

        protected BindableSettingsBase(string registryKeyName)
        {
            var format = string.IsNullOrWhiteSpace(registryKeyName) ? "Software\\{0}" : "Software\\{0}\\{1}";
            _registryKey = string.Format(format, SettingsKey.ProductName, registryKeyName);
        }

        protected RegistryKey GetSubKey(string subKeyName)
        {
            using var productKey = Registry.CurrentUser.CreateSubKey(_registryKey);
            return productKey!.CreateSubKey(subKeyName);
        }

        protected T GetValue<T>(ref T storage, T defaultValue, [CallerMemberName] string propertyName = null)
            where T : class
        {
            if (storage == null)
            {
                using var registryKey = Registry.CurrentUser.CreateSubKey(_registryKey);
                storage = (T)GetValue(ref defaultValue, propertyName, registryKey);
                SetValue(registryKey, storage, propertyName);
            }
            return storage;
        }

        protected T GetValue<T>(ref T? storage, T defaultValue, [CallerMemberName] string propertyName = null)
            where T: struct
        {
            if (!storage.HasValue)
            {
                using var registryKey = Registry.CurrentUser.CreateSubKey(_registryKey);
                storage = (T)GetValue(ref defaultValue, propertyName, registryKey);
                SetValue(registryKey, storage.Value, propertyName);
            }
            return storage.Value;
        }

        private object GetValue<T>(ref T defaultValue, string propertyName, RegistryKey productKey)
        {
            object value;

            if (typeof(T) == typeof(bool))
            {
                value = productKey.GetValue(propertyName, (bool)(object)defaultValue ? 1 : 0);
                value = (int)value! == 1;
            }
            else if (typeof(T) == typeof(int))
            {
                value = productKey.GetValue(propertyName, defaultValue);
                value = (int)value!;
            }
            else if (typeof(T) == typeof(double))
            {
                value = (byte[])productKey.GetValue(propertyName, BitConverter.GetBytes((double)(object)defaultValue));
                value = BitConverter.ToDouble((byte[])value!, 0);
            }
            else if (typeof(T) == typeof(string))
            {
                value = productKey.GetValue(propertyName, defaultValue);
                value = (string)value;
            }
            else
            {
                throw new NotSupportedException();
            }
            return value;
        }

        protected void SetValue(RegistryKey registryKey, object newValue, string propertyName = null)
        {
            if (newValue == null)
            {
                registryKey.DeleteValue(propertyName);
            }
            else if (newValue is bool)
            {
                registryKey.SetValue(propertyName, newValue, RegistryValueKind.DWord);
            }
            else if (newValue is string)
            {
                registryKey.SetValue(propertyName, newValue, RegistryValueKind.String);
            }
            else if (newValue is int)
            {
                registryKey.SetValue(propertyName, newValue, RegistryValueKind.DWord);
            }
            else if (newValue is double)
            {
                registryKey.SetValue(propertyName, BitConverter.GetBytes((double)newValue), RegistryValueKind.Binary);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        protected override void NotifyPropertyChanged(object sender, object oldValue, object newValue, string propertyName = null)
        {
            using (var registryKey = Registry.CurrentUser.CreateSubKey(_registryKey))
            {
                SetValue(registryKey, newValue, propertyName);
            }
            base.NotifyPropertyChanged(sender, oldValue, newValue, propertyName);
        }

        public void Delete()
        {
            Registry.CurrentUser.DeleteSubKeyTree(_registryKey);
        }
    }
}
