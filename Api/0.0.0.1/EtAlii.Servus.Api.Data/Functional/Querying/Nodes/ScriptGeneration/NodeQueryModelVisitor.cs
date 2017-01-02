namespace EtAlii.Servus.Api.Data
{
    using Remotion.Linq;
    using Remotion.Linq.Clauses;
    using Remotion.Linq.Clauses.ResultOperators;
    using System;
    using System.Linq.Expressions;

    public class NodeQueryModelVisitor : QueryModelVisitorBase
    {
        private readonly ScriptAggregator _scriptAggregator;

        public ResultOperator ResultOperator { get; private set; }

        public NodeQueryModelVisitor(ScriptAggregator scriptAggregator)
        {
            _scriptAggregator = scriptAggregator;
        }

        public override void VisitQueryModel(QueryModel queryModel)
        {
            //_scriptAggregator.Clear();

            queryModel.SelectClause.Accept(this, queryModel);
            queryModel.MainFromClause.Accept(this, queryModel);
            VisitBodyClauses(queryModel.BodyClauses, queryModel);
            VisitResultOperators(queryModel.ResultOperators, queryModel);
        }

        public override void VisitResultOperator(ResultOperatorBase resultOperator, QueryModel queryModel, int index)
        {
            var nodeOperator = resultOperator as INodeOperator;
            if (nodeOperator != null)
            {
                nodeOperator.Operate(queryModel, index, _scriptAggregator);
            }
            else if(resultOperator is AnyResultOperator)
            {
                ResultOperator = ResultOperator.Any;
            }
            else if (resultOperator is CountResultOperator)
            {
                ResultOperator = ResultOperator.Count;
            }
            else if (resultOperator is CastResultOperator)
            {
                ResultOperator = ResultOperator.Cast;
            }

            base.VisitResultOperator(resultOperator, queryModel, index);
        }

        public override void VisitMainFromClause(MainFromClause fromClause, QueryModel queryModel)
        {
            var constantExpression = fromClause.FromExpression as ConstantExpression;
            if(constantExpression != null)
            {
                var queryable = constantExpression.Value as NodeQueryable<INode>;
                if(queryable != null)
                {
                    _scriptAggregator.AddVariableAssignment(queryable);
                }
                else
                {
                    throw new NotSupportedException("Check needed");
                }
            }
            else
            {
                throw new NotSupportedException("Check needed");
            }

            base.VisitMainFromClause(fromClause, queryModel);
        }

        public override void VisitGroupJoinClause(GroupJoinClause groupJoinClause, QueryModel queryModel, int index)
        {
            throw new NotSupportedException();
            //throw new NotSupportedException("Adding a join ... into ... implementation to the query provider is left to the reader for extra points.");
        }

        public string GetScriptText()
        {
            return _scriptAggregator.GetScript();
        }
    }
}