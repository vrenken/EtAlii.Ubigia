namespace EtAlii.Servus.Api
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class Structure : INotifyPropertyChanged
    {
        public Identifier Id { get; private set; }
        public bool IsModified { get; private set; }


        /// <summary>
        /// Multicast event for property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public LinkCollection Links { get { return _links; } }
        private readonly LinkCollection _links = new LinkCollection();

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners.</param>
        protected void NotifyPropertyChanged(string propertyName)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}