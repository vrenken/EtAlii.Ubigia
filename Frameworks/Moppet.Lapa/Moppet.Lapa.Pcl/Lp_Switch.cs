using System;
using System.Collections.Generic;

namespace Moppet.Lapa
{
    /// <summary>
    /// Parsers for lambda expressions. Lp - short for Lambda Parsers.
    /// </summary>
    public static partial class Lp
    {
        /// <summary>
        /// Choosing a variety of options parsing text input character.
        /// </summary>
        /// <param name="parser">The parser that accepts a symbol and an incoming text beginning with that letter.</param>
        /// <returns>parser.</returns>
        public static LpsParser Switch(Func<char, LpText, LpNode> parser)
        {
            return new LpsParser(null, (t) => // The ID is not needed, it suppresses subsidiaries
            {
                if (t.Length <= 0)
                    return new LpNode(t);
                char c = t[0];
                return parser(t[0], t);
            });
        }

        /// <summary>
        /// Choosing a variety of options.
        /// This implementation is aligned with the parser behindParser, which provides a more optimized design analogue (behindParser + nextParser (...)).
        /// If behindParser performed, then further performed nextParser and success of both parsers results analysis is expanded (uncover) and placed in a single node.
        /// </summary>
        /// <param name="behindParser">The parser, which is concatenated like this (behindParser + nextParser (...)). The results of analysis revealed (uncover) and placed in a single node.</param>
        /// <param name="nextParser">The parser that can change behavior based on the input character.</param>
        /// <param name="ifFurtherNothing">The parser that is used, if after behindParser nothing.</param>
        /// <returns>parser.</returns>
        public static LpsParser Switch(this LpsParser behindParser, Func<char, LpText, LpNode> nextParser, LpsParser ifFurtherNothing = null)
        {
            if (ifFurtherNothing != null) return new LpsParser(null, (t) =>
            {
                var prev = behindParser.Do(t);
                if (prev.Match.Length < 0)
                    return prev;
                var rest = prev.Rest;
                var next = rest.Length <= 0  ? ifFurtherNothing.Do(t)  :  nextParser(rest[0], rest);
                if (next.Match.Length < 0)
                    return new LpNode(t);

                var child = new List<LpNode>(0x10);
                child.AddChildrenOrNodeOrNothing(prev);
                child.AddChildrenOrNodeOrNothing(next);
                if (child.Count <= 1)
                    return child.Count == 0 ? next : child[0];
                return new LpNode(t, next.Rest.Index - t.Index, null, child);
            });
            
            return new LpsParser(null, (t) =>
            {
                var prev = behindParser.Do(t);
                if (prev.Match.Length < 0)
                    return prev;
                var rest = prev.Rest;
                if (rest.Length <= 0)
                    return new LpNode(t);

                var next = nextParser(rest[0], rest);
                if (next.Match.Length < 0)
                    return new LpNode(t);

                var child = new List<LpNode>(0x10);
                child.AddChildrenOrNodeOrNothing(prev);
                child.AddChildrenOrNodeOrNothing(next);
                if (child.Count <= 1)
                    return child.Count == 0 ? next : child[0];
                return new LpNode(t, next.Rest.Index - t.Index, null, child);
            });
        }

        ///// <summary>
        ///// Выбор множества вариантов. Если ни один из вариантов не подходит, парсер возвращает неудачное соответствие.
        ///// </summary>
        ///// <param name="actions">Варианты.</param>
        ///// <returns>parser.</returns>
        //[Obsolete]
        //public static LpsParser Switch(params SwAction[] actions)
        //{
        //    // Вариант по умолчанию.
        //    var defaultCase = actions
        //        .Where(a => a is SwDefault)
        //        .Select(a => a.Action)
        //        .SingleOrDefault();

        //    // Выбираем SwCase и уходим от побочных эффектов с помощью копий.
        //    SwCase<char>[] charCases = actions
        //        .Where(c => c is SwCase<char>)
        //        .Select(c => (SwCase<char>)c.Copy())
        //        .ToArray();

        //    if (charCases.Length != 0)
        //        return Switch(charCases, defaultCase);

        //    // Выбираем SwCase и уходим от побочных эффектов с помощью копий.
        //    SwCase<Func<char, bool>>[] funcCases = actions
        //        .Where(c => c is SwCase<Func<char, bool>>)
        //        .Select(c => (SwCase<Func<char, bool>>)c.Copy())
        //        .ToArray();

        //    if (funcCases.Length != 0)
        //        return Switch(funcCases, defaultCase);

        //    throw new ArgumentException("There is no SwCase objects from args to choose right constructor of LpParser.");
        //}

        ///// <summary>
        ///// Выбор множества вариантов. Если ни один из вариантов не подходит, за behind применяется парсер типа SwDefault.
        ///// Эта реализация совмещена с парсером behind, что обеспечивает более оптимизированный аналог конструкции (behind + Switch(...)).
        ///// Если behind выполняется, тогда далее выполняется Switch и при успешности обоих парсеров результаты разбора раскрыватся (uncover) и помещаются в один узел.
        ///// </summary>
        ///// <param name="behind">Парсер, который конкатенируется вот так (behind + Switch(...)). Результаты разбора раскрываются (uncover) и помещаются в один узел.</param>
        ///// <param name="actions">Варианты. Предусмотрены SwDefault и SwCase.</param>
        ///// <returns>parser.</returns>
        //[Obsolete]
        //public static LpsParser Switch(this LpsParser behind, params SwAction[] actions)
        //{
        //    // Вариант по умолчанию.
        //    var defaultCase = actions
        //        .Where(a => a is SwDefault)
        //        .Select(a => a.Action)
        //        .SingleOrDefault();

        //    // Выбираем SwCase и уходим от побочных эффектов с помощью копий.
        //    SwCase<char>[] charCases = actions
        //        .Where(c => c is SwCase<char>)
        //        .Select(c => (SwCase<char>)c.Copy())
        //        .ToArray();

        //    if (charCases.Length > 0)
        //        return Switch(behind, charCases, defaultCase);

        //    // Выбираем SwCase и уходим от побочных эффектов с помощью копий.
        //    SwCase<Func<char, bool>>[] funcCases = actions
        //        .Where(c => c is SwCase<Func<char, bool>>)
        //        .Select(c => (SwCase<Func<char, bool>>)c.Copy())
        //        .ToArray();

        //    if (funcCases.Length > 0)
        //        return Switch(behind, funcCases, defaultCase);

        //    throw new ArgumentException("There is no SwCase objects from args to choose right constructor of LpParser.");
        //}

        ///// <summary>
        ///// Выбор множества вариантов. Если ни один из вариантов не подходит, парсер возвращает неудачное соответствие.
        ///// </summary>
        ///// <param name="cases">Варианты.</param>
        ///// <param name="defaultCase">Вариант по умолчанию.</param>
        ///// <returns>parser.</returns>
        //static LpsParser Switch(SwCase<char>[] cases, LpsParser defaultCase)
        //{
        //    //var swExpr = LpLex.Switch(cases, defaultCase);
        //    //return swExpr.Compile();

        //    int nCases = cases.Length;

        //    return new LpsParser(null, (t) => // Идентификатор не нужен, он подавляет дочерние
        //    {
        //        if (t.Length <= 0)
        //            return new LpNode(t);

        //        char c = t[0];
        //        for (int i = 0; i < nCases; ++i)
        //        {
        //            var condition = cases[i];
        //            if (condition.Condition == c)
        //                return condition.Action.Do(t);
        //        }
        //        return defaultCase != null ? defaultCase.Do(t) : new LpNode(t);
        //    });
        //}

        ///// <summary>
        ///// Выбор множества вариантов. Если ни один из вариантов не подходит, парсер возвращает неудачное соответствие.
        ///// </summary>
        ///// <param name="cases">Действия.</param>
        ///// <param name="defaultCase">Вариант по умолчанию.</param>
        ///// <returns>parser.</returns>
        //static LpsParser Switch(SwCase<Func<char, bool>>[] cases, LpsParser defaultCase)
        //{
        //    if (cases.Length == 1)
        //    {
        //        var c0 = cases[0].Condition;
        //        var a0 = cases[0].Action;

        //        return new LpsParser(null, (t) =>
        //        {
        //            if (t.Length <= 0)
        //                return new LpNode(t);
        //            char c = t[0];
        //            if (c0(c))
        //                return a0.Do(t);
        //            return defaultCase != null ? defaultCase.Do(t) : new LpNode(t);
        //        });
        //    }
        //    else if (cases.Length == 2)
        //    {
        //        var c0 = cases[0].Condition; var a0 = cases[0].Action;
        //        var c1 = cases[1].Condition; var a1 = cases[1].Action;

        //        return new LpsParser(null, (t) =>
        //        {
        //            if (t.Length <= 0)
        //                return new LpNode(t);
        //            char c = t[0];
        //            if (c0(c)) return a0.Do(t);
        //            else if (c1(c)) return a1.Do(t);
        //            return defaultCase != null ? defaultCase.Do(t) : new LpNode(t);
        //        });
        //    }
        //    else if (cases.Length == 3)
        //    {
        //        var c0 = cases[0].Condition; var a0 = cases[0].Action;
        //        var c1 = cases[1].Condition; var a1 = cases[1].Action;
        //        var c2 = cases[2].Condition; var a2 = cases[2].Action;

        //        return new LpsParser(null, (t) =>
        //        {
        //            if (t.Length <= 0)
        //                return new LpNode(t);
        //            char c = t[0];
        //            if (c0(c)) return a0.Do(t);
        //            else if (c1(c)) return a1.Do(t);
        //            else if (c2(c)) return a2.Do(t);
        //            return defaultCase != null ? defaultCase.Do(t) : new LpNode(t);
        //        });
        //    }
        //    else if (cases.Length == 4)
        //    {
        //        var c0 = cases[0].Condition; var a0 = cases[0].Action;
        //        var c1 = cases[1].Condition; var a1 = cases[1].Action;
        //        var c2 = cases[2].Condition; var a2 = cases[2].Action;
        //        var c3 = cases[3].Condition; var a3 = cases[3].Action;

        //        return new LpsParser(null, (t) =>
        //        {
        //            if (t.Length <= 0)
        //                return new LpNode(t);
        //            char c = t[0];
        //            if (c0(c)) return a0.Do(t);
        //            else if (c1(c)) return a1.Do(t);
        //            else if (c2(c)) return a2.Do(t);
        //            else if (c3(c)) return a3.Do(t);
        //            return defaultCase != null ? defaultCase.Do(t) : new LpNode(t);
        //        });
        //    }
        //    else if (cases.Length == 5)
        //    {
        //        var c0 = cases[0].Condition; var a0 = cases[0].Action;
        //        var c1 = cases[1].Condition; var a1 = cases[1].Action;
        //        var c2 = cases[2].Condition; var a2 = cases[2].Action;
        //        var c3 = cases[3].Condition; var a3 = cases[3].Action;
        //        var c4 = cases[4].Condition; var a4 = cases[4].Action;

        //        return new LpsParser(null, (t) =>
        //        {
        //            if (t.Length <= 0)
        //                return new LpNode(t);
        //            char c = t[0];
        //            if (c0(c)) return a0.Do(t);
        //            else if (c1(c)) return a1.Do(t);
        //            else if (c2(c)) return a2.Do(t);
        //            else if (c3(c)) return a3.Do(t);
        //            else if (c4(c)) return a4.Do(t);
        //            return defaultCase != null ? defaultCase.Do(t) : new LpNode(t);
        //        });
        //    }

        //    // Для всех прочих случаев
        //    int nCases = cases.Length;
        //    return new LpsParser(null, (t) =>
        //    {
        //        if (t.Length <= 0)
        //            return new LpNode(t);

        //        char c = t[0];
        //        for (int i = 0; i < nCases; ++i)
        //        {
        //            var condition = cases[i];
        //            if (condition.Condition(c))
        //                return condition.Action.Do(t);
        //        }
        //        return defaultCase != null ? defaultCase.Do(t) : new LpNode(t);
        //    });
        //}

        ///// <summary>
        ///// Выбор множества вариантов. Если ни один из вариантов не подходит, за behind применяется парсер defaultCase.
        ///// Эта реализация совмещена с парсером behind, что обеспечивает более оптимизированный аналог конструкции (behind + Switch(...)).
        ///// Если behind выполняется, тогда далее выполняется Switch и при успешности обоих парсеров результаты разбора раскрыватся (uncover) и помещаются в один узел.
        ///// </summary>
        ///// <param name="behind">Парсер, который конкатенируется вот так (behind + Switch(...)). Результаты разбора раскрываются (uncover) и помещаются в один узел.</param>
        ///// <param name="cases">Варианты.</param>
        ///// <param name="defaultCase">Вариант по умолчанию.</param>
        ///// <returns>parser.</returns>
        //static LpsParser Switch(LpsParser behind, SwCase<char>[] cases, LpsParser defaultCase)
        //{
        //    int nCases = cases.Length;

        //    // ============================================================== defaultCase == null
        //    if (defaultCase == null) return new LpsParser(null, (t) =>
        //    {
        //        var prev = behind.Do(t);
        //        if (!prev.Success)
        //            return prev;
        //        var rest = prev.Rest;
        //        if (rest.Length <= 0)
        //            return new LpNode(t);

        //        char c = rest[0];
        //        for (int i = 0; i < nCases; ++i)
        //        {
        //            var condition = cases[i];
        //            if (condition.Condition == c)
        //            {
        //                var next = condition.Action.Do(rest);
        //                if (!next.Success)
        //                    return new LpNode(t);
        //                var child = new List<LpNode>(0x10);
        //                child.AddChildrenOrNodeOrNothing(prev);
        //                child.AddChildrenOrNodeOrNothing(next);
        //                if (child.Count <= 1)
        //                    return child.Count == 0 ? next : child[0];
        //                return new LpNode(t, next.Rest.Index - t.Index, null, child);
        //            }
        //        }
        //        return new LpNode(t);
        //    });


        //    // ============================================================== defaultCase != null
        //    return new LpsParser(null, (t) =>
        //    {
        //        LpNode prev = behind.Do(t);
        //        if (!prev.Success)
        //            return prev;
        //        LpText rest = prev.Rest;
        //        LpsParser than = defaultCase;
        //        if (rest.Length > 0)
        //        {
        //            char c = rest[0];
        //            for (int i = 0; i < nCases; ++i)
        //            {
        //                var condition = cases[i];
        //                if (condition.Condition == c)
        //                {
        //                    than = condition.Action;
        //                    break;
        //                }
        //            }
        //        }
        //        var next = than.Do(rest);
        //        if (!next.Success)
        //            return new LpNode(t);
        //        var child = new List<LpNode>(0x10);
        //        child.AddChildrenOrNodeOrNothing(prev);
        //        child.AddChildrenOrNodeOrNothing(next);
        //        if (child.Count <= 1)
        //            return child.Count == 0 ? next : child[0];
        //        return new LpNode(t, next.Rest.Index - t.Index, null, child);
        //    });
        //}

        ///// <summary>
        ///// Выбор множества вариантов. Если ни один из вариантов не подходит, за behind применяется парсер defaultCase.
        ///// Эта реализация совмещена с парсером behind, что обеспечивает более оптимизированный аналог конструкции (behind + Switch(...)).
        ///// Если behind выполняется, тогда далее выполняется Switch и при успешности обоих парсеров результаты разбора раскрыватся (uncover) и помещаются в один узел.
        ///// </summary>
        ///// <param name="behind">Парсер, который конкатенируется вот так (behind + Switch(...)). Результаты разбора раскрываются (uncover) и помещаются в один узел.</param>
        ///// <param name="cases">Варианты.</param>
        ///// <param name="defaultCase">Вариант по умолчанию.</param>
        ///// <returns>parser.</returns>
        //static LpsParser Switch(LpsParser behind, SwCase<Func<char, bool>>[] cases, LpsParser defaultCase)
        //{
        //    int nCases = cases.Length;

        //    // ================================================================================== defaultCase == null
        //    if (defaultCase == null) return new LpsParser(null, (t) =>
        //    {
        //        var prev = behind.Do(t);
        //        if (!prev.Success)
        //            return prev;
        //        var rest = prev.Rest;
        //        if (rest.Length <= 0)
        //            return new LpNode(t);

        //        char c = rest[0];
        //        for (int i = 0; i < nCases; ++i)
        //        {
        //            var condition = cases[i];
        //            if (condition.Condition(c))
        //            {
        //                var next = condition.Action.Do(rest);
        //                if (!next.Success)
        //                    return new LpNode(t);
        //                var child = new List<LpNode>(0x10);
        //                child.AddChildrenOrNodeOrNothing(prev);
        //                child.AddChildrenOrNodeOrNothing(next);
        //                if (child.Count <= 1)
        //                    return child.Count == 0 ? next : child[0];
        //                return new LpNode(t, next.Rest.Index - t.Index, null, child);
        //            }
        //        }
        //        return new LpNode(t);
        //    });

        //    // ================================================================================== defaultCase != null
        //    return new LpsParser(null, (t) =>
        //    {
        //        LpNode prev = behind.Do(t);
        //        if (!prev.Success)
        //            return prev;
        //        LpText rest = prev.Rest;
        //        LpsParser than = defaultCase;
        //        if (rest.Length > 0)
        //        {
        //            char c = rest[0];
        //            for (int i = 0; i < nCases; ++i)
        //            {
        //                var condition = cases[i];
        //                if (condition.Condition(c))
        //                {
        //                    than = condition.Action;
        //                    break;
        //                }
        //            }
        //        }
        //        var next = than.Do(rest);
        //        if (!next.Success)
        //            return new LpNode(t);
        //        var child = new List<LpNode>(0x10);
        //        child.AddChildrenOrNodeOrNothing(prev);
        //        child.AddChildrenOrNodeOrNothing(next);
        //        if (child.Count <= 1)
        //            return child.Count == 0 ? next : child[0];
        //        return new LpNode(t, next.Rest.Index - t.Index, null, child);
        //    });
        //}

        //#region Helper structs

        ///// <summary>
        ///// Синтаксически более удобный вариант создания SwDefault&lt;T&gt;
        ///// </summary>
        ///// <param name="parser">Парсер, который будет применён, если все прочие варианты ложны.</param>
        ///// <returns>Структура, описывающая случай.</returns>
        //public static SwDefault Default(LpsParser parser)
        //{
        //    return new SwDefault(parser);
        //}

        ///// <summary>
        ///// Синтаксически более удобный вариант создания SwCase&lt;T&gt;
        ///// </summary>
        ///// <param name="ch">Предикат - функция принадлежности символа заданному множеству.</param>
        ///// <param name="parser">Парсер, который будет применён, если предикат вернён истину.</param>
        ///// <returns>Структура, описывающая случай.</returns>
        //public static SwCase<Func<char, bool>> Case(Func<char, bool> ch, LpsParser parser)
        //{
        //    return new SwCase<Func<char, bool>>(ch, parser);
        //}

        ///// <summary>
        ///// Синтаксически более удобный вариант создания SwCase&lt;T&gt;
        ///// </summary>
        ///// <param name="ch">symbol.</param>
        ///// <param name="parser">Парсер, который будет применён, если встретится заданный символ.</param>
        ///// <returns>Структура, описывающая случай.</returns>
        //public static SwCase<char> Case(char ch, LpsParser parser)
        //{
        //    return new SwCase<char>(ch, parser);
        //}


        ///// <summary>
        ///// Описание действия в конструкции switch.
        ///// </summary>
        //public class SwAction
        //{
        //    /// <summary>
        //    /// Вариант разбора.
        //    /// </summary>
        //    public LpsParser Action;

        //    /// <summary>
        //    /// The main constructor.
        //    /// </summary>
        //    /// <param name="action">Действие, если случай произойдёт.</param>
        //    public SwAction(LpsParser action) { Action = action; }

        //    /// <summary>
        //    /// A copy of the object.
        //    /// </summary>
        //    /// <returns>A copy of the object.</returns>
        //    public virtual SwAction Copy()
        //    {
        //        return new SwAction(Action);
        //    }
        //}

        ///// <summary>
        ///// Описание действия по умолчанию в конструкции switch.
        ///// </summary>
        //public class SwDefault : SwAction
        //{
        //    /// <summary>
        //    /// The main constructor.
        //    /// </summary>
        //    /// <param name="action">Действие, если случай произойдёт.</param>
        //    public SwDefault(LpsParser action) : base(action) { }

        //    /// <summary>
        //    /// A copy of the object.
        //    /// </summary>
        //    /// <returns>A copy of the object.</returns>
        //    public override SwAction Copy()
        //    {
        //        return new SwDefault(Action);
        //    }
        //}

        ///// <summary>
        ///// Описание случая при выборе вариантов разбора в конструкции switch.
        ///// </summary>
        ///// <typeparam name="T">Тип.</typeparam>
        //public class SwCase<T> : SwAction
        //{
        //    /// <summary>
        //    /// Случай.
        //    /// </summary>
        //    public T Condition;

        //    /// <summary>
        //    /// The main constructor.
        //    /// </summary>
        //    /// <param name="condition">Случай.</param>
        //    /// <param name="action">Выбор, если случай произойдёт.</param>
        //    public SwCase(T condition, LpsParser action) : base(action) { Condition = condition; }

        //    /// <summary>
        //    /// A copy of the object.
        //    /// </summary>
        //    /// <returns>A copy of the object.</returns>
        //    public override SwAction Copy()
        //    {
        //        return new SwCase<T>(Condition, Action);
        //    }
        //}

        //#endregion  Helper structs
    }
}
