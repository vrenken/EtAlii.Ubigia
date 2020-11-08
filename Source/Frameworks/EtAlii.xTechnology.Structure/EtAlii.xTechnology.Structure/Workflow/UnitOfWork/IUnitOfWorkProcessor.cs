namespace EtAlii.xTechnology.Structure.Workflow
{
    public interface IUnitOfWorkProcessor
    {
        void Process(IUnitOfWork unitOfWork, IUnitOfWorkHandler handler);
    }
}