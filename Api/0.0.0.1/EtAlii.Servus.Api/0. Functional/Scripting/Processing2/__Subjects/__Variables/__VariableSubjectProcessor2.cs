//namespace EtAlii.Servus.Api.Functional
//{
//    using System;
//    using System.Collections.Generic;

//    internal class VariableSubjectProcessor2 : IVariableSubjectProcessor2
//    {
//        private readonly IProcessingContext _context;

//        public VariableSubjectProcessor2(IProcessingContext context)
//        {
//            _context = context;
//        }

//        public void Process(Subject subject, ExecutionScope scope, IObserver<object> output)
//        {
//            var variableName = ((VariableSubject)subject).Name;
//            ScopeVariable variable;
//            if (_context.Scope.Variables.TryGetValue(variableName, out variable))
//            {
//                var enumerable = variable.Value as IEnumerable<object>;
//                if (enumerable != null)
//                {
//                    foreach (var o in enumerable)
//                    {
//                        output.OnNext(o);
//                    }
//                }
//                else
//                {
//                    output.OnNext(variable.Value);
//                }
//            }
//            else
//            {
//                //string message = String.Format("Variable {0} not set (subject: {0})", variableName, parameters.Target.ToString());
//                //throw new ScriptParserException(message);
//            }
//            output.OnCompleted();
//        }
//    }
//}
