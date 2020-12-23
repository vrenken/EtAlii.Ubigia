namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal interface ISequenceParser : IParser
    {
        /// <summary>
        /// Parse a LpNode for a Sequence.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="restIsAllowed"></param>
        /// <returns></returns>
        Sequence Parse(LpNode node, bool restIsAllowed);

        /// <summary>
        /// Parse a string for a Sequence.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        Sequence Parse(string text);
    }
}
