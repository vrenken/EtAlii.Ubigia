//namespace EtAlii.Servus.Api.Functional
//{
//    using System;

//    internal class AssignToOutputProcessor2 : IAssignToOutputProcessor2
//    {
//        private readonly IResultConverterSelector2 _resultConverterSelector;

//        public AssignToOutputProcessor2(
//            IResultConverterSelector2 resultConverterSelector)
//        {
//            _resultConverterSelector = resultConverterSelector;
//        }

//        public void Assign(OperatorParameters parameters)
//        {
//            parameters.RightInput.Subscribe(
//                onError: (e) => parameters.Output.OnError(e),
//                onCompleted: () => parameters.Output.OnCompleted(),
//                onNext: o =>
//                {
//                    // TODO: Refactor so that results are returned immediately.
//                    var outputConverter = _resultConverterSelector.Select(o);
//                    outputConverter(o, parameters.Scope, parameters.Output);
//                });
//            //parameters.Output.OnCompleted();
//        }
//    }
//}
