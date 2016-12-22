namespace EtAlii.Servus.Api.Functional
{
    using System.Threading.Tasks;

    internal interface IItemToIdentifierConverter
    {
        Task<Identifier> Convert(object items, ExecutionScope scope);
    }
}