namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Structure;

    internal class ConditionalPredicateFactorySelector : Selector<ConditionType, IConditionalPredicateFactory>, IConditionalPredicateFactorySelector
    {
        public ConditionalPredicateFactorySelector(
            IEqualPredicateFactory equalPredicateFactory,
            INotEqualPredicateFactory notEqualPredicateFactory,
            ILessThanPredicateFactory lessThanPredicateFactory,
            ILessThanOrEqualPredicateFactory lessThanOrEqualPredicateFactory,
            IMoreThanPredicateFactory moreThanPredicateFactory,
            IMoreThanOrEqualPredicateFactory moreThanOrEqualPredicateFactory)
        {
            Register(c => c == ConditionType.Equal, equalPredicateFactory)
                .Register(c => c == ConditionType.NotEqual, notEqualPredicateFactory)
                .Register(c => c == ConditionType.LessThan, lessThanPredicateFactory)
                .Register(c => c == ConditionType.LessThanOrEqual, lessThanOrEqualPredicateFactory)
                .Register(c => c == ConditionType.MoreThan, moreThanPredicateFactory)
                .Register(c => c == ConditionType.MoreThanOrEqual, moreThanOrEqualPredicateFactory);
        }
    }
}
