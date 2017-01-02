//namespace EtAlii.Servus.Api.Functional
//{
//    using EtAlii.xTechnology.Structure;

//    internal class SubjectProcessorSelector2 : Selector<Subject, ISubjectProcessor2>, ISubjectProcessorSelector2
//    {
//        public SubjectProcessorSelector2(
//            IPathSubjectProcessor2 pathSubjectProcessor,
//            IConstantSubjectsProcessor2 constantSubjectsProcessor,
//            IVariableSubjectProcessor2 variableSubjectProcessor,
//            IFunctionSubjectProcessor2 functionSubjectProcessor)
//        {
//            Register(subject => subject is PathSubject, pathSubjectProcessor)
//            .Register(subject => subject is ConstantSubject, constantSubjectsProcessor)
//            .Register(subject => subject is VariableSubject, variableSubjectProcessor)
//            .Register(subject => subject is FunctionSubject, functionSubjectProcessor);
//        }
//    }
//}
