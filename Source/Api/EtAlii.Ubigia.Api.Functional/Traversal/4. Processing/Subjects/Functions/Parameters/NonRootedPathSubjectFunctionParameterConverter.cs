namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Reactive.Linq;
    using EtAlii.Ubigia.Api.Logical;

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
