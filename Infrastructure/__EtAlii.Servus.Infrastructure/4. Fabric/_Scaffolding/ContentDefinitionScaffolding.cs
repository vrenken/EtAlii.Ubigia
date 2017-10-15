namespace EtAlii.Servus.Infrastructure.Fabric
{

    internal class ContentDefinitionScaffolding : EtAlii.xTechnology.MicroContainer.IScaffolding
    {
        public void Register(EtAlii.xTechnology.MicroContainer.Container container)
        {
            container.Register<IContentDefinitionSet, ContentDefinitionSet>();

            //container.Register<IContentDefinitionRepository, ContentDefinitionRepository>();
            container.Register<IContentDefinitionGetter, ContentDefinitionGetter>();
            container.Register<IContentDefinitionPartGetter, ContentDefinitionPartGetter>();
            container.Register<IContentDefinitionStorer, ContentDefinitionStorer>();
            container.Register<IContentDefinitionPartStorer, ContentDefinitionPartStorer>();
        }
    }
}