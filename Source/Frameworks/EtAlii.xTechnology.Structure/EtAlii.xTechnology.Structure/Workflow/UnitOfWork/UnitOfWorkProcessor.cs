namespace EtAlii.xTechnology.Structure.Workflow
{
    public class UnitOfWorkProcessor : IUnitOfWorkProcessor
    {
        public void Process(IUnitOfWork unitOfWork, IUnitOfWorkHandler handler)
        {
            handler.Handle(unitOfWork);
        }
    }
}
