namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Threading.Tasks;

    public interface IIdentifierHeadGetter
    {
        Task<Identifier> GetCurrent(Guid spaceId);
        Task<(Identifier NextHeadIdentifier, Identifier PreviousHeadIdentifier)> GetNext(Guid spaceId);
    }
}