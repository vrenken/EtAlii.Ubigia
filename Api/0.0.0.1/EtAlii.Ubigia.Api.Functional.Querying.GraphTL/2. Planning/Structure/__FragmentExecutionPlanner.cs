//namespace EtAlii.Ubigia.Api.Functional
//{
//    internal class FragmentExecutionPlanner : IFragmentExecutionPlanner
//    {
//        private readonly IFragmentExecutionPlannerSelector _fragmentExecutionPlannerSelector;
//        //private readonly IFragmentPartExecutionPlannerSelector _fragmentPartExecutionPlannerSelector;
//        //private readonly IExecutionPlanCombinerSelector _executionPlanCombinerSelector;
//
//        public FragmentExecutionPlanner(
//            IFragmentExecutionPlannerSelector fragmentExecutionPlannerSelector
//            //IFragmentPartExecutionPlannerSelector fragmentPartExecutionPlannerSelector, 
//            //IExecutionPlanCombinerSelector executionPlanCombinerSelector
//            )
//        {
//            _fragmentExecutionPlannerSelector = fragmentExecutionPlannerSelector;
//            //_fragmentPartExecutionPlannerSelector = fragmentPartExecutionPlannerSelector;
//            //_executionPlanCombinerSelector = executionPlanCombinerSelector;
//        }
//
//        public FragmentExecutionPlan Plan(Fragment fragment)
//        {
////            ISubjectExecutionPlan previousPartExecutionPlan = null;
////
////            // We are not interested in planning execution of comment parts, so let's exclude them.
////            var parts = fragment.Parts
////                .Where(p => !(p is Comment))
////                .ToArray();
////            var count = parts.Length;
////
////            for (int i = 1; i <= count; i++)
////            {
////                var currentPosition = count - i;
////
////                var currentPart = parts[currentPosition];
//            var executionPlanner = _fragmentExecutionPlannerSelector.Select(fragment);
//            //new FragmentExecutionPlan()
////                var currentPartExecutionPlanner = _fragmentPartExecutionPlannerSelector.Select(currentPart);
////                var currentPartExecutionPlanCombiner = _executionPlanCombinerSelector.Select(currentPartExecutionPlanner);
////
////                var nextPart = currentPosition >= 1 ? parts[currentPosition - 1] : null;
////                var currentPartExecutionPlan = currentPartExecutionPlanCombiner.Combine(currentPartExecutionPlanner, currentPart, nextPart, previousPartExecutionPlan, out var skipNext);
////                i = skipNext ? i + 1 : i;
////
////                previousPartExecutionPlan = currentPartExecutionPlan;
////            }
////
////            var startExecutionPlan = previousPartExecutionPlan;
////            var executionPlan = startExecutionPlan != null ? new FragmentExecutionPlan(startExecutionPlan) : FragmentExecutionPlan.Empty;
////            return executionPlan;
//            return executionPlanner(fragment);
//        }
//    }
//}