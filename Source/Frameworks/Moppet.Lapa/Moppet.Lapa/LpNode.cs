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

using System;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable ArrangeStaticMemberQualifier

namespace Moppet.Lapa
{
	/// <summary>
    /// Element (node) of the parse tree.
	/// </summary>
    public class LpNode // Do not make this node structure, and the rate of fall.
	{
		/// <summary>
        /// Match. This is what the parser found.
        /// Block of text that has been analyzed (dismantled).
        /// </summary>
		public LpText Match { get; init; }

		/// <summary>
        /// Remainder of the text. This is something that remains to examine (analyze further).
		/// </summary>
		public LpText Rest { get; init; }

		/// <summary>
        /// Child blocks of text consisting Match.
		/// </summary>
		public IEnumerable<LpNode> Children { get; init; }

		/// <summary>
        /// Node ID.
		/// </summary>
		public string Id { get; set; }

        /// <summary>
        /// Constructs a node that represents a bad match.
        /// Ie unsuccessful attempt to parse.
        /// </summary>
        /// <param name="id">node identifier.</param>
        /// <param name="rest">remainder of the text.</param>
        public LpNode(string id, LpText rest)
        {
            Match = new LpText(rest.Source, rest.Index, -1);
            Rest = rest;
        }

        /// <summary>
        /// Unsuccessful match, an unsuccessful attempt to parse.
        /// </summary>
        /// <param name="rest">remainder of the text.</param>
        public LpNode(LpText rest)
        {
            Match = new LpText(rest.Source, rest.Index, -1);
            Rest = rest;
        }

        #region Take

        /// <summary>
        /// Designer to generate the result of a given length.
        /// </summary>
        /// <param name="text">source.</param>
        /// <param name="nTake">How many characters must take.</param>
        /// <param name="id">node identifier.</param>
        /// <param name="children">subsites.</param>
        public LpNode(LpText text, int nTake, string id, IEnumerable<LpNode> children)
        {
            Id = id;
            Children = children;
            Match = new LpText(text.Source, text.Index, nTake);
            Rest = new LpText(text.Source, text.Index + nTake, text.Length - nTake);
        }

        /// <summary>
        /// Designer to generate the result of a given length.
        /// </summary>
        /// <param name="text">source.</param>
        /// <param name="nTake">How many characters must take.</param>
        /// <param name="id">node identifier.</param>
        /// <param name="children">Дочерние узлы.</param>
        public LpNode(LpText text, int nTake, string id, params LpNode[] children)
        {
            Id       = id;
            Children = children;
            Match    = new LpText(text.Source, text.Index, nTake);
            Rest     = new LpText(text.Source, text.Index + nTake, text.Length - nTake);
        }

        /// <summary>
        /// Designer to generate the result of a given length.
        /// </summary>
        /// <param name="text">source.</param>
        /// <param name="nTake">How many characters must take.</param>
        /// <param name="id">node identifier.</param>
        public LpNode(LpText text, int nTake, string id = null)
        {
            Id = id;
            Match = new LpText(text.Source, text.Index, nTake);
            Rest = new LpText(text.Source, text.Index + nTake, text.Length - nTake);
        }

        #endregion Take

        /// <summary>
        /// The main constructor.
		/// </summary>
		/// <param name="match">Compliance (block parsed text).</param>
		/// <param name="rest">The remaining text.</param>
		/// <param name="children">Subsidiaries blocks of text that make up match.</param>
		public LpNode(LpText match, LpText rest, params LpNode[] children)
		{
			Match = match; Rest = rest; Children = children;
		}

        /// <summary>
		/// The main constructor.
		/// </summary>
		/// <param name="id">node identifier.</param>
		/// <param name="match">Compliance (block parsed text).</param>
		/// <param name="rest">The remaining text.</param>
		/// <param name="children">Subsidiaries blocks of text that make up match.</param>
		public LpNode(string id, LpText match, LpText rest, params LpNode[] children)
		{
			Id = id; Match = match; Rest = rest; Children = children;
		}

		/// <summary>
        /// True if the node is a successful matching the text.
		/// </summary>
		public bool Success => Match.Length >= 0;

	    /// <summary>
        /// Returns a block of text that represents the node.
        /// If non-empty block of text, then the text is considered to be parsed (analyzed).
        /// </summary>
		/// <returns>The string is not always null.</returns>
		public override string ToString() { return Match.ToString(); }

		/// <summary>
		/// Combines the previous and the following result in one.
		/// </summary>
		/// <param name="prev">Previous Match.</param>
		/// <param name="next">Next Match.</param>
		/// <returns>an object of class.</returns>
		public static LpNode Concat(LpNode prev, LpNode next)
		{
			return new LpNode(new LpText(prev.Match.Source, prev.Match.Index, next.Rest.Index - prev.Match.Index), next.Rest, prev, next);
		}

		#region Node helpers

		/// <summary>
        /// Selects all nodes with the specified identifier.
        /// Nodes are selected not only from a list of nodes, but for all children.
        /// </summary>
        /// <param name="nodes">nodes.</param>
		/// <param name="predicate">F-I search for an ID.</param>
		/// <param name="goIntoTheDeep">If the node does not satisfy the predicate and it has child elements, then this is called lambda resolution dfs.</param>
		/// <returns>sample.</returns>
		public static IEnumerable<LpNode> Select(IEnumerable<LpNode> nodes, Func<LpNode, bool> predicate, Func<LpNode, bool> goIntoTheDeep)
		{
			foreach (var cn in nodes)
			{
				var find = predicate(cn);
				if (find)
					yield return cn;

				if (!find && cn.Match.Length > 0 && cn.Children != null && goIntoTheDeep(cn))
				{
					foreach (var cnn in Select(cn.Children, predicate, goIntoTheDeep))
						yield return cnn;
				}
			}
		}

		/// <summary>
		/// Selects the first node with the specified identifier.
		/// </summary>
        /// <param name="predicate">F-I search the site.</param>
		/// <returns>node or null.</returns>
		public LpNode FirstOrDefault(Func<LpNode, bool> predicate)
		{
			return LpNode.Select(new[] { this }, predicate, no => true).FirstOrDefault();
		}

		/// <summary>
		/// Selects the first node with the specified identifier.
		/// </summary>
		/// <param name="id">ID.</param>
		/// <param name="goIntoTheDeep">Truth is, if you need to look deeper into.</param>
		/// <returns>node or null.</returns>
		public LpNode FirstOrDefault(string id, bool goIntoTheDeep = true)
		{
			return LpNode.Select(new[] { this }, n => n.Id == id, no => goIntoTheDeep).FirstOrDefault();
		}

		#endregion // Node helpers
	}


}
