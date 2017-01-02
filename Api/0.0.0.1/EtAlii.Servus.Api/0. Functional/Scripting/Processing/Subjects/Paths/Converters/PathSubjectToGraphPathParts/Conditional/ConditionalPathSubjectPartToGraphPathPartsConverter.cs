namespace EtAlii.Servus.Api.Functional
{
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Logical;

    internal class ConditionalPathSubjectPartToGraphPathPartsConverter : IConditionalPathSubjectPartToGraphPathPartsConverter
    {
        private readonly IProcessingContext _context;
        private readonly IConditionalPredicateFactorySelector _predicateFactorySelector;

        public ConditionalPathSubjectPartToGraphPathPartsConverter(
            IProcessingContext context,
            IConditionalPredicateFactorySelector predicateFactorySelector)
        {
            _context = context;
            _predicateFactorySelector = predicateFactorySelector;
        }

        public Task<GraphPathPart[]> Convert(PathSubjectPart pathSubjectPart, int pathSubjectPartPosition, PathSubjectPart previousPathSubjectPart, PathSubjectPart nextPathSubjectPart, ExecutionScope scope)
        {
            return Task.Run(() =>
            {
                var conditions = ((ConditionalPathSubjectPart) pathSubjectPart).Conditions;

                return conditions
                    .Select(CreateGraphCondition)
                    .Cast<GraphPathPart>()
                    .ToArray();
            });
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