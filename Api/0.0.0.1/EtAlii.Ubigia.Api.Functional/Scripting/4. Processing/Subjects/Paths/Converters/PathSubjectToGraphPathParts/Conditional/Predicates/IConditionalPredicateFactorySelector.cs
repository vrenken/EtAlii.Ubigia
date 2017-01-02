namespace EtAlii.Ubigia.Api.Functional
{
    internal interface IConditionalPredicateFactorySelector
    {
        IConditionalPredicateFactory Select(ConditionType criteria);
    }
}