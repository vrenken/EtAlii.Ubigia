namespace EtAlii.Servus.Windows
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;

    [Serializable]
    internal class ConnectionSettings
    {
        public string Address { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Space { get; set; }
    }
}
