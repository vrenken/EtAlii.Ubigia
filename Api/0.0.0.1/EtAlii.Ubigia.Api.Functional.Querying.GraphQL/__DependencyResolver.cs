//namespace EtAlii.Ubigia.Api.Functional
//{
//    using System
//    using EtAlii.Ubigia.Api.Functional.Querying.GraphQL
//    using EtAlii.xTechnology.MicroContainer
//    using GraphQL
//
//    internal class DependencyResolver : IDependencyResolver
//    {
//        private readonly Container _container
//
//        private readonly Type _baseClassType = typeof(DynamicObjectGraphType)
//        
//        public DependencyResolver(Container container)
//        {
//            _container = container
//        }
//        public T Resolve<T>()
//        {
//            return (T)Resolve(typeof(T))
//        }
//
//        public object Resolve(Type type)
//        {
////            if (type.IsSubclassOf(_baseClassType))
////            {
////                return Activator.CreateInstance(type)
////            }
//            if (type == typeof(PersonType))
//            {
//                var data = _container.GetInstance<IUbigiaData>()
//                return new PersonType(data)
//            }
//            if (type == typeof(HumanType))
//            {
//                var data = _container.GetInstance<IUbigiaData>()
//                return new HumanType(data)
//            }
//            if (type == typeof(HumanInputType))
//            {
//                return new HumanInputType()
//            }
//            if (type == typeof(DroidType))
//            {
//                var data = _container.GetInstance<IUbigiaData>()
//                return new DroidType(data)
//            }
//            if (type == typeof(CharacterInterface))
//            {
//                return new CharacterInterface()
//            }
//            if (type == typeof(EpisodeEnum))
//            {
//                return new EpisodeEnum()
//            }
//            return _container.GetInstance(type)
//
//        }
//    }
//}