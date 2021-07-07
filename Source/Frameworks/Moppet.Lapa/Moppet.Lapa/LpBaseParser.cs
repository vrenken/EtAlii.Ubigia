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

namespace Moppet.Lapa
{



	/// <summary>
    /// Base class. Here all that is common between LpsParser and LpmParser.
	/// </summary>
	/// <typeparam name="TDerived">Is derived from this class.</typeparam>
	public class LpParserAttrs<TDerived> //: ICloneable
		where TDerived : LpParserAttrs<TDerived>, new()
	{
		/// <summary>
		/// Truth is, if you want to wrap the node (the result returned by the parser), if it has a non-zero identifier.
		/// </summary>
		public bool WrapNode { get => _wrapNode; protected set => _wrapNode = value; }
		private bool _wrapNode;

	    /// <summary>
		/// Identifier.
		/// </summary>
		public string Identifier { get => _identifier; protected set => _identifier = value; }
	    private string _identifier;


	    /// <summary>
		/// Creates a copy of the parser with a new identifier.
		/// </summary>
		/// <param name="id">ID.</param>
		/// <returns>Clone with a new ID.</returns>
		public TDerived Id(string id)
		{
			return Id(id: id, wrap: _wrapNode);
		}

		/// <summary>
		/// Creates a copy of the parser with a new identifier.
        /// Property WrapNode regulated argument wrap.
		/// </summary>
		/// <param name="id">ID.</param>
        /// <param name="wrap">Truth is, if you want to wrap the node (the result returned by the parser), if it has a non-zero identifier.</param>
		/// <returns>Clone with a new ID.</returns>
		public TDerived Id(string id, bool wrap)
		{
			if (_identifier != null && _identifier != id)
				throw new ArgumentException("Parser has another identifier already: " + _identifier, nameof(id));

			LpParserAttrs<TDerived> copy = Copy();
			copy._identifier = id;
			copy._wrapNode = wrap;
			return (TDerived)copy;
		}

		/// <summary>
        /// Does the same thing as f-I Id, but it does not throw an exception, allowing reassign identifier.
		/// Creates a copy of the parser with a new identifier.
		/// </summary>
		/// <param name="newId">New identifier.</param>
		/// <returns>Clone with a new ID.</returns>
		public TDerived Rename(string newId)
		{
			return Rename(newId: newId, wrap: _wrapNode);
		}

		/// <summary>
        /// Does the same thing as f-I Id, but it does not throw an exception, allowing reassign identifier.
        /// Creates a copy of the parser with a new identifier.
        /// Property WrapNode regulated argument wrap.
		/// </summary>
		/// <param name="newId">New identifier.</param>
        /// <param name="wrap">Truth is, if you want to wrap the node (the result returned by the parser), if it has a non-zero identifier.</param>
		/// <returns>Clone with a new ID.</returns>
        private TDerived Rename(string newId, bool wrap)
		{
			LpParserAttrs<TDerived> copy = Copy();
			copy._identifier = newId;
			copy._wrapNode = wrap;
			return (TDerived)copy;
		}


		/// <summary>
		/// Typed object cloning.
		/// </summary>
		/// <returns>Branch.</returns>
		public virtual TDerived Copy()
		{
			var c = new TDerived
			{
				_identifier = Identifier,
				_wrapNode = WrapNode
			};
			return c;
		}

		/// <summary>
		/// Identifier ?? GetType().Name;
		/// </summary>
        /// <returns>Information about the parser.</returns>
		public override string ToString()
		{
			return Identifier ?? GetType().Name;
		}
	}

	/// <summary>
    /// Base class. Here all that is common between LpsParser and LpmParser.
    /// Pay attention to the type TDerived, so we implement typed for TDerived functionality in the base class.
	/// </summary>
	/// <typeparam name="TResult">The result of the parser.</typeparam>
	/// <typeparam name="TDerived">Is derived from this class.</typeparam>
	public class LpBaseParser<TResult, TDerived> : LpParserAttrs<TDerived>
		where TDerived : LpBaseParser<TResult, TDerived>, new()
	{
		/// <summary>
        /// Protivozatsiklivatel. In the copying is not involved and should not be.
		/// </summary>
		protected LinkedList<LpText> Stack;

		/// <summary>
        /// Parser - lambda.
        /// This property is intended for a single unusual initialization and external access.
		/// </summary>
		public Func<LpText, TResult> Parser
		{
			get => _parser;
			set
			{
				if (_parser != null) throw new ArgumentException("Property \"Parser\" already initialized.");
				_parser = value;
			}
		}
		private Func<LpText, TResult> _parser;

		/// <summary>
        /// Control option for recursion, control over a stack overflow.
        /// This is a custom property.
		/// </summary>
		public bool Recurse
		{
			get => Stack != null;
			set
			{
				if (Stack != null)
					throw new ArgumentException("The 'Recurse' property already initialized.");
				Stack = value ? new LinkedList<LpText>() : null;
			}
		}

		/// <summary>
		/// Typed object cloning.
		/// </summary>
		/// <returns>Branch.</returns>
		public override TDerived  Copy()
		{
			if (_parser == null)
			{
				if (Identifier != null)
					throw new NullReferenceException($"The parser named '{Identifier}' has not been initialized completely.");
				else
					throw new NullReferenceException("The 'Parser' property has not been initialized.");
			}
			LpBaseParser<TResult, TDerived> c = base.Copy();
			c._parser = _parser;
			return (TDerived)c;
		}
	}
}
