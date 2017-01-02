//namespace EtAlii.Servus.Api.Functional
//{
//    using System;

//    internal class AssignOperatorProcessor : IAssignOperatorProcessor
//    {
//        private readonly IAssignOperatorSelector _assignOperatorSelector;

//        public AssignOperatorProcessor(IAssignOperatorSelector assignOperatorSelector)
//        {
//            _assignOperatorSelector = assignOperatorSelector;
//        }

//        public void Process(OperatorParameters parameters)
//        {
//            var assigner = _assignOperatorSelector.TrySelect(parameters);
//            if (assigner == null)
//            {
//                var message = String.Format("Left part not supported by the AssignOperatorProcessor (part: {0})", parameters.LeftSubject.ToString());
//                throw new ScriptProcessingException(message);
//            }
//            assigner.Assign(parameters);
//        }
//    }
//}
