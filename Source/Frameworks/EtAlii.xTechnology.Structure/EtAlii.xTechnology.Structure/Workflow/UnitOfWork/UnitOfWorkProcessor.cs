namespace EtAlii.xTechnology.Structure.Workflow
{
    public class UnitOfWorkProcessor : IUnitOfWorkProcessor
    {
        public void Process(IUnitOfWork unitOfWork, IUnitOfWorkHandler handler)
        {
            //var handler = unitOfWork.GetHandler(_container);
            handler.Handle(unitOfWork);
        }
    }
}
