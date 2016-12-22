namespace EtAlii.xTechnology.Workflow
{
    public interface IUnitOfWorkHandler
    {
        void Handle(IUnitOfWork unitOfWork);
    }
}
