using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Moppet.Lapa
{
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
            var p_text = Param<LpText>("text"); // Входной параметр.
            var p_ch   = Var  <char>  ("ch");   // Аргумент для predicate
            var p_end  = Var<int>     ("end");
            var p_cur  = Var<int>     ("cur");
            var p_str  = Var<string>  ("str");
            var p_ind  = Var<int>     ("ind");
            var pred   = ExtractBody(predicate, p_ch); // Копия тела лямбда выражения.

            var returnTarget = Expression.Label(typeof(LpNode));
            var loopLabel    = Expression.Label();

            var block = Expression.Block
            (
                new[] { p_ch, p_end, p_cur, p_str, p_ind },      // Локальные переменные
                Expression.Assign(p_end, LpText_Length(p_text)), // Инициализация переменных
                Expression.Assign(p_str, LpText_Source(p_text)),
                Expression.Assign(p_ind, LpText_Index(p_text)),
                Expression.Assign(p_cur, Expression.Constant(0)),

                Expression.Label(loopLabel), // Точка возврата. Для цикла.
                Expression.IfThen
                (
                    Expression.LessThan(p_cur, p_end),
                    Expression.Block
                    (
                        Expression.Assign(p_ch, Expression.Call(p_str, _stringGetCharsMethodInfo, p_ind)), // ch = str[ind]
                        Expression.IfThen                                                                              // if (predicate(ch)) { ind++; cur++; goto loopLabel; }
                        (
                            pred, Expression.Block
                            (
                                Expression.PreIncrementAssign(p_ind),
                                Expression.PreIncrementAssign(p_cur),
                                Expression.Goto(loopLabel)
                            )
                        )
                    )
                ),
                Expression.IfThen
                (
                    Expression.GreaterThan(p_cur, Expression.Constant(0)),      // cur > 0
                    Expression.Return(returnTarget, LpNode_Take(p_text, p_cur)) // return LpNode.Take(text, cur)
                ),
                Expression.Label(returnTarget, LpNode_Fail(p_text))             // return LpNode.Fail(text)
            );
            var oneOrMore = Expression.Lambda<Func<LpText, LpNode>>(block, p_text);
            return oneOrMore;
        }

        /// <summary>
        /// Search parser zero or more characters. Greedy algorithm, but fast.
        /// </summary>
        /// <param name="predicate">predicate.</param>
        /// <returns>Parser search of one or more tokens.</returns>
        public static Expression<Func<LpText, LpNode>> ZeroOrMore(Expression<Func<char, bool>> predicate)
        {
            var p_text = Param<LpText>("text"); // Входной параметр.
            var p_ch   = Var<char>("ch");       // Аргумент для predicate
            var p_end  = Var<int>("end");
            var p_cur  = Var<int>("cur");
            var p_str  = Var<string>("str");
            var p_ind  = Var<int>("ind");
            var pred   = ExtractBody(predicate, p_ch); // Копия тела лямбда выражения.

            var returnTarget = Expression.Label(typeof(LpNode));
            var loopLabel = Expression.Label();

            var block = Expression.Block
            (
                new[] { p_ch, p_end, p_cur, p_str, p_ind },      // Локальные переменные
                Expression.Assign(p_end, LpText_Length(p_text)), // Инициализация переменных
                Expression.Assign(p_str, LpText_Source(p_text)),
                Expression.Assign(p_ind, LpText_Index(p_text)),
                Expression.Assign(p_cur, Expression.Constant(0)),

                Expression.Label(loopLabel), // Точка возврата. Для цикла.
                Expression.IfThen
                (
                    Expression.LessThan(p_cur, p_end),
                    Expression.Block
                    (
                        Expression.Assign(p_ch, Expression.Call(p_str, _stringGetCharsMethodInfo, p_ind)), // ch = str[ind]
                        Expression.IfThen                                                                              // if (predicate(ch)) { ind++; cur++; goto loopLabel; }
                        (
                            pred, Expression.Block
                            (
                                Expression.PreIncrementAssign(p_ind),
                                Expression.PreIncrementAssign(p_cur),
                                Expression.Goto(loopLabel)
                            )
                        )
                    )
                ),
                Expression.Label(returnTarget, LpNode_Take(p_text, p_cur)) // return LpNode.Take(text, cur)
            );
            var zeroOrMore = Expression.Lambda<Func<LpText, LpNode>>(block, p_text);
            return zeroOrMore;
        }

        /// <summary>
        /// Parser search a number of characters within a predetermined range.
        /// </summary>
        /// <param name="predicate">Feature request - predicate.</param>
        /// <param name="min">The minimum number of characters allowed, but not less than.</param>
        /// <param name="max">Maksimalnoe allowable number of characters, but more.</param>
        /// <returns>parser.</returns>
        public static Expression<Func<LpText, LpNode>> Range(Expression<Func<char, bool>> predicate, int min, int max)
        {
            // ------------
            var p_text = Param<LpText>("text"); // Входной параметр.
            var p_ch   = Var<char>("ch");       // Аргумент для predicate

            var p_end = Var<int>("end");
            var p_cur = Var<int>("cur");
            var p_str = Var<string>("str");
            var p_ind = Var<int>("ind");
            var pred  = ExtractBody(predicate, p_ch); // Копия тела лямбда выражения.

            var returnTarget = Expression.Label(typeof(LpNode));
            var loopLabel    = Expression.Label();

            var block = Expression.Block
            (
                new[] { p_ch, p_end, p_cur, p_str, p_ind },      // Локальные переменные
                Expression.Assign(p_end, LpText_Length(p_text)), // Инициализация переменных
                Expression.Assign(p_str, LpText_Source(p_text)),
                Expression.Assign(p_ind, LpText_Index(p_text)),
                Expression.Assign(p_cur, Expression.Constant(0)),
                Expression.IfThenElse
                (
                    test   : Expression.GreaterThan(p_end, Expression.Constant(max)),
                    ifTrue : Expression.Assign(p_end, Expression.Constant(max + 1)),
                    ifFalse: Expression.IfThen
                    (
                        Expression.LessThan(p_end, Expression.Constant(min)),
                        Expression.Return(returnTarget, LpNode_Fail(p_text))
                    )
                ),
                Expression.Label(loopLabel), // Точка возврата. Для цикла.
                Expression.IfThen
                (
                    Expression.LessThan(p_cur, p_end),
                    Expression.Block
                    (
                        Expression.Assign(p_ch, Expression.Call(p_str, _stringGetCharsMethodInfo, p_ind)), // ch = str[ind]
                        Expression.IfThen                                                                              // if (predicate(ch)) { ind++; cur++; goto loopLabel; }
                        (
                            pred, Expression.Block
                            (
                                Expression.PreIncrementAssign(p_ind),
                                Expression.PreIncrementAssign(p_cur),
                                Expression.Goto(loopLabel)
                            )
                        )
                    )
                ),
                Expression.IfThen
                (
                    Expression.And(Expression.GreaterThanOrEqual(p_cur, Expression.Constant(min)), Expression.LessThanOrEqual(p_cur, Expression.Constant(max))), // cur >= min && cur <= max
                    Expression.Return(returnTarget, LpNode_Take(p_text, p_cur)) // return LpNode.Take(text, cur)
                ),
                Expression.Label(returnTarget, LpNode_Fail(p_text))             // return LpNode.Fail(text)
            );
            var range = Expression.Lambda<Func<LpText, LpNode>>(block, p_text);
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
            var p_text = Param<LpText>("text"); // Входной параметр.
            var p_ch = Var<char>("ch");         // Аргумент для predicate
            var p_end = Var<int>("end");
            var p_cur = Var<int>("cur");
            var p_str = Var<string>("str");
            var p_ind = Var<int>("ind");
            var first = ExtractBody(firstChar, p_ch);      // Копия тела лямбда выражения.
            var next = ExtractBody(maybeNextChars, p_ch); // Копия тела лямбда выражения.

            var returnTarget = Expression.Label(typeof(LpNode));
            var loopLabel = Expression.Label();

            var block = Expression.Block
            (
                new[] { p_ch, p_end, p_cur, p_str, p_ind },      // Локальные переменные
                Expression.Assign(p_end, LpText_Length(p_text)), // Инициализация переменных
                Expression.Assign(p_str, LpText_Source(p_text)),
                Expression.Assign(p_ind, LpText_Index(p_text)),
                Expression.Assign(p_cur, Expression.Constant(0)),

                // if (end > maxLength) end = maxLength + 1;
                Expression.IfThen
                (
                    test   : Expression.GreaterThan(p_end, Expression.Constant(maxLength)),
                    ifTrue : Expression.Assign(p_end, Expression.Constant(maxLength + 1))
                ),

                // if (end <= 0) return LpNode.Fail(text)
                Expression.IfThen(Expression.LessThanOrEqual(p_end, Expression.Constant(0)), Expression.Return(returnTarget, LpNode_Fail(p_text))),
                Expression.Assign(p_ch, Expression.Call(p_str, _stringGetCharsMethodInfo, p_ind)), // ch = str[ind]

                // if (!first(ch)) return LpNode.Fail(text)
                Expression.IfThen(Expression.Not(first), Expression.Return(returnTarget, LpNode_Fail(p_text))),

                // ++cur; ++ind;
                Expression.PreIncrementAssign(p_ind), Expression.PreIncrementAssign(p_cur),

                // while(cur < end)
                Expression.Label(loopLabel), // Точка возврата. Для цикла.
                Expression.IfThen
                (
                    Expression.LessThan(p_cur, p_end),
                    Expression.Block
                    (
                        Expression.Assign(p_ch, Expression.Call(p_str, _stringGetCharsMethodInfo, p_ind)), // ch = str[ind]
                        Expression.IfThen                                                                              // if (next(ch)) { ind++; cur++; goto loopLabel; }
                        (
                            next, Expression.Block
                            (
                                Expression.PreIncrementAssign(p_ind),
                                Expression.PreIncrementAssign(p_cur),
                                Expression.Goto(loopLabel)
                            )
                        )
                    )
                ),

                // if (cur <= max) return LpNode.Take(text, cur)
                Expression.IfThen
                (
                    Expression.LessThanOrEqual(p_cur, Expression.Constant(maxLength)),
                    Expression.Return(returnTarget, LpNode_Take(p_text, p_cur))
                ),

                // return LpNode.Fail(text)
                Expression.Label(returnTarget, LpNode_Fail(p_text))
            );
            var ret = Expression.Lambda<Func<LpText, LpNode>>(block, p_text);
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
            var p_text = Param<LpText>("text"); // Входной параметр.
            var p_ch   = Var<char>("ch");       // Аргумент для firstChars и lastChars
            var p_end  = Var<int>("end");
            var p_cur  = Var<int>("cur");
            var p_str  = Var<string>("str");
            var p_ind  = Var<int>("ind");
            var p_dash = Var<bool>("dash");
            var first  = ExtractBody(firstChars, p_ch); // Копия тела лямбда выражения.
            var last   = ExtractBody(lastChars,  p_ch); // Копия тела лямбда выражения.

            var returnTarget = Expression.Label(typeof(LpNode));
            var loopLabel = Expression.Label();

            var block = Expression.Block
            (
                new[] { p_ch, p_end, p_cur, p_str, p_ind, p_dash },    // Локальные переменные
                Expression.Assign(p_end, LpText_Length(p_text)),       // end = text.Length;
                Expression.Assign(p_str, LpText_Source(p_text)),       // str = text.Source;
                Expression.Assign(p_ind, LpText_Index(p_text)),        // ind = text.Index;
                Expression.Assign(p_cur,  Expression.Constant(0)),     // cur = 0;
                Expression.Assign(p_dash, Expression.Constant(false)), // dash = false;

                // if (end > maxLength) end = maxLength + 1;
                Expression.IfThen
                (
                    test  : Expression.GreaterThan(p_end, Expression.Constant(maxLength)),
                    ifTrue: Expression.Assign(p_end, Expression.Constant(maxLength + 1))
                ),

                // if (end <= 0) return LpNode.Fail(text)
                Expression.IfThen(Expression.LessThanOrEqual(p_end, Expression.Constant(0)), Expression.Return(returnTarget, LpNode_Fail(p_text))),
                Expression.Assign(p_ch, Expression.Call(p_str, _stringGetCharsMethodInfo, p_ind)), // ch = str[ind]

                // if (!first(ch)) return LpNode.Fail(text)
                Expression.IfThen(Expression.Not(first), Expression.Return(returnTarget, LpNode_Fail(p_text))),

                // ++cur; ++ind;
                Expression.PreIncrementAssign(p_ind), Expression.PreIncrementAssign(p_cur),

                // while(cur < end)
                Expression.Label(loopLabel), // Точка возврата. Для цикла.
                Expression.IfThen
                (
                    Expression.LessThan(p_cur, p_end),
                    Expression.Block
                    (
                        // ch = str[ind]
                        Expression.Assign(p_ch, Expression.Call(p_str, _stringGetCharsMethodInfo, p_ind)),

                        // if (last(ch)) { dash = false; ind++; cur++; goto loopLabel; }
                        Expression.IfThenElse
                        (
                            test  : last,
                            ifTrue: Expression.Block
                            (
                                Expression.Assign(p_dash, Expression.Constant(false)),
                                Expression.PreIncrementAssign(p_ind),
                                Expression.PreIncrementAssign(p_cur),
                                Expression.Goto(loopLabel)
                            ),

                            // else if (dashChar == ch) { if (dash) return LpNode.Fail(text); dash = true; ind++; cur++; goto loopLabel; }
                            ifFalse: Expression.IfThen
                            (
                                Expression.Equal(Expression.Constant(dashChar), p_ch),
                                Expression.Block
                                (
                                     Expression.IfThen(p_dash, Expression.Return(returnTarget, LpNode_Fail(p_text))),
                                     Expression.Assign(p_dash, Expression.Constant(true)),
                                     Expression.PreIncrementAssign(p_ind),
                                     Expression.PreIncrementAssign(p_cur),
                                     Expression.Goto(loopLabel)
                                )
                            )
                        )
                    )
                ),

                // if (dash || cur > max) return LpNode.Fail(text);
                Expression.IfThen
                (
                    Expression.Or(p_dash, Expression.GreaterThan(p_cur, Expression.Constant(maxLength))),
                    Expression.Return(returnTarget, LpNode_Fail(p_text))
                ),

                // return LpNode.Take(text, cur)
                Expression.Label(returnTarget, LpNode_Take(p_text, p_cur))
            );
            var ret = Expression.Lambda<Func<LpText, LpNode>>(block, p_text);
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
            var p_text = Param<LpText>("text");      // Входной параметр.
            var p_ch = Var<char>("ch");              // Аргумент для predicate
            var pred = ExtractBody(predicate, p_ch); // Копия тела лямбда выражения.
            var returnTarget = Expression.Label(typeof(LpNode));

            var block = Expression.Block
            (
                new[] { p_ch },
                Expression.IfThen
                (
                    Expression.GreaterThan(Expression.PropertyOrField(p_text, "Length"), Expression.Constant(0)),
                    Expression.Block
                    (
                        Expression.Assign(p_ch, Expression.Call(LpText_Source(p_text), _stringGetCharsMethodInfo, LpText_Index(p_text))), // ch = text.Source[text.Index]
                        Expression.IfThen(pred, Expression.Return(returnTarget, LpNode_Take(p_text, Expression.Constant(1))))
                    )
                ),
                Expression.Label(returnTarget, LpNode_Fail(p_text))
            );
            var one = Expression.Lambda<Func<LpText, LpNode>>(block, p_text);
            return one;
        }



        #region Helpers

        /// <summary>
        /// Выражение - паременная.
        /// </summary>
        /// <typeparam name="T">Тип.</typeparam>
        /// <param name="n">Имя.</param>
        /// <returns>Выражение.</returns>
        static ParameterExpression Var<T>(string n)
        {
            return Expression.Variable(typeof(T), n);
        }

        /// <summary>
        /// Выражение - аргумент.
        /// </summary>
        /// <typeparam name="T">Тип.</typeparam>
        /// <param name="n">Имя.</param>
        /// <returns>Выражение.</returns>
        static ParameterExpression Param<T>(string n)
        {
            return Expression.Parameter(typeof(T), n);
        }

        /// <summary>
        /// Вызов функции LpNode.Take.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="len">Сколько символов взять.</param>
        /// <returns>Выражение.</returns>
        static Expression LpNode_Take(Expression text, Expression len)
        {
            // return Expression.Call(typeof(LpNode).GetMethod("Take", new Type[] { typeof(LpText), typeof(int) }), text, len);
            return Expression.New(_lpNodeTextIntStringConstructorInfo, text, len, Expression.Constant(null, typeof(string)));
        }

        /// <summary>
        /// Вызов функции LpNode.Fail.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <returns>Выражение.</returns>
        static Expression LpNode_Fail(Expression text)
        {
            // return Expression.Call(typeof(LpNode).GetMethod("Fail", new Type[] { typeof(LpText) }), text);
            return Expression.New(_lpNodeTextConstructorInfo, text);
        }

        /// <summary>
        /// Доступ к свойству LpText.Length.
        /// </summary>
        /// <param name="text">Переменная.</param>
        /// <returns>Доступ к свойству.</returns>
        static Expression LpText_Length(Expression text)
        {
            return Expression.PropertyOrField(text, "Length");
        }

        /// <summary>
        /// Доступ к свойству LpText.Source.
        /// </summary>
        /// <param name="text">Переменная.</param>
        /// <returns>Доступ к свойству.</returns>
        static Expression LpText_Source(Expression text)
        {
            return Expression.PropertyOrField(text, "Source");
        }

        /// <summary>
        /// Доступ к свойству LpText.Index.
        /// </summary>
        /// <param name="text">Переменная.</param>
        /// <returns>Доступ к свойству.</returns>
        static Expression LpText_Index(Expression text)
        {
            return Expression.PropertyOrField(text, "Index");
        }


        /// <summary>
        /// Разворачивает выражение, делая его копию и заменяя аргумент.
        /// </summary>
        /// <param name="predicate">Выражение.</param>
        /// <param name="arg">Аргумент.</param>
        /// <returns>Тело выражения.</returns>
        static Expression ExtractBody(Expression<Func<char, bool>> predicate, ParameterExpression arg)
        {
            var rep = new LambdaParameterReplacer();
            return rep.ReplaceArgs(predicate, predicate.Parameters[0], arg).Body;
        }

        /// <summary>
        /// Класс для копирования лямбда выражений.
        /// </summary>
        class LambdaParameterReplacer : ExpressionVisitor
        {
            IEnumerable<ParameterExpression> m_lambdaArgsToSearch = new List<ParameterExpression>();
            IEnumerable<ParameterExpression> m_lambdaArgsToReplace = new List<ParameterExpression>();


            /// <summary>
            /// Копирует выражение, подставляя вместо одних парамертов другие.
            /// </summary>
            /// <typeparam name="TDelegate">Делегат.</typeparam>
            /// <param name="lambda">Лямбда выражение.</param>
            /// <param name="oldParam">То, что нужно найти.</param>
            /// <param name="newParam">То, чем нужно заменить.</param>
            /// <returns>Копия.</returns>
            public Expression<TDelegate> ReplaceArgs<TDelegate>(Expression<TDelegate> lambda, ParameterExpression oldParam, ParameterExpression newParam)
            {
                return ReplaceArgs(lambda, new List<ParameterExpression>() { oldParam }, new List<ParameterExpression>() { newParam });
            }

            /// <summary>
            /// Копирует выражение, подставляя вместо одних парамертов другие.
            /// Размер массивов args1 и args2 должен быть идентичным.
            /// </summary>
            /// <typeparam name="TDelegate">Делегат.</typeparam>
            /// <param name="lambda">Лямбда выражение.</param>
            /// <param name="argsToSearch">Аргументы, которые нужно найти.</param>
            /// <param name="argsToReplace">Аргументы, которыми нужно заменить.</param>
            /// <returns>Копия.</returns>
            public Expression<TDelegate> ReplaceArgs<TDelegate>(Expression<TDelegate> lambda, IEnumerable<ParameterExpression> argsToSearch, IEnumerable<ParameterExpression> argsToReplace)
            {
                m_lambdaArgsToSearch = argsToSearch;
                m_lambdaArgsToReplace = argsToReplace;
                var lambdaСopy = (Expression<TDelegate>)VisitLambda(lambda);
                return lambdaСopy;
            }

            /// <summary>
            /// Функция, которая вызывается для каждого найденного параметра в любом уровне вложенности.
            /// </summary>
            /// <param name="node">Параметр.</param>
            /// <returns>Тот же параметр или подставной.</returns>
            protected override Expression VisitParameter(ParameterExpression node)
            {
                var replacer = m_lambdaArgsToSearch.Where(t => t == node).Select((t, i) => m_lambdaArgsToReplace.Skip(i).First()).FirstOrDefault();
                if (replacer != null)
                    return replacer;
                return base.VisitParameter(node);
            }
        }

        #endregion
    }
}
