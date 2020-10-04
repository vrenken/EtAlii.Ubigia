namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;

    internal interface IConditionalPredicateFactory
    {
        Predicate<PropertyDictionary> Create(Condition condition);
    }
}