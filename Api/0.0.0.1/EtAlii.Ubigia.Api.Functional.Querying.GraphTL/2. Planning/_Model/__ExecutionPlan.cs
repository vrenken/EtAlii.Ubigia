//namespace EtAlii.Ubigia.Api.Functional 
//{
//    using System;
//    using System.Reactive.Linq;
//    using System.Threading.Tasks;

//    public static class QueryExecutionPlan
//    {
//        internal static readonly EmptyExecutionPlan Empty = new EmptyExecutionPlan();

//        private class EmptyExecutionPlan : Frag
//        {
//            public Type OutputType { get; }

//            public EmptyExecutionPlan()
//            {
//                OutputType = GetType();
//            }

//            public Task<IObservable<Structure>> Execute(QueryExecutionScope scope)
//            {
//                return Task.FromResult(Observable.Empty<Structure>());
//            }

//            public override string ToString()
//            {
//                return "[Empty]";
//            }
//        }
//    }
//}