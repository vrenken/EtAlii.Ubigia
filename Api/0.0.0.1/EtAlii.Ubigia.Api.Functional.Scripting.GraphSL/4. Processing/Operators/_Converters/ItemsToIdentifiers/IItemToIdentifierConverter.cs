namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;

    internal interface IItemToIdentifierConverter
    {
        Task<Identifier> Convert(object item, ExecutionScope scope);
    }
}