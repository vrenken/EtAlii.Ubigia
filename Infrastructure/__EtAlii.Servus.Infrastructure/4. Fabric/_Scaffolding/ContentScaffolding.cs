namespace EtAlii.Servus.Infrastructure.Fabric
{
    using HashLib;
    using EtAlii.xTechnology.MicroContainer;

    internal class ContentScaffolding : EtAlii.xTechnology.MicroContainer.IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IContentSet, ContentSet>();

            container.Register<IContentGetter, ContentGetter>();
            container.Register<IContentPartGetter, ContentPartGetter>();
            container.Register<IContentStorer, ContentStorer>();
            container.Register<IContentPartStorer, ContentPartStorer>();
            container.Register<IHash>(HashFactory.Checksum.CreateCRC64_ECMA);
        }
    }
}