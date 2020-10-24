﻿namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class ResultSet : INotifyPropertyChanged
    {
        public string Id { get; }

        public int Count { get => _count; set => SetProperty(ref _count, value); }
        private int _count;

        public bool ShowCount { get => _showCount; set => SetProperty(ref _showCount, value); }
        private bool _showCount;

        public ObservableCollection<Result> Results { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ResultSet(string id, ObservableCollection<Result> results)
        {
            Id = id;
            Results = results;
        }

        /// <summary>
        /// Checks if a property already matches a desired value.  Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="newValue">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected bool SetProperty<T>(ref T storage, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, newValue)) return false;

            var oldValue = storage;
            storage = newValue;
            NotifyPropertyChanged(this, oldValue, newValue, propertyName);

            return true;
        }


        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        /// <param name="sender"></param>
        /// <param name="oldValue"></param>
        protected virtual void NotifyPropertyChanged(object sender, object oldValue, object newValue, [CallerMemberName] string propertyName = null)
        {
            var eventHandler = PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}