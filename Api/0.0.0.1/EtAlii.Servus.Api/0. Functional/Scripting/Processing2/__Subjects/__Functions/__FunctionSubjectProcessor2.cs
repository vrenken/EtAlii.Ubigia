//namespace EtAlii.Servus.Api.Functional
//{
//    using System;
//    using System.Reactive.Linq;

//    internal class FunctionSubjectProcessor2 : IFunctionSubjectProcessor2
//    {
//        private readonly IFunctionSubjectParameterConverterSelector2 _parameterConverterSelector;
//        private readonly IFunctionContext _functionContext;

//        public FunctionSubjectProcessor2(
//            IFunctionSubjectParameterConverterSelector2 parameterConverterSelector,
//            IFunctionContext functionContext)
//        {
//            _parameterConverterSelector = parameterConverterSelector;
//            _functionContext = functionContext;
//        }

//        public void Process(Subject subject, ExecutionScope scope, IObserver<object> output)
//        {
//            var functionSubject = (FunctionSubject)subject;

//            var argumentSet = functionSubject.GetArgumentSet(_parameterConverterSelector);

//            //if(argumentSet.Arguments.Any(a => a == null))
//            //{
//            //    throw new ScriptProcessingException("Empty arguments are not supported");
//            //}

//            // And one single parameter set with the exact same parameters.
//            var parameterSet = functionSubject.FindParameterSet(argumentSet);

//            functionSubject.FunctionHandler.Process(_functionContext, parameterSet, argumentSet, Observable.Empty<object>(), scope, output);
//        }

//        //private ParameterSet FindParameterSet(FunctionSubject functionSubject, ArgumentSet argumentSet)
//        //{
//        //    var parameterSet = functionSubject.PossibleParameterSets
//        //        .SingleOrDefault(parameters => HasSameParameters(parameters, argumentSet));

//        //    if (parameterSet == null)
//        //    {
//        //        var message = String.Format("No function found with name '{0}' and parameters [{1}]", functionSubject.Name, argumentSet);
//        //        throw new ScriptProcessingException(message);
//        //    }

//        //    return parameterSet;
//        //}

//        //private bool HasSameParameters(ParameterSet parameterSet, ArgumentSet argumentSet)
//        //{
//        //    var result = true;
//        //    for (int i = 0; i < argumentSet.Arguments.Length; i++)
//        //    {
//        //        var parameterTypeInfo = parameterSet.ParameterTypeInfos[i];
//        //        var argumentTypeInfo = argumentSet.ArgumentTypeInfos[i];
//        //        if (!parameterTypeInfo.IsAssignableFrom(argumentTypeInfo))
//        //        {
//        //            result = false;
//        //            break;
//        //        }
//        //    }
//        //    return result;
//        //}
//    }
//}
