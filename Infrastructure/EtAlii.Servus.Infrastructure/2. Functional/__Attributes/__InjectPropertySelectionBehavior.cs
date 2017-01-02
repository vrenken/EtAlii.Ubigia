//namespace EtAlii.Servus.Infrastructure
//{
//    using System;
//    using System.Linq;
//    using System.Reflection;
//    using SimpleInjector.Advanced;

//    public class InjectPropertySelectionBehavior : IPropertySelectionBehavior
//    {
//        public bool SelectProperty(Type serviceType, PropertyInfo propertyInfo)
//        {
//            return propertyInfo.GetCustomAttributes(typeof(InjectAttribute), true).Any();
//        }
//    }
//}