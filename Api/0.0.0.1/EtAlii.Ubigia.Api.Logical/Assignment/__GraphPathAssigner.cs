//namespace EtAlii.Ubigia.Api.Logical
//{
//    using System.Threading.Tasks;

//    internal class GraphPathAssigner : IGraphPathAssigner
//    {
//        private readonly IAssignmentContext _context;
//        private readonly IToIdentifierAssignerSelector _toIdentifierAssignerSelector;

//        public GraphPathAssigner(
//            IAssignmentContext context, 
//            IToIdentifierAssignerSelector toIdentifierAssignerSelector)
//        {
//            _context = context;
//            _toIdentifierAssignerSelector = toIdentifierAssignerSelector;
//        }

//        // TODO: The Assignment result should be a IReadOnlyEntry and not an INode.
//        public async Task<INode> Assign(GraphPath path, Identifier location, object item, ExecutionScope scope) 
//        {
//            var toIdentifierAssigner = _toIdentifierAssignerSelector.TrySelect(path, item);

//            if (toIdentifierAssigner == null)
//            {
//                throw new AssignmentException("Object not supported for assignment operations: " + (item != null ? item.ToString() : "NULL"));
//            }

//            return await toIdentifierAssigner.Assign(item, location, scope);
//        }
//    }
//}
