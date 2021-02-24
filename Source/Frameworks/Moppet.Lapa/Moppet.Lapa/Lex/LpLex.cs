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
        /// Parser search a number of characters within a predetermined range.
        /// </summary>
        /// <param name="predicate">Feature request - predicate.</param>
        /// <param name="min">The minimum number of characters allowed, but not less than.</param>
        /// <param name="max">Maximum allowable number of characters, but more.</param>
        /// <returns>parser.</returns>
        public static Expression<Func<LpText, LpNode>> Range(Expression<Func<char, bool>> predicate, int min, int max)
        {
            // ------------
            var text = Param<LpText>("text"); // Input parameter.
            var ch   = Var<char>("ch");       // Argument for predicate

            var end = Var<int>("end");
            var cur = Var<int>("cur");
            var str = Var<string>("str");
            var ind = Var<int>("ind");
            var pred  = ExtractBody(predicate, ch); // A copy of the body of the lambda expression.

            var returnTarget = Expression.Label(typeof(LpNode));
            var loopLabel    = Expression.Label();

            var block = Expression.Block
            (
                new[] { ch, end, cur, str, ind },      // Local variables
                Expression.Assign(end, LpText_Length(text)), // Initialize variables
                Expression.Assign(str, LpText_Source(text)),
                Expression.Assign(ind, LpText_Index(text)),
                Expression.Assign(cur, Expression.Constant(0)),
                Expression.IfThenElse
                (
                    test   : Expression.GreaterThan(end, Expression.Constant(max)),
                    ifTrue : Expression.Assign(end, Expression.Constant(max + 1)),
                    ifFalse: Expression.IfThen
                    (
                        Expression.LessThan(end, Expression.Constant(min)),
                        Expression.Return(returnTarget, LpNode_Fail(text))
                    )
                ),
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
                    Expression.And(Expression.GreaterThanOrEqual(cur, Expression.Constant(min)), Expression.LessThanOrEqual(cur, Expression.Constant(max))), // cur >= min && cur <= max
                    Expression.Return(returnTarget, LpNode_Take(text, cur)) // return LpNode.Take(text, cur)
                ),
                Expression.Label(returnTarget, LpNode_Fail(text))             // return LpNode.Fail(text)
            );
            var range = Expression.Lambda<Func<LpText, LpNode>>(block, text);
            return range;
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

        /// <summary>
        /// Parser an identifier or a domain name, which must start with a certain set of characters (eg letters only)
        /// and requirements for subsequent characters other. In behalf of always limited reach.
        /// Here name can be written with a hyphen (dashChar), with a dash can not be repeated more than once in a row (-), to be at the beginning or end of the name.
        /// After a dash or trailing characters allowed lastChars.
        /// </summary>
        /// <param name="firstChars">The first character or characters.</param>
        /// <param name="dashChar">Symbol denoting a dash.</param>
        /// <param name="lastChars">Valid characters after the dash or end of.</param>
        /// <param name="maxLength">The maximum length of the name.</param>
        /// <returns>greedy parser.</returns>
        public static Expression<Func<LpText, LpNode>> Name(Expression<Func<char, bool>> firstChars, char dashChar, Expression<Func<char, bool>> lastChars, int maxLength = int.MaxValue)
        {
            var text = Param<LpText>("text"); // Input parameter.
            var ch   = Var<char>("ch");       // Аргумент для firstChars и lastChars
            var end  = Var<int>("end");
            var cur  = Var<int>("cur");
            var str  = Var<string>("str");
            var ind  = Var<int>("ind");
            var dash = Var<bool>("dash");
            var first  = ExtractBody(firstChars, ch); // A copy of the body of the lambda expression.
            var last   = ExtractBody(lastChars,  ch); // A copy of the body of the lambda expression.

            var returnTarget = Expression.Label(typeof(LpNode));
            var loopLabel = Expression.Label();

            var block = Expression.Block
            (
                new[] { ch, end, cur, str, ind, dash },    // Local variables
                Expression.Assign(end, LpText_Length(text)),       // end = text.Length;
                Expression.Assign(str, LpText_Source(text)),       // str = text.Source;
                Expression.Assign(ind, LpText_Index(text)),        // ind = text.Index;
                Expression.Assign(cur,  Expression.Constant(0)),     // cur = 0;
                Expression.Assign(dash, Expression.Constant(false)), // dash = false;

                // if (end > maxLength) end = maxLength + 1;
                Expression.IfThen
                (
                    test  : Expression.GreaterThan(end, Expression.Constant(maxLength)),
                    ifTrue: Expression.Assign(end, Expression.Constant(maxLength + 1))
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
                        // ch = str[ind]
                        Expression.Assign(ch, Expression.Call(str, _stringGetCharsMethodInfo, ind)),

                        // if (last(ch)) { dash = false; ind++; cur++; goto loopLabel; }
                        Expression.IfThenElse
                        (
                            test  : last,
                            ifTrue: Expression.Block
                            (
                                Expression.Assign(dash, Expression.Constant(false)),
                                Expression.PreIncrementAssign(ind),
                                Expression.PreIncrementAssign(cur),
                                Expression.Goto(loopLabel)
                            ),

                            // else if (dashChar == ch) { if (dash) return LpNode.Fail(text); dash = true; ind++; cur++; goto loopLabel; }
                            ifFalse: Expression.IfThen
                            (
                                Expression.Equal(Expression.Constant(dashChar), ch),
                                Expression.Block
                                (
                                     Expression.IfThen(dash, Expression.Return(returnTarget, LpNode_Fail(text))),
                                     Expression.Assign(dash, Expression.Constant(true)),
                                     Expression.PreIncrementAssign(ind),
                                     Expression.PreIncrementAssign(cur),
                                     Expression.Goto(loopLabel)
                                )
                            )
                        )
                    )
                ),

                // if (dash || cur > max) return LpNode.Fail(text);
                Expression.IfThen
                (
                    Expression.Or(dash, Expression.GreaterThan(cur, Expression.Constant(maxLength))),
                    Expression.Return(returnTarget, LpNode_Fail(text))
                ),

                // return LpNode.Take(text, cur)
                Expression.Label(returnTarget, LpNode_Take(text, cur))
            );
            var ret = Expression.Lambda<Func<LpText, LpNode>>(block, text);
            return ret;

            //return new LpsParser("Name", (text) =>
            //{
            //    int end = maxLength < text.Length ? maxLength + 1 : text.Length;
            //    int cur = 0, ind = text.Index;
            //    var str = text.Source;

            //    if (cur < end && firstChars(str[ind])) { ++ind; ++cur; } else { return LpNode.Fail(text); }

            //    bool dash = false;
            //    while (cur < end)
            //    {
            //        char c = str[ind];
            //        if (lastChars(c))
            //        {
            //            dash = false;
            //        }
            //        else if (dashChar == c)
            //        {
            //            if (dash) return LpNode.Fail(text); // Повторное тире
            //            dash = true;
            //        }
            //        else break;
            //        ++ind; ++cur;
            //    }
            //    return (dash || cur > maxLength) ? LpNode.Fail(text) : LpNode.Take(text, cur);
            //});
        }


        /// <summary>
        /// Parser search a single character.
        /// The expression builder to parse the ones char.
        /// </summary>
        /// <param name="predicate">The predicate to select char.</param>
        /// <returns>The lambda expression.</returns>
        [Obsolete("Not tested.")]
        public static Expression<Func<LpText, LpNode>> One(Expression<Func<char, bool>> predicate)
        {
            var text = Param<LpText>("text");      // Input parameter.
            var ch = Var<char>("ch");              // Argument for predicate
            var pred = ExtractBody(predicate, ch); // A copy of the body of the lambda expression.
            var returnTarget = Expression.Label(typeof(LpNode));

            var block = Expression.Block
            (
                new[] { ch },
                Expression.IfThen
                (
                    Expression.GreaterThan(Expression.PropertyOrField(text, "Length"), Expression.Constant(0)),
                    Expression.Block
                    (
                        Expression.Assign(ch, Expression.Call(LpText_Source(text), _stringGetCharsMethodInfo, LpText_Index(text))), // ch = text.Source[text.Index]
                        Expression.IfThen(pred, Expression.Return(returnTarget, LpNode_Take(text, Expression.Constant(1))))
                    )
                ),
                Expression.Label(returnTarget, LpNode_Fail(text))
            );
            var one = Expression.Lambda<Func<LpText, LpNode>>(block, text);
            return one;
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
        private class LambdaParameterReplacer : ExpressionVisitor
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
