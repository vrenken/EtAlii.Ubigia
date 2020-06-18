namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public interface IConnection
    {
        /// <summary>
        /// The storage to which the connection talks. 
        /// </summary>
        Storage Storage { get; }

        // TODO: is a must.
        //Account Account [ get ]
        
        /// <summary>
        /// Returns true when a connection with the server has been made.  
        /// </summary>
        bool IsConnected { get; }

        Task Close();
        Task Open(string accountName, string password);
    }
}