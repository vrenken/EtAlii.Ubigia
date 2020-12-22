namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;

    internal interface IConditionalPredicateFactory
    {
        Predicate<PropertyDictionary> Create(Condition condition);
    }
}
