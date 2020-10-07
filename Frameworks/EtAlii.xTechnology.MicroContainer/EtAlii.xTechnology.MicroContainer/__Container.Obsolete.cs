// namespace EtAlii.xTechnology.MicroContainer
// {
//     using System;
//     using System.Linq;
//     using System.Reflection;
//
//     public partial class Container
// 	{
// 		[Obsolete("Bad practice, use RegisterTransient instead.")]
// 		public TInterface InstantiateUnregistered<TInterface, TInstance>()
// 			where TInterface : class
// 			where TInstance : class
// 		{
// 			var typeToInstantiate = typeof(TInstance);
// 			return InstantiateUnregistered<TInterface>(typeToInstantiate);
// 		}
//
// 		[Obsolete("Bad practice, use RegisterTransient instead.")]
// 		public TInterface InstantiateUnregistered<TInterface>(Type typeToInstantiate)
// 			where TInterface : class
// 		{
// 			var constructors = typeToInstantiate
// 				.GetTypeInfo()
// 				.DeclaredConstructors
// 				.ToArray();
// 			if (constructors.Length != 1)
// 			{
// 				throw new InvalidOperationException("Only items with one single constructor can be used during DI discovery and injection");
// 			}
// 			var constructor = constructors.Single();
// 			var parameters = constructor.GetParameters();
//
// 			var arguments = parameters
// 				.Select(p => GetInstance(p.ParameterType))
// 				.ToArray();
//
// 			return (TInterface)Activator.CreateInstance(typeToInstantiate, arguments);
// 		}
// 	}
// }
