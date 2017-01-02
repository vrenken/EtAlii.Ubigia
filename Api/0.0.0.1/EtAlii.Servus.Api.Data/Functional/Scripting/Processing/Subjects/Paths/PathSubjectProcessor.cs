namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Servus.Api.Data.Model;

    internal class PathSubjectProcessor : ISubjectProcessor
    {
        private readonly StaticPathSubjectProcessor _staticPathSubjectProcessor;
        private readonly QueryPathSubjectProcessor _queryPathSubjectProcessor;
        private readonly ProcessingContext _context;

        public PathSubjectProcessor(
            QueryPathSubjectProcessor queryPathSubjectProcessor,
            StaticPathSubjectProcessor staticPathSubjectProcessor, 
            ProcessingContext context)
        {
            _queryPathSubjectProcessor = queryPathSubjectProcessor;
            _staticPathSubjectProcessor = staticPathSubjectProcessor;
            _context = context;
        }

        public object Process(ProcessParameters<Subject, SequencePart> parameters)
        {
            object result = null;

            var isAbsolute = IsAbsolute((PathSubject)parameters.Target);
            if (parameters.FuturePart is VariableSubject)
            {
                var variableName = ((VariableSubject) parameters.FuturePart).Name;
                ScopeVariable variable;
                if (_context.Scope.Variables.TryGetValue(variableName, out variable))
                {
                    if (variable.Value is PathSubject)
                    {
                        // Ok, let's forward the whole pathsubject.
                        result = parameters.Target;
                    }
                    else if (variable.Value is INode)
                    {
                        // Ok, let's forward the whole pathsubject.
                        result = parameters.Target;
                    }
                    else 
                    {
                        if (isAbsolute)
                        {
                            // Query the database to retrieve the result.
                            result = _queryPathSubjectProcessor.Process(parameters);
                        }
                        else
                        {
                            result = parameters.Target;
                        }
                    }
                }
                else
                {
                    if (isAbsolute)
                    {
                        // Query the database to retrieve the result.
                        result = _queryPathSubjectProcessor.Process(parameters);
                    }
                    else
                    {
                        result = parameters.Target;
                    }
                }
            }
            else if (parameters.FuturePart is PathSubject)
            {
                result = parameters.Target;
            }
            else
            {
                // Query the database to retrieve the result.
                result = _queryPathSubjectProcessor.Process(parameters);
            }
            return result;
        }

        private bool IsAbsolute(PathSubject path)
        {
            return path.Parts.FirstOrDefault() is IsParentOfPathSubjectPart;
        }
    }
}
