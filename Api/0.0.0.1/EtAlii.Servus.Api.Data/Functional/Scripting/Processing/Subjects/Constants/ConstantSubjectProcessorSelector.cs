namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.xTechnology.Structure;

    internal class ConstantSubjectProcessorSelector : Selector<ConstantSubject, IConstantSubjectProcessor>
    {
        public ConstantSubjectProcessorSelector(StringConstantSubjectProcessor stringConstantSubjectProcessor)
        {
            Register(subject => subject is StringConstantSubject, stringConstantSubjectProcessor);
        }
    }
}
