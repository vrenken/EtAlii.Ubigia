//namespace EtAlii.Ubigia.Api.Functional 
//{
//    using System;
//    using System.Threading.Tasks;
//
//    internal class QueryFragmentExecutionPlan<TFragment> : FragmentExecutionPlanBase<TFragment>
//        where TFragment : Fragment
//    {
//        private readonly IQueryFragmentProcessor _processor;
//
//        public QueryFragmentExecutionPlan(
//            TFragment fragment,
//            FragmentMetadata fragmentMetadata,
//            IQueryFragmentProcessor processor)
//            : base(fragment, fragmentMetadata)
//        {
//            _processor = processor;
//        }
//
//        protected override Type GetOutputType()
//        {
//            return typeof(TFragment);
//        }
//
//        protected override Task Execute(FragmentMetadata fragmentMetadata, QueryExecutionScope executionScope, IObserver<Structure> output)
//        {
//            return _processor.Process(Fragment, executionScope, fragmentMetadata, output);
//        }
//
//        public override string ToString()
//        {
//            return Fragment.ToString();
//        }
//    }
//}