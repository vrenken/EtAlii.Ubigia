// ReSharper disable All

////////////////////////////////////////////////////////////////////////////////////////////////
//
// Copyright © Yaroslavov Alexander 2010
//
// Contacts:
// Phone: +7(906)827-27-51
// Email: x-ronos@yandex.ru
//
/////////////////////////////////////////////////////////////////////////////////////////////////

namespace Moppet.Lapa
{
    /// <summary>
    /// То же самое, что и класс LpUncover, только оператор неявного приведения устанавливает свойство Uncover в false.
    /// </summary>
    /// <typeparam name="TParser">Тип парсера.</typeparam>
    /// <typeparam name="TResult">Тип результата парсера.</typeparam>
    public struct LpCover<TParser, TResult> where TParser : LpBaseParser<TResult, TParser>, new()
    {
        /// <summary>
        /// The main constructor.
        /// </summary>
        /// <param name="parser">parser.</param>
        /// <param name="uncover">Истина, если результат парсера нужно раскрыть.</param>
        public LpCover(TParser parser, bool uncover)
        {
            Uncover = uncover; Parser = parser;
        }

        /// <summary>
        /// Parser.
        /// </summary>
        public TParser Parser { get; init; }

        /// <summary>
        /// Истина, если результат парсера нужно раскрыть.
        /// </summary>
        public bool Uncover { get; init; }

        /// <summary>
        /// Неявное преобразование LpUncover в LpCover.
        /// </summary>
        /// <param name="uncover">Parser.</param>
        /// <returns>Объект LpCover.</returns>
        public static implicit operator LpCover<TParser, TResult>(LpUncover<TParser, TResult> uncover)
        {
            return new LpCover<TParser, TResult>(uncover.Parser, uncover.Uncover);
        }

        /// <summary>
        /// Неявное преобразование парсера в LpСover.
        /// </summary>
        /// <param name="parser">parser.</param>
        /// <returns>Объект LpCover.</returns>
        public static implicit operator LpCover<TParser, TResult>(TParser parser)
        {
            return new LpCover<TParser, TResult>(parser, false);
        }
    }
}
