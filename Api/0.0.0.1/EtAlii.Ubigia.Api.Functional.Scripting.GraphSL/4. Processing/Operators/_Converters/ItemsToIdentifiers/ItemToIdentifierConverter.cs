namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Structure;

    internal class ItemToIdentifierConverter : IItemToIdentifierConverter
    {
        public Task<Identifier> Convert(object item, ExecutionScope scope)
        {
            switch (item)
            {
                case Identifier identifier:
                    return Task.FromResult(identifier);
                case IReadOnlyEntry entry:
                    return Task.FromResult(entry.Id);
                case INode node:
                    return Task.FromResult(node.Id);
                default:
                    throw new ScriptProcessingException($"The {this.GetType().Name} is unable to convert the specified object: {item ?? "NULL"}");
            }                    

        }
    }
}
