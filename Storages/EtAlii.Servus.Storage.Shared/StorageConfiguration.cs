namespace EtAlii.Servus.Storage
{
    using System;
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;

    public class StorageConfiguration : IStorageConfiguration
    {
        public IStorageExtension[] Extensions { get { return _extensions; } }
        private IStorageExtension[] _extensions;

        public string Name { get { return _name; } }
        private string _name;


        private Func<Container, IStorage> _getStorage;
        public IStorage GetStorage(Container container)
        {
            return _getStorage(container);
        }

        public StorageConfiguration()
        {
            _extensions = new IStorageExtension[0];
        }

        public IStorageConfiguration Use(IStorageExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException("extensions");
            }

            var alreadyRegistered = _extensions.FirstOrDefault(e => extensions.Any(e2 => e2.GetType() == e.GetType()));
            if(alreadyRegistered != null)
            { 
                throw new InvalidOperationException("Extension already registered: " + alreadyRegistered.GetType().Name);
            }

            _extensions = extensions
                .Concat(_extensions)
                .Distinct()
                .ToArray();
            return this;
        }

        public IStorageConfiguration Use(string name)
        {
            _name = name;
            _getStorage = container =>
            {
                container.Register<IStorage, DefaultStorage>();
                return container.GetInstance<IStorage>();
            };
            return this;
        }


        public IStorageConfiguration Use<TStorage>()
            where TStorage : class, IStorage
        {
            //if (_getStorage != null)
            //{
            //    throw new InvalidOperationException("GetStorage already set.");
            //}

            _getStorage = container =>
            {
                container.Register<IStorage, TStorage>();
                return container.GetInstance<IStorage>();
            };

            return this;
        }

    }
}
