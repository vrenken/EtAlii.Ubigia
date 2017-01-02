namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;

    internal class VariableSubjectProcessor : ISubjectProcessor
    {
        private readonly ProcessingContext _context;

        public VariableSubjectProcessor(ProcessingContext context)
        {
            _context = context;
        }

        public object Process(ProcessParameters<Subject, SequencePart> parameters)
        {
            object result = null;
            var variableName = ((VariableSubject)parameters.Target).Name;
            ScopeVariable variable;
            if (_context.Scope.Variables.TryGetValue(variableName, out variable))
            {
                result = variable.Value;
            }
            else
            {
                //string message = String.Format("Variable {0} not set (subject: {0})", variableName, parameters.Target.ToString());
                //throw new ScriptParserException(message);
            }
            return result;
        }
    }
}
