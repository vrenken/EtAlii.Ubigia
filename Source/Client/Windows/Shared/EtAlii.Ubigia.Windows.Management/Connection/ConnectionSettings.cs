namespace EtAlii.Ubigia.Windows.Management
{
    using System;

    [Serializable]
    internal class ConnectionSettings
    {
        public string Address { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string TransportType { get; set; }
    }
}
