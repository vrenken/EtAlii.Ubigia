namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    public interface IContentDefinitionQueryHandler
    {
        Task<ContentDefinition> Execute(ContentDefinitionQuery query);
    }
}
