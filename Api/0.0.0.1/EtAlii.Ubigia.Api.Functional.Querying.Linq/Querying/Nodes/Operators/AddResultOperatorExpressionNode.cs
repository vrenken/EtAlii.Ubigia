namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq.Expressions;
    using Remotion.Linq.Clauses;
    using Remotion.Linq.Parsing.Structure.IntermediateModel;

    public class AddResultOperatorExpressionNode : ResultOperatorExpressionNodeBase
    {
        private readonly ConstantExpression _constantExpression;

        public AddResultOperatorExpressionNode(MethodCallExpressionParseInfo parseInfo, ConstantExpression parameter)
            : base(parseInfo, null, null)
        {
            _constantExpression = parameter;
        }

        protected override ResultOperatorBase CreateResultOperator(
            ClauseGenerationContext clauseGenerationContext)
        {
            return new AddNodeOperator(_constantExpression);
        }

        public override Expression Resolve(
             ParameterExpression inputParameter,
             Expression expressionToBeResolved,
             ClauseGenerationContext clauseGenerationContext)
        {
            return Source.Resolve(inputParameter, expressionToBeResolved, clauseGenerationContext); 
        }
    }
}