//namespace EtAlii.Ubigia.Api.Functional
//{
//    using Remotion.Linq.Clauses.Expressions
//    using Remotion.Linq.Clauses.ExpressionTreeVisitors
//    using Remotion.Linq.Parsing
//    using System
//    using System.Linq.Expressions
//    using System.Reflection
//    using System.Text

//    public class NodeExpressionTreeVisitor : ThrowingExpressionTreeVisitor
//    {
//        private readonly StringBuilder _gqlExpression = new StringBuilder()

//        public NodeExpressionTreeVisitor()
//        {
//        }

//        public string GetGqlExpression()
//        {
//            return _gqlExpression.ToString()
//        }

//        protected override Expression VisitInvocationExpression(InvocationExpression expression)
//        {
//            return base.VisitInvocationExpression(expression)
//        }
//        protected override Expression VisitQuerySourceReferenceExpression(QuerySourceReferenceExpression expression)
//        {
//            _gqlExpression.Append(expression.ReferencedQuerySource.ItemName)
//            return expression
//        }

//        protected override Expression VisitBinaryExpression(BinaryExpression expression)
//        {
//            _gqlExpression.Append("(")

//            VisitExpression(expression.Left)

//            // In production code, handle this via lookup tables.
//            switch (expression.NodeType)
//            {
//                case ExpressionType.Equal:
//                    _gqlExpression.Append(" = ")
//                    break

//                case ExpressionType.AndAlso:
//                case ExpressionType.And:
//                    _gqlExpression.Append(" and ")
//                    break

//                case ExpressionType.OrElse:
//                case ExpressionType.Or:
//                    _gqlExpression.Append(" or ")
//                    break

//                case ExpressionType.Add:
//                    _gqlExpression.Append(" + ")
//                    break

//                case ExpressionType.Subtract:
//                    _gqlExpression.Append(" - ")
//                    break

//                case ExpressionType.Multiply:
//                    _gqlExpression.Append(" * ")
//                    break

//                case ExpressionType.Divide:
//                    _gqlExpression.Append(" / ")
//                    break

//                default:
//                    base.VisitBinaryExpression(expression)
//                    break
//            }

//            VisitExpression(expression.Right)
//            _gqlExpression.Append(")")

//            return expression
//        }

//        protected override Expression VisitMemberExpression(MemberExpression expression)
//        {
//            VisitExpression(expression.Expression)
//            _gqlExpression.AppendFormat(".{0}", expression.Member.Name)

//            return expression
//        }

//        protected override Expression VisitConstantExpression(ConstantExpression expression)
//        {
//            //var namedParameter = _parameterAggregator.AddParameter(expression.Value)
//            //_gqlExpression.AppendFormat(":{0}", namedParameter.Name)

//            return expression
//        }

//        protected override Expression VisitMethodCallExpression(MethodCallExpression expression)
//        {
//            // In production code, handle this via method lookup tables.

//            var supportedMethod = typeof(string).GetRuntimeMethod("Contains", new Type[] { typeof(string) })
//            if (expression.Method.Equals(supportedMethod))
//            {
//                _gqlExpression.Append("(")
//                VisitExpression(expression.Object)
//                _gqlExpression.Append(" like '%'+")
//                VisitExpression(expression.Arguments[0])
//                _gqlExpression.Append("+'%')")
//                return expression
//            }
//            else
//            {
//                return base.VisitMethodCallExpression(expression) // throws
//            }
//        }

//        // Called when a LINQ expression type is not handled above.
//        protected override Exception CreateUnhandledItemException<T>(T unhandledItem, string visitMethod)
//        {
//            string itemText = FormatUnhandledItem(unhandledItem)
//            var message = string.Format("The expression '{0}' (type: {1}) is not supported by this LINQ provider.", itemText, typeof(T))
//            return new NotSupportedException(message)
//        }

//        private string FormatUnhandledItem<T>(T unhandledItem)
//        {
//            var itemAsExpression = unhandledItem as Expression
//            return itemAsExpression != null ? FormattingExpressionTreeVisitor.Format(itemAsExpression) : unhandledItem.ToString()
//        }
//    }
//}