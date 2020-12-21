namespace EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The Ubigia version of the EF DbContext.
    /// </summary>
    public class UbigiaDbContext : DbContext
    {
        public UbigiaDbContext(DbContextOptions options)
            : base(options)
        {
        }    
    }
}