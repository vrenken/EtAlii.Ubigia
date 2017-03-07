// ReSharper disable All

namespace Moppet.Lapa
{
    /// <summary>
    /// Нода и её статистика при меморизации.
    /// </summary>
    public class LpMemNode
    {
        /// <summary>
        /// Нода.
        /// </summary>
        public LpNode Node;

        /// <summary>
        /// Сколько раз возвращена из словаря по запросу.
        /// </summary>
        public int Count;

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
            return string.Format("{0}: {1}", Count, Node);
        }
    }
}
