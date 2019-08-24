namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using Moppet.Lapa;

    /// <summary>
    /// This is the basic interface of all Moppet.Lapa parsers.
    /// </summary>
    internal interface IParser
    {
        /// <summary>
        /// The Id that can be used to find LpNode instances for the given LpsParser. 
        /// </summary>
        string Id { get; }

        /// <summary>
        /// The LpsParser used to parse text for instances.
        /// </summary>
        LpsParser Parser { get; }
    }

    /// <summary>
    /// This is the basic interface of all Moppet.Lapa parsers.
    /// </summary>
    internal interface IParser<TItem, in TOtherItem> : IParser
    {
        /// <summary>
        /// Parse the given node and return an item instance that matches it.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        TItem Parse(LpNode node);
        
        /// <summary>
        /// Return true when the node can be parsed by the parser.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        bool CanParse(LpNode node);
        
        /// <summary>
        /// Validate the item after it has been parsed from a node.  
        /// </summary>
        /// <param name="before"></param>
        /// <param name="item"></param>
        /// <param name="itemIndex"></param>
        /// <param name="after"></param>
        void Validate(TOtherItem before, TItem item, int itemIndex, TOtherItem after);
        
        /// <summary>
        /// Return true when the instance can be validated by the parser.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool CanValidate(TItem item);

    }
}