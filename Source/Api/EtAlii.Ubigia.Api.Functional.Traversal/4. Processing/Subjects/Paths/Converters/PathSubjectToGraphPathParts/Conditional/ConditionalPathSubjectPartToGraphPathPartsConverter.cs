namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class ConditionalPathSubjectPartToGraphPathPartsConverter : IConditionalPathSubjectPartToGraphPathPartsConverter
    {
        private readonly IConditionalPredicateFactorySelector _predicateFactorySelector;

        public ConditionalPathSubjectPartToGraphPathPartsConverter(IConditionalPredicateFactorySelector predicateFactorySelector)
        {
            _predicateFactorySelector = predicateFactorySelector;
        }

        public Task<GraphPathPart[]> Convert(PathSubjectPart pathSubjectPart, int pathSubjectPartPosition, PathSubjectPart previousPathSubjectPart, PathSubjectPart nextPathSubjectPart, ExecutionScope scope)
        {
            var conditions = ((ConditionalPathSubjectPart) pathSubjectPart).Conditions;

            var result = conditions
                .Select(CreateGraphCondition)
                .Cast<GraphPathPart>()
                .ToArray();
            return Task.FromResult(result);
        }

        private GraphCondition CreateGraphCondition(Condition condition)
        {
            var description = condition.ToString();
            var predicateFactory = _predicateFactorySelector.Select(condition.Type);
            var predicate = predicateFactory.Create(condition);
            return new GraphCondition(predicate, description);
        }
    }
}
