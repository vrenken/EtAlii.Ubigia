namespace EtAlii.Servus.Api.Logical
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Fabric;

    /// <summary>
    /// Facade that hides away complex logical Properties operations.
    /// </summary>
    internal class PropertiesManager : IPropertiesManager
    {
        private readonly IFabricContext _fabric;
        private readonly IPropertiesGetter _propertiesGetter;

        public PropertiesManager(
            IFabricContext fabric, 
            IPropertiesGetter propertiesGetter)
        {
            _fabric = fabric;
            _propertiesGetter = propertiesGetter;
        }

        public async Task Set(Identifier identifier, PropertyDictionary properties, ExecutionScope scope)
        {
            if (identifier == Identifier.Empty)
            {
                throw new PropertyManagerException("No identifier specified");
            }

            await _fabric.Properties.Store(identifier, properties, scope);
        }


        public async Task<PropertyDictionary> Get(Identifier identifier, ExecutionScope scope)
        {
            if (identifier == Identifier.Empty)
            {
                throw new PropertyManagerException("No identifier specified");
            }

            return await _propertiesGetter.Get(identifier, scope);
        }

        public Task<bool> HasProperties(Identifier identifier, ExecutionScope scope)
        {
            if (identifier == Identifier.Empty)
            {
                throw new PropertyManagerException("No identifier specified");
            }

            throw new NotImplementedException();
        }
    }
}
