namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    internal interface IConditionalPredicateFactorySelector
    {
        IConditionalPredicateFactory Select(ConditionType criteria);
    }
}