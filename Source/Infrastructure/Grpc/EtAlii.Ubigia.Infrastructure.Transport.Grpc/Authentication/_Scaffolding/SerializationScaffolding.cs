namespace EtAlii.Ubigia.Infrastructure.Transport.Grpc
{
    using EtAlii.Ubigia.Serialization;
    using EtAlii.xTechnology.MicroContainer;

	public class SerializationScaffolding : IScaffolding
	{
		public void Register(Container container)
		{
			// We need to use our in-house serialization. This to ensure that dictionaries, ulongs and floats are serialized correctly.
			container.Register(() => new SerializerFactory().Create());
		}
	}
}