namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using EtAlii.xTechnology.MicroContainer;

    public interface ILogicalContextExtension
    {
        void Initialize(Container container);
    }
}