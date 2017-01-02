namespace EtAlii.Servus.Api.Functional
{
    using System.Reactive.Linq;

    internal class PathSubjectFunctionParameterConverter : IPathSubjectFunctionParameterConverter
    {
        public object Convert(FunctionSubjectArgument argument)
        {
            var constantArgument = (PathFunctionSubjectArgument)argument;
            return Observable
                .Return<object>(new PathSubject(constantArgument.Parts))
                .ToHotObservable();
        }
    }
}
