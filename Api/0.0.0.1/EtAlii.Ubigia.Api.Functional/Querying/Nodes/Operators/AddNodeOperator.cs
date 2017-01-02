namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq.Expressions;
    using Remotion.Linq;
    using Remotion.Linq.Clauses;
    using Remotion.Linq.Clauses.ResultOperators;
    using Remotion.Linq.Clauses.StreamedData;

    public class AddNodeOperator : SequenceTypePreservingResultOperatorBase, INodeOperator
    {
        public AddNodeOperator(Expression parameter)
        {
            _parameter = parameter;
        }

        private Expression _parameter;

        public override string ToString()
        {
            return "Add (" + _parameter.ToString() + ")";
        }

        public override ResultOperatorBase Clone(CloneContext cloneContext)
        {
            return new AddNodeOperator(_parameter);
        }

        public override void TransformExpressions(
            Func<Expression, Expression> transformation)
        {
            _parameter = transformation(_parameter);
        }

        public override StreamedSequence ExecuteInMemory<T>(StreamedSequence input)
        {
            return input; // sequence is not changed by this operator
        }

        public void Operate(QueryModel queryModel, int index, IScriptAggregator scriptAggregator)
        {
            var path = (string)((ConstantExpression)_parameter).Value;
            scriptAggregator.AddAddItem(path);
        }
    }
}
