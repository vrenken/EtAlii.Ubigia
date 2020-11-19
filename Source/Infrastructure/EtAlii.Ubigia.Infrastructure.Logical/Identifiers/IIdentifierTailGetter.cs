namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Threading.Tasks;

    public interface IIdentifierTailGetter
    {
        Task<Identifier> Get(Guid spaceId);
    }
}