//namespace EtAlii.Servus.Api.Functional
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Reactive;
//    using System.Reactive.Linq;
//    using System.Threading.Tasks;

//    internal class SequentialExecutionPlan : IUnaryExecutionPlan
//    {
//        private readonly List<IExecutionPlan> _subExecutionPlans;

//        public SequentialExecutionPlan()
//        {
//            _subExecutionPlans = new List<IExecutionPlan>();
//        }
//        public void Add(IExecutionPlan subExecutionPlan)
//        {
//            _subExecutionPlans.Add(subExecutionPlan);
//        }

//        public Task Execute(IObserver<object> output, IObservable<object> rightInput)
//        {
//            return Task.Run(() =>
//            {
//                var count = _subExecutionPlans.Count;
//                if (count > 1)
//                {
//                    for (int i = 0; i < count; i++)
//                    {

//                    }
//                }
//                else if(count == 1)
//                {
//                    return _subExecutionPlans
//                }
//                else
//                {
//                    throw new InvalidOperationException("Sequential execution plan contains no sub plans.");
//                }
//                foreach (var subExecutionPlan in _subExecutionPlans)
//                {
//                    var currentOutput = Observable.Create<object>(currentInput =>
//                    {
//                        subExecutionPlan.Execute(currentInput)
                        
//                    });
//                }
//            });
//        }

//        public override string ToString()
//        {
//            return String.Join(" ",_subExecutionPlans.Select(plan => plan.ToString()));
//        }
//    }
//}