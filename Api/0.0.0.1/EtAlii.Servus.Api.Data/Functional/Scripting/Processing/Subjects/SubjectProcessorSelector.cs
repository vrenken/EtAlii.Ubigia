namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.xTechnology.Structure;

    internal class SubjectProcessorSelector : Selector<Subject, ISubjectProcessor>
    {
        public SubjectProcessorSelector(
            PathSubjectProcessor pathSubjectProcessor,
            ConstantSubjectProcessor constantSubjectProcessor,
            VariableSubjectProcessor variableSubjectProcessor)
        {
            Register(subject => subject is PathSubject, pathSubjectProcessor)
            .Register(subject => subject is ConstantSubject, constantSubjectProcessor)
            .Register(subject => subject is VariableSubject, variableSubjectProcessor);
        }
    }
}
