namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;

    public class StringConstantSubjectProcessor : IConstantSubjectProcessor
    {
        public object Process(ProcessParameters<ConstantSubject, SequencePart> parameters)
        {
            return ((StringConstantSubject)parameters.Target).Value;
        }
    }
}
