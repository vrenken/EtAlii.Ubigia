namespace EtAlii.xTechnology.Structure.Workflow
{
    public abstract class UnitOfWorkHandlerBase<TUnitOfWork> : IUnitOfWorkHandler<TUnitOfWork>
        where TUnitOfWork : IUnitOfWork
    {
        protected internal abstract void Handle(TUnitOfWork unitOfWork);

        public void Handle(IUnitOfWork unitOfWork)
        {
            Handle((TUnitOfWork)unitOfWork);
        }
    }
}
