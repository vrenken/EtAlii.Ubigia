using SimpleInjector;
namespace EtAlii.xTechnology.Workflow
{
    public class UnitOfWorkProcessor : IUnitOfWorkProcessor
    {
        private Container _container;

        public UnitOfWorkProcessor(Container container)
        {
            _container = container;
        }

        public void Process(IUnitOfWork unitOfWork, IUnitOfWorkHandler handler)
        {
            //var handler = unitOfWork.GetHandler(_container);
            handler.Handle(unitOfWork);
        }
    }
}
