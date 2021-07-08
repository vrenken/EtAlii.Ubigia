// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    public class GraphAssigner : IGraphAssigner
    {
        private readonly IPropertiesToIdentifierAssigner _propertiesToIdentifierAssigner;
        private readonly IDynamicObjectToIdentifierAssigner _dynamicObjectToIdentifierAssigner;
        private readonly INodeToIdentifierAssigner _nodeToIdentifierAssigner;
        private readonly IConstantToIdentifierTagAssigner _constantToIdentifierTagAssigner;

        public GraphAssigner(
            IPropertiesToIdentifierAssigner propertiesToIdentifierAssigner,
            IDynamicObjectToIdentifierAssigner dynamicObjectToIdentifierAssigner,
            INodeToIdentifierAssigner nodeToIdentifierAssigner,
            IConstantToIdentifierTagAssigner constantToIdentifierTagAssigner)
        {
            _propertiesToIdentifierAssigner = propertiesToIdentifierAssigner;
            _dynamicObjectToIdentifierAssigner = dynamicObjectToIdentifierAssigner;
            _nodeToIdentifierAssigner = nodeToIdentifierAssigner;
            _constantToIdentifierTagAssigner = constantToIdentifierTagAssigner;
        }


        public async Task<IReadOnlyEntry> AssignProperties(Identifier location, IPropertyDictionary properties, ExecutionScope scope)
        {
            return await _propertiesToIdentifierAssigner.Assign(properties, location, scope).ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> AssignTag(Identifier location, string tag, ExecutionScope scope)
        {
            return await _constantToIdentifierTagAssigner.Assign(tag, location, scope).ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> AssignNode(Identifier location, Node node, ExecutionScope scope)
        {
            return await _nodeToIdentifierAssigner.Assign(node, location, scope).ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> AssignDynamic(Identifier location, object o, ExecutionScope scope)
        {
            return await _dynamicObjectToIdentifierAssigner.Assign(o, location, scope).ConfigureAwait(false);
        }

    }
}
