namespace EtAlii.Servus.Api.Logical
{
    using EtAlii.Servus.Api.Fabric;

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
