// ReSharper disable All

namespace Moppet.Lapa
{
    /// <summary>
    /// Noda and its statistics during memorialization.
    /// </summary>
    public class LpMemNode
    {
        /// <summary>
        /// Нода.
        /// </summary>
        public LpNode Node { get; init; }

        /// <summary>
        /// How many times was returned from the dictionary on request.
        /// </summary>
        public int Count { get; init; }

        /// <summary>
        /// The main constructor.
        /// </summary>
        /// <param name="n">Нода.</param>
        public LpMemNode(LpNode n)
        {
            Node = n; Count = 0;
        }

        /// <summary>
        /// Строковое представление. Главным образом для отладки.
        /// </summary>
        /// <returns>Строка. Всегда не null.</returns>
        public override string ToString()
        {
            return $"{Count}: {Node}";
        }
    }
}
