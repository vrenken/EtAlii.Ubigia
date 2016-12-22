namespace EtAlii.Servus.Api.Functional
{
    using System;
    using EtAlii.xTechnology.Structure;

    internal interface IResultConverterSelector : ISelector<object, Action<object, ExecutionScope, IObserver<object>>>
    {
    }
}