namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using EtAlii.xTechnology.MicroContainer;

    internal class IdentifierScaffolding : EtAlii.xTechnology.MicroContainer.IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<ILogicalIdentifierSet, LogicalIdentifierSet>();

            container.Register<IIdentifierTailGetter, IdentifierTailGetter>();
            container.Register<IIdentifierHeadGetter, IdentifierHeadGetter>();
            container.Register<INextIdentifierGetter, NextIdentifierGetter>();
            container.Register<IIdentifierRootUpdater, IdentifierRootUpdater>();
        }
    }
}