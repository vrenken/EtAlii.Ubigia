namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Structure;

    internal interface IResultConverterSelector : ISelector<object, Func<object, ExecutionScope, IObserver<object>, Task>>
    {
    }
}