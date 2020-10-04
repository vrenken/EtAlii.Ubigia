namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Logical;

    internal class PropertiesRepository : IPropertiesRepository
    {
        private readonly ILogicalContext _logicalContext;

        public PropertiesRepository(ILogicalContext logicalContext)
        {
            _logicalContext = logicalContext;
        }

        public void Store(Identifier identifier, PropertyDictionary properties)
        {
            if (identifier == Identifier.Empty)
            {
                throw new PropertiesRepositoryException("No identifier specified");
            }

            if (properties == null)
            {
                throw new PropertiesRepositoryException("No properties specified");
            }

            try
            {
                _logicalContext.Properties.Store(identifier, properties);
            }
            catch (Exception e)
            {
                throw new PropertiesRepositoryException("Unable to store properties for the specified identifier", e);
            }
        }

        public PropertyDictionary Get(Identifier identifier)
        {
            if (identifier == Identifier.Empty)
            {
                throw new PropertiesRepositoryException("No identifier specified");
            }

            try
            {
                return _logicalContext.Properties.Get(identifier);
            }
            catch (Exception e)
            {
                throw new PropertiesRepositoryException("Unable to get properties for the specified identifier", e);
            }
        }
    }
}