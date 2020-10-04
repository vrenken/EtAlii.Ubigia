namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    public interface IPropertiesGetter
    {
        Task<PropertyDictionary> Get(Identifier identifier, ExecutionScope scope);
    }
}