namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;

    public class AssignmentContext : IAssignmentContext
    {
        public IFabricContext Fabric { get { return _fabric; } }
        private readonly IFabricContext _fabric;

        public AssignmentContext(IFabricContext fabric)
        {
            _fabric = fabric;
        }
    }
}
