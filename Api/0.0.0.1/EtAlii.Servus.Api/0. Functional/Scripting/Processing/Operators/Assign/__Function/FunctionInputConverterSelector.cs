//namespace EtAlii.Servus.Api.Functional
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Threading.Tasks;
//    using EtAlii.xTechnology.Collections;
//    using EtAlii.xTechnology.Structure;

//    internal class FunctionInputConverterSelector : Selector<object, Func<object, Task<object>>>, IFunctionInputConverterSelector
//    {
//        public FunctionInputConverterSelector(IProcessingContext context)
//        {
//            this.Register(o => o is INode, o => Task.FromResult(o))
//            .Register(o => o is IReadOnlyEntry, o => Task.FromResult(o))
//            .Register(o => o is Identifier, o => Task.FromResult(o))
//            .Register(o => o is IEnumerable<INode>, o => ReturnMultiple<INode>(o))
//            .Register(o => o is IEnumerable<IReadOnlyEntry>, o => ReturnMultiple<IReadOnlyEntry>(o))
//            .Register(o => o is IEnumerable<Identifier>, o => ReturnMultiple<Identifier>(o))
//            .Register(o => o != null, o => Task.FromResult(o));
//        }

//        private Task<object> ReturnMultiple<T>(object items)
//        {
//            object result = null;
//            var enumerableItems = (IEnumerable<T>)items;
//            if (enumerableItems.Multiple())
//            {
//                result = enumerableItems;
//            }
//            else
//            {
//                result = enumerableItems.FirstOrDefault();
//            }
//            return Task.FromResult(result);
//        }
//    }
//}
