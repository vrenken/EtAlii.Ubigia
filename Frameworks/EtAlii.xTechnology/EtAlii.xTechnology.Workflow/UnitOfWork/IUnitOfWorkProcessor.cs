namespace EtAlii.xTechnology.Workflow
{
    public interface IUnitOfWorkProcessor
    {
        void Process(IUnitOfWork unitOfWork, IUnitOfWorkHandler handler);
    }
}