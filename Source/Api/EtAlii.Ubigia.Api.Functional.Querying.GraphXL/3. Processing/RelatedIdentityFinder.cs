namespace EtAlii.Ubigia.Api.Functional
{
    internal class RelatedIdentityFinder : IRelatedIdentityFinder
    {
        public Identifier Find(Structure structure)
        {
            var node = structure.Node;
            if (node != null)
            {
                return node.Id;
            }
            var parent = structure.Parent;
            return parent != null 
                ? Find(parent) 
                : Identifier.Empty;
        }

    }
}