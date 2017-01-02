namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    internal interface IConditionalPredicateFactory
    {
        Predicate<PropertyDictionary> Create(Condition condition);
    }
}