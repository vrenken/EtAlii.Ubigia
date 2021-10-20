namespace Moppet.Lapa
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Lexers - simple parsers for lambda expressions.
    /// Lp - Lambda Parsers.
    /// </summary>
    public static class LpLex
    {
        private static readonly MethodInfo _stringGetCharsMethodInfo;

        private static readonly ConstructorInfo _lpNodeTextConstructorInfo;
        private static readonly ConstructorInfo _lpNodeTextIntStringConstructorInfo;

        static LpLex()
        {
            var stringTypeInfo = typeof (string).GetTypeInfo();
            _stringGetCharsMethodInfo = stringTypeInfo.GetDeclaredMethod("get_Chars");

            var lpNodeTypeInfo = typeof (LpNode).GetTypeInfo();
            _lpNodeTextConstructorInfo = lpNodeTypeInfo.DeclaredConstructors.Single(
                c =>
                {
                    var parameters = c.GetParameters();
                    return parameters.Length == 1 && parameters[0].ParameterType == typeof (LpText);
                });
            _lpNodeTextIntStringConstructorInfo = lpNodeTypeInfo.DeclaredConstructors.Single(
                c =>
                {
                    var parameters = c.GetParameters();
                    return parameters.Length == 3 &&
                           parameters[0].ParameterType == typeof (LpText) &&
                           parameters[1].ParameterType == typeof (int) &&
                           parameters[2].ParameterType == typeof (string);
                });
        }

        /// <summary>
        /// Parser search of one or more characters. Greedy algorithm, but fast.
        /// </summary>
        /// <param name="predicate">predicate.</param>
        /// <returns>Parser search of one or more tokens.</returns>
        public static Expression<Func<LpText, LpNode>> OneOrMore(Expression<Func<char, bool>> predicate)
        {
            // ------------
            var text = Param<LpText>("text"); // Input parameter.
            var ch   = Var  <char>  ("ch");   // Argument for predicate
            var end  = Var<int>     ("end");
            var cur  = Var<int>     ("cur");
            var str  = Var<string>  ("str");
            var ind  = Var<int>     ("ind");
            var pred       = ExtractBody(predicate, ch); // A copy of the body of the lambda expression.

            var returnTarget = Expression.Label(typeof(LpNode));
            var loopLabel    = Expression.Label();

            var block = Expression.Block
            (
                new[] { ch, end, cur, str, ind },      // Local variables
                Expression.Assign(end, LpText_Length(text)), // Initialize variables
                Expression.Assign(str, LpText_Source(text)),
                Expression.Assign(ind, LpText_Index(text)),
                Expression.Assign(cur, Expression.Constant(0)),

                Expression.Label(loopLabel), // Return point. For a cycle.
                Expression.IfThen
                (
                    Expression.LessThan(cur, end),
                    Expression.Block
                    (
                        Expression.Assign(ch, Expression.Call(str, _stringGetCharsMethodInfo, ind)), // ch = str[ind]
                        Expression.IfThen                                                                              // if (predicate(ch)) { ind++; cur++; goto loopLabel; }
                        (
                            pred, Expression.Block
                            (
                                Expression.PreIncrementAssign(ind),
                                Expression.PreIncrementAssign(cur),
                                Expression.Goto(loopLabel)
                            )
                        )
                    )
                ),
                Expression.IfThen
                (
                    Expression.GreaterThan(cur, Expression.Constant(0)),      // cur > 0
                    Expression.Return(returnTarget, LpNode_Take(text, cur)) // return LpNode.Take(text, cur)
                ),
                Expression.Label(returnTarget, LpNode_Fail(text))             // return LpNode.Fail(text)
            );
            var oneOrMore = Expression.Lambda<Func<LpText, LpNode>>(block, text);
            return oneOrMore;
        }

        /// <summary>
        /// Search parser zero or more characters. Greedy algorithm, but fast.
        /// </summary>
        /// <param name="predicate">predicate.</param>
        /// <returns>Parser search of one or more tokens.</returns>
        public static Expression<Func<LpText, LpNode>> ZeroOrMore(Expression<Func<char, bool>> predicate)
        {
            var text = Param<LpText>("text"); // Input parameter.
            var ch   = Var<char>("ch");       // Argument for predicate
            var end  = Var<int>("end");
            var cur  = Var<int>("cur");
            var str  = Var<string>("str");
            var ind  = Var<int>("ind");
            var pred   = ExtractBody(predicate, ch); // A copy of the body of the lambda expression.

            var returnTarget = Expression.Label(typeof(LpNode));
            var loopLabel = Expression.Label();

            var block = Expression.Block
            (
                new[] { ch, end, cur, str, ind },      // Local variables
                Expression.Assign(end, LpText_Length(text)), // Initialize variables
                Expression.Assign(str, LpText_Source(text)),
                Expression.Assign(ind, LpText_Index(text)),
                Expression.Assign(cur, Expression.Constant(0)),

                Expression.Label(loopLabel), // Return point. For a cycle.
                Expression.IfThen
                (
                    Expression.LessThan(cur, end),
                    Expression.Block
                    (
                        Expression.Assign(ch, Expression.Call(str, _stringGetCharsMethodInfo, ind)), // ch = str[ind]
                        Expression.IfThen                                                                              // if (predicate(ch)) { ind++; cur++; goto loopLabel; }
                        (
                            pred, Expression.Block
                            (
                                Expression.PreIncrementAssign(ind),
                                Expression.PreIncrementAssign(cur),
                                Expression.Goto(loopLabel)
                            )
                        )
                    )
                ),
                Expression.Label(returnTarget, LpNode_Take(text, cur)) // return LpNode.Take(text, cur)
            );
            var zeroOrMore = Expression.Lambda<Func<LpText, LpNode>>(block, text);
            return zeroOrMore;
        }

        /// <summary>
        /// Parser an identifier or name (term), which must start with a certain set of characters (eg only with the letters), and
        /// requirements followed by other characters. Also the name is always limited reach.
        /// </summary>
        /// <param name="firstChar">The first character.</param>
        /// <param name="maybeNextChars">Zero or more subsequent characters.</param>
        /// <param name="maxLength">The maximum length of the name.</param>
        /// <returns>greedy parser.</returns>
        public static Expression<Func<LpText, LpNode>> Name(Expression<Func<char, bool>> firstChar, Expression<Func<char, bool>> maybeNextChars, int maxLength)
        {
            var text = Param<LpText>("text"); // Input parameter.
            var ch = Var<char>("ch");         // Argument for predicate
            var end = Var<int>("end");
            var cur = Var<int>("cur");
            var str = Var<string>("str");
            var ind = Var<int>("ind");
            var first = ExtractBody(firstChar, ch);      // A copy of the body of the lambda expression.
            var next = ExtractBody(maybeNextChars, ch); // A copy of the body of the lambda expression.

            var returnTarget = Expression.Label(typeof(LpNode));
            var loopLabel = Expression.Label();

            var block = Expression.Block
            (
                new[] { ch, end, cur, str, ind },      // Local variables
                Expression.Assign(end, LpText_Length(text)), // Initialize variables
                Expression.Assign(str, LpText_Source(text)),
                Expression.Assign(ind, LpText_Index(text)),
                Expression.Assign(cur, Expression.Constant(0)),

                // if (end > maxLength) end = maxLength + 1;
                Expression.IfThen
                (
                    test   : Expression.GreaterThan(end, Expression.Constant(maxLength)),
                    ifTrue : Expression.Assign(end, Expression.Constant(maxLength + 1))
                ),

                // if (end <= 0) return LpNode.Fail(text)
                Expression.IfThen(Expression.LessThanOrEqual(end, Expression.Constant(0)), Expression.Return(returnTarget, LpNode_Fail(text))),
                Expression.Assign(ch, Expression.Call(str, _stringGetCharsMethodInfo, ind)), // ch = str[ind]

                // if (!first(ch)) return LpNode.Fail(text)
                Expression.IfThen(Expression.Not(first), Expression.Return(returnTarget, LpNode_Fail(text))),

                // ++cur; ++ind;
                Expression.PreIncrementAssign(ind), Expression.PreIncrementAssign(cur),

                // while(cur < end)
                Expression.Label(loopLabel), // Return point. For a cycle.
                Expression.IfThen
                (
                    Expression.LessThan(cur, end),
                    Expression.Block
                    (
                        Expression.Assign(ch, Expression.Call(str, _stringGetCharsMethodInfo, ind)), // ch = str[ind]
                        Expression.IfThen                                                                              // if (next(ch)) { ind++; cur++; goto loopLabel; }
                        (
                            next, Expression.Block
                            (
                                Expression.PreIncrementAssign(ind),
                                Expression.PreIncrementAssign(cur),
                                Expression.Goto(loopLabel)
                            )
                        )
                    )
                ),

                // if (cur <= max) return LpNode.Take(text, cur)
                Expression.IfThen
                (
                    Expression.LessThanOrEqual(cur, Expression.Constant(maxLength)),
                    Expression.Return(returnTarget, LpNode_Take(text, cur))
                ),

                // return LpNode.Fail(text)
                Expression.Label(returnTarget, LpNode_Fail(text))
            );
            var ret = Expression.Lambda<Func<LpText, LpNode>>(block, text);
            return ret;
        }

        #region Helpers

        /// <summary>
        /// Выражение - паременная.
        /// </summary>
        /// <typeparam name="T">Тип.</typeparam>
        /// <param name="n">Имя.</param>
        /// <returns>Выражение.</returns>
        private static ParameterExpression Var<T>(string n)
        {
            return Expression.Variable(typeof(T), n);
        }

        /// <summary>
        /// Выражение - аргумент.
        /// </summary>
        /// <typeparam name="T">Тип.</typeparam>
        /// <param name="n">Имя.</param>
        /// <returns>Выражение.</returns>
        private static ParameterExpression Param<T>(string n)
        {
            return Expression.Parameter(typeof(T), n);
        }

        /// <summary>
        /// Вызов функции LpNode.Take.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="len">Сколько символов взять.</param>
        /// <returns>Выражение.</returns>
        private static Expression LpNode_Take(Expression text, Expression len)
        {
            // return Expression.Call(typeof(LpNode).GetMethod("Take", new Type[] { typeof(LpText), typeof(int) }), text, len);
            return Expression.New(_lpNodeTextIntStringConstructorInfo, text, len, Expression.Constant(null, typeof(string)));
        }

        /// <summary>
        /// Вызов функции LpNode.Fail.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <returns>Выражение.</returns>
        private static Expression LpNode_Fail(Expression text)
        {
            // return Expression.Call(typeof(LpNode).GetMethod("Fail", new Type[] { typeof(LpText) }), text);
            return Expression.New(_lpNodeTextConstructorInfo, text);
        }

        /// <summary>
        /// Доступ к свойству LpText.Length.
        /// </summary>
        /// <param name="text">Переменная.</param>
        /// <returns>Доступ к свойству.</returns>
        private static Expression LpText_Length(Expression text)
        {
            return Expression.PropertyOrField(text, "Length");
        }

        /// <summary>
        /// Доступ к свойству LpText.Source.
        /// </summary>
        /// <param name="text">Переменная.</param>
        /// <returns>Доступ к свойству.</returns>
        private static Expression LpText_Source(Expression text)
        {
            return Expression.PropertyOrField(text, "Source");
        }

        /// <summary>
        /// Доступ к свойству LpText.Index.
        /// </summary>
        /// <param name="text">Переменная.</param>
        /// <returns>Доступ к свойству.</returns>
        private static Expression LpText_Index(Expression text)
        {
            return Expression.PropertyOrField(text, "Index");
        }


        /// <summary>
        /// Expands the expression, making a copy and replacing the argument.
        /// </summary>
        /// <param name="predicate">Выражение.</param>
        /// <param name="arg">Аргумент.</param>
        /// <returns>Тело выражения.</returns>
        private static Expression ExtractBody(Expression<Func<char, bool>> predicate, ParameterExpression arg)
        {
            var rep = new LambdaParameterReplacer();
            return rep.ReplaceArgs(predicate, predicate.Parameters[0], arg).Body;
        }

        /// <summary>
        /// A class for copying lambda expressions.
        /// </summary>
        private sealed class LambdaParameterReplacer : ExpressionVisitor
        {
            private IEnumerable<ParameterExpression> _lambdaArgsToSearch = new List<ParameterExpression>();
            private IEnumerable<ParameterExpression> _lambdaArgsToReplace = new List<ParameterExpression>();


            /// <summary>
            /// Copies the expression, substituting others for some parameters.
            /// </summary>
            /// <typeparam name="TDelegate">Делегат.</typeparam>
            /// <param name="lambda">Лямбда выражение.</param>
            /// <param name="oldParam">То, что нужно найти.</param>
            /// <param name="newParam">То, чем нужно заменить.</param>
            /// <returns>Копия.</returns>
            public Expression<TDelegate> ReplaceArgs<TDelegate>(Expression<TDelegate> lambda, ParameterExpression oldParam, ParameterExpression newParam)
            {
                return ReplaceArgs(lambda, new List<ParameterExpression> { oldParam }, new List<ParameterExpression> { newParam });
            }

            /// <summary>
            /// Copies the expression, substituting others for some parameters.
            /// The size of the args1 and args2 arrays must be identical.
            /// </summary>
            /// <typeparam name="TDelegate">Делегат.</typeparam>
            /// <param name="lambda">Лямбда выражение.</param>
            /// <param name="argsToSearch">Аргументы, которые нужно найти.</param>
            /// <param name="argsToReplace">Аргументы, которыми нужно заменить.</param>
            /// <returns>Копия.</returns>
            private Expression<TDelegate> ReplaceArgs<TDelegate>(Expression<TDelegate> lambda, IEnumerable<ParameterExpression> argsToSearch, IEnumerable<ParameterExpression> argsToReplace)
            {
                _lambdaArgsToSearch = argsToSearch;
                _lambdaArgsToReplace = argsToReplace;
                var lambdaСopy = (Expression<TDelegate>)VisitLambda(lambda);
                return lambdaСopy;
            }

            /// <summary>
            /// A function that is called for each found parameter at any nesting level.
            /// </summary>
            /// <param name="node">Параметр.</param>
            /// <returns>Тот же параметр или подставной.</returns>
            protected override Expression VisitParameter(ParameterExpression node)
            {
                var replacer = _lambdaArgsToSearch.Where(t => t == node).Select((_, i) => _lambdaArgsToReplace.Skip(i).First()).FirstOrDefault();
                if (replacer != null)
                    return replacer;
                return base.VisitParameter(node);
            }
        }

        #endregion
    }
}
