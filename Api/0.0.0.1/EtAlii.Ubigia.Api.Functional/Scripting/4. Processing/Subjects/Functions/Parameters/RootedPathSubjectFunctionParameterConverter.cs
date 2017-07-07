namespace EtAlii.Ubigia.Api.Functional
{
    using System.Reactive.Linq;
    using EtAlii.Ubigia.Api.Logical;

    internal class RootedPathSubjectFunctionParameterConverter : IRootedPathSubjectFunctionParameterConverter
    {
        public object Convert(FunctionSubjectArgument argument)
        {
            var constantArgument = (RootedPathFunctionSubjectArgument)argument;
            return Observable
                .Return<object>(constantArgument.Subject)
                .ToHotObservable();
        }
    }
}
