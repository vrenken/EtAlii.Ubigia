//namespace EtAlii.Servus.Api.Functional
//{
//    using System;

//    internal class UnaryExecutionPlanCombiner : IUnaryExecutionPlanCombiner
//    {
//        public Func<IObservable<object>> Combine(
//            IExecutionPlanner planner,
//            SequencePart currentPart,
//            SequencePart nextPart,
//            Func<IObservable<object>> getLeftInput,
//            Func<IObservable<object>> getRightInput,
//            out bool skipNext)
//        {
//            skipNext = false;
//            return ((IUnaryExecutionPlanner) planner).Plan(currentPart, getRightInput);
//        }
//    }
//}