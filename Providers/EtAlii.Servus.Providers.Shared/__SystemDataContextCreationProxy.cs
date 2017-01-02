//namespace EtAlii.Servus.Provisioning
//{
//    using System;
//    using EtAlii.Servus.Api.Functional;

//    public class SystemDataContextCreationProxy : ISystemDataContextCreationProxy
//    {
//        private Func<IDataContext> _create;

//        public IDataContext Request()
//        {
//            if (_create == null)
//            {
//                throw new NotSupportedException("This SystemDataContextCreationProxy  instance has no Create function assigned.");
//            }
//            return _create();
//        }

//        public void Initialize(Func<IDataContext> create)
//        {
//            if (create == null)
//            {
//                throw new ArgumentException(nameof(create));
//            }

//            _create = create;
//        }
//    }
//}