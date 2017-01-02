namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api.Data.Model;
    using System;
    using EtAlii.xTechnology.Structure;

    internal class AssignOperatorProcessor : IOperatorProcessor
    {
        private readonly ISelector<ProcessParameters<Operator, SequencePart>, IAssigner> _assignerSelector;

        public AssignOperatorProcessor(ISelector<ProcessParameters<Operator, SequencePart>, IAssigner> assignerSelector)
        {
            _assignerSelector = assignerSelector;
        }

        public object Process(ProcessParameters<Operator, SequencePart> parameters)
        {
            var assigner = _assignerSelector.TrySelect(parameters);
            if (assigner == null)
            {
                var message = String.Format("Left part not supported by the AssignOperatorProcessor (part: {0})", parameters.LeftPart.ToString());
                throw new ScriptProcessingException(message);
            }
            var result = assigner.Assign(parameters);

            return result;
        }
    }
}
