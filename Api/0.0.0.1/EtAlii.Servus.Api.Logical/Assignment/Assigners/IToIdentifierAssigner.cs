namespace EtAlii.Servus.Api.Logical
{
    using System.Threading.Tasks;

    internal interface IToIdentifierAssigner 
    {
        Task<INode> Assign(object o, Identifier id, ExecutionScope scope);
    }
}