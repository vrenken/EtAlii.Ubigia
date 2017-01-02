namespace EtAlii.Servus.Api.Logical
{
    using EtAlii.Servus.Api.Transport;
    using EtAlii.xTechnology.MicroContainer;

    internal class SerializationScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register(() => new SerializerFactory().Create());
            //_container.Register<IPropertiesSerializer, JsonPropertiesSerializer>();
            container.Register<IPropertiesSerializer, BsonPropertiesSerializer>();
        }
    }
}
