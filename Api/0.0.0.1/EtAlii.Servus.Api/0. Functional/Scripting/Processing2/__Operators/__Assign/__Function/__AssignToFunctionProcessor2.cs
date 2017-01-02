//namespace EtAlii.Servus.Api.Functional
//{
//    internal class AssignToFunctionProcessor2 : IAssignToFunctionProcessor2
//    {
//        private readonly IFunctionSubjectParameterConverterSelector2 _parameterConverterSelector;
//        private readonly IFunctionContext _functionContext;

//        public AssignToFunctionProcessor2(
//            IFunctionSubjectParameterConverterSelector2 parameterConverterSelector, 
//            IFunctionContext functionContext)
//        {
//            _parameterConverterSelector = parameterConverterSelector;
//            _functionContext = functionContext;
//        }

//        public void Assign(OperatorParameters parameters)
//        {
//            var functionSubject = (FunctionSubject)parameters.LeftSubject;

//            var argumentSet = functionSubject.GetArgumentSet(_parameterConverterSelector);

//            //if(argumentSet.Arguments.Any(a => a == null))
//            //{
//            //    throw new ScriptProcessingException("Empty arguments are not supported");
//            //}

//            // And one single parameter set with the exact same parameters.
//            var parameterSet = functionSubject.FindParameterSet(argumentSet);

//            functionSubject.FunctionHandler.Process(_functionContext, parameterSet, argumentSet, parameters.RightInput, parameters.Scope, parameters.Output);
//        }
//    }
//}
