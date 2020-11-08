namespace EtAlii.xTechnology.Structure.Workflow
{
    public interface IUnitOfWorkHandler
    {
        void Handle(IUnitOfWork unitOfWork);
    }
}
