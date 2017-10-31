//namespace EtAlii.Ubigia.Infrastructure.Hosting
//{
//    using System;
//    using System.Linq;
//    using EtAlii.Ubigia.Infrastructure.Functional;
//    using EtAlii.Ubigia.Storage;
//    using EtAlii.xTechnology.MicroContainer;
//    using EtAlii.xTechnology.Hosting;

//    public class HostConfiguration : EtAlii.xTechnology.Hosting.HostConfiguration, IHostConfiguration
//    {
//        public IStorage Storage { get; private set; }

//        public IInfrastructure Infrastructure { get; private set; }

//        //private Func<Container, IHost> _getHost; 
//        //public IHost GetHost(Container container)
//        //{
//        //    return _getHost(container);
//        //}


//        public HostConfiguration()
//        {
//            //_getHost = container =>
//            //{
//            //    container.Register<IHost, DefaultHost>();
//            //    return container.GetInstance<IHost>();
//            //};
//        }

//        public IHostConfiguration Use(IStorage storage)
//        {
//            if (storage == null)
//            {
//                throw new ArgumentException(nameof(storage));
//            }

//            Storage = storage;

//            return this;
//        }
//        public IHostConfiguration Use(IInfrastructure infrastructure)
//        {
//            if (infrastructure == null)
//            {
//                throw new ArgumentException(nameof(infrastructure));
//            }

//            Infrastructure = infrastructure;

//            return this;
//        }

//        //public IHostConfiguration Use<THost>()
//        //    where THost : class, IHost
//        //{
//        //    //if (_getHost != null)
//        //    //{
//        //    //    throw new InvalidOperationException("GetHost already set.");
//        //    //}

//        //    //_getHost = container =>
//        //    //{
//        //    //    container.Register<IHost, THost>();
//        //    //    return container.GetInstance<IHost>();
//        //    //};

//        //    return this;
//        //}
//    }
//}
