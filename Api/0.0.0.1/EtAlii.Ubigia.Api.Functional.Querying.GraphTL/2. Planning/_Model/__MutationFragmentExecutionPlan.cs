//namespace EtAlii.Ubigia.Api.Functional 
//{
//    using System;
//    using System.Threading.Tasks;
//
//    internal class MutationFragmentExecutionPlan<TFragment> : FragmentExecutionPlanBase<TFragment>
//        where TFragment: Fragment
//    {
//        private readonly IFragmentProcessor<TFragment> _processor;
//
//        public MutationFragmentExecutionPlan(
//            TFragment fragment,
//            FragmentMetadata fragmentMetadata,
//            IFragmentProcessor<TFragment> processor)
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