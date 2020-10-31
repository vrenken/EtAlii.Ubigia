namespace EtAlii.Ubigia.Api
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The Ubigia version of the EF DbContext.
    /// </summary>
    public class UbigiaContext : DbContext
    {
        
    }

    public interface ITransport
    {
        
    }
    
    public class GrpcTransport : ITransport { }
    public class SignalRTransport : ITransport { }
    public class WebApiTransport : ITransport { }
}