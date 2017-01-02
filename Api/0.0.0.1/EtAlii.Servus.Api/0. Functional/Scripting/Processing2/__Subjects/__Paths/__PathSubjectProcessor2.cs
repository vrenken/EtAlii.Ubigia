//namespace EtAlii.Servus.Api.Functional
//{
//    using System;

//    internal class PathSubjectProcessor2 : IPathSubjectProcessor2
//    {
//        private readonly IPathSubjectForOutputConverter2 _converter;

//        public PathSubjectProcessor2(
//            IPathSubjectForOutputConverter2 converter)
//        {
//            _converter = converter;
//        }

//        public void Process(Subject subject, ExecutionScope scope, IObserver<object> output)
//        {
//            _converter.Convert((PathSubject)subject, scope, output);
//            //var converter = _pathSubjectConverterSelector.Select(parameters.FuturePart, parameters.LeftPart);
//            //converter.Convert((PathSubject)parameters.Target, scope, output);
//        }
//    }
//}
