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
    /// Подцепляет к парсеру дополнительное информационное свойство Uncover.
    /// Если Uncover истина, то будет добавляться не результат парсера, представленный в виде узла, а содержимое узла, т.е. Children.
    /// </summary>
    /// <typeparam name="TParser">Тип парсера.</typeparam>
    /// <typeparam name="TResult">Тип результата парсера.</typeparam>
    public struct LpUncover<TParser, TResult> where TParser : LpBaseParser<TResult, TParser>, new()
    {
        /// <summary>
        /// The main constructor.
        /// </summary>
        /// <param name="parser">parser.</param>
        /// <param name="uncover">Истина, если результат парсера нужно раскрыть.</param>
        public LpUncover(TParser parser, bool uncover)
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
        /// Неявное преобразование парсера в LpUncover.
        /// </summary>
        /// <param name="parser">parser.</param>
        /// <returns>Объект LpUncover.</returns>
        public static implicit operator LpUncover<TParser, TResult>(TParser parser)
        {
            return new LpUncover<TParser, TResult>(parser, true);
        }
    }
}
