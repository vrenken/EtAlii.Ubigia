namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal interface IConditionalPredicateFactorySelector
    {
        IConditionalPredicateFactory Select(ConditionType criteria);
    }
}
