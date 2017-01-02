namespace EtAlii.Servus.Api
{
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Content_Rename : Structure
    {
        private readonly Dictionary<string, object> _properties = new Dictionary<string, object>();

        public Content_Rename()
        {

        }

        protected T GetProperty<T>(string propertyName)
            where T: struct
        {
            object value = default(T);
            _properties.TryGetValue(propertyName, out value);
            return (T)value;
        }

        /// <summary>
        /// Checks if a property already matches a desired value.  Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected bool SetProperty<T>(T newValue, [CallerMemberName]string propertyName = null)
        {
            object oldValue = null;
            _properties.TryGetValue(propertyName, out oldValue);

            if (object.Equals(oldValue, newValue)) return false;

            _properties[propertyName] = newValue;
            this.NotifyPropertyChanged(propertyName);

            return true;
        }
    }
}