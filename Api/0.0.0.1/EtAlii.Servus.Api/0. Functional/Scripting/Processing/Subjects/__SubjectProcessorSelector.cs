//namespace EtAlii.Servus.Api.Functional
//{
//    using EtAlii.xTechnology.Structure;

//    internal class SubjectProcessorSelector : Selector<Subject, ISubjectProcessor>, ISubjectProcessorSelector
//    {
//        public SubjectProcessorSelector(
//            IPathSubjectProcessor pathSubjectProcessor,
//            IConstantSubjectsProcessor constantSubjectsProcessor,
//            IVariableSubjectProcessor variableSubjectProcessor,
//            IFunctionSubjectProcessor functionSubjectProcessor)
//        {
//            Register(subject => subject is PathSubject, pathSubjectProcessor)
//            .Register(subject => subject is ConstantSubject, constantSubjectsProcessor)
//            .Register(subject => subject is VariableSubject, variableSubjectProcessor)
//            .Register(subject => subject is FunctionSubject, functionSubjectProcessor);
//        }
//    }
//}
