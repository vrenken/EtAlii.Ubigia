namespace EtAlii.Servus.Api.Functional
{
    internal interface IConditionalPredicateFactorySelector
    {
        IConditionalPredicateFactory Select(ConditionType criteria);
    }
}