namespace EtAlii.Ubigia.Api.Functional
{
    using System.Reactive.Linq;

    internal class NonRootedPathSubjectFunctionParameterConverter : INonRootedPathSubjectFunctionParameterConverter
    {
        public object Convert(FunctionSubjectArgument argument)
        {
            var constantArgument = (NonRootedPathFunctionSubjectArgument)argument;
            return Observable
                .Return<object>(constantArgument.Subject)
                .ToHotObservable();
        }
    }
}
