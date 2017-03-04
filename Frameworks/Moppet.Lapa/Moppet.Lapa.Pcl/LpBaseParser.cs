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
		protected bool m_wrapNode = false;

		/// <summary>
		/// Identifier.
		/// </summary>
		protected string m_identifier = null;


		/// <summary>
		/// Truth is, if you want to wrap the node (the result returned by the parser), if it has a non-zero identifier.
		/// </summary>
		public bool WrapNode => m_wrapNode;

	    /// <summary>
		/// Identifier.
		/// </summary>
		public string Identifier => m_identifier;


	    /// <summary>
		/// Creates a copy of the parser with a new identifier.
		/// </summary>
		/// <param name="id">ID.</param>
		/// <returns>Clone with a new ID.</returns>
		public TDerived Id(string id)
		{
			return Id(id: id, wrap: m_wrapNode);
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
			if (m_identifier != null && m_identifier != id)
				throw new ArgumentException("Parser has another identifier already: " + m_identifier, "id");

			LpParserAttrs<TDerived> copy = Copy();
			copy.m_identifier = id;
			copy.m_wrapNode = wrap;
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
			return Rename(newId: newId, wrap: m_wrapNode);
		}

		/// <summary>
        /// Does the same thing as f-I Id, but it does not throw an exception, allowing reassign identifier.
        /// Creates a copy of the parser with a new identifier.
        /// Property WrapNode regulated argument wrap.
		/// </summary>
		/// <param name="newId">New identifier.</param>
        /// <param name="wrap">Truth is, if you want to wrap the node (the result returned by the parser), if it has a non-zero identifier.</param>
		/// <returns>Clone with a new ID.</returns>
		public TDerived Rename(string newId, bool wrap)
		{
			LpParserAttrs<TDerived> copy = Copy();
			copy.m_identifier = newId;
			copy.m_wrapNode = wrap;
			return (TDerived)copy;
		}


		/// <summary>
		/// Typed object cloning.
		/// </summary>
		/// <returns>Branch.</returns>
		public virtual TDerived Copy()
		{
			TDerived c = new TDerived();
			c.m_identifier = Identifier;
			c.m_wrapNode   = WrapNode;
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

		/// <summary>
        /// Cloning an object.
		/// </summary>
		/// <returns>Branch.</returns>
		public object Clone()
		{
			return Copy();
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
		protected LinkedList<LpText> m_stack = null;

		/// <summary>
		/// Parser.
		/// </summary>
		protected Func<LpText, TResult> m_parser = null;

		/// <summary>
        /// Parser - lambda.
        /// This property is intended for a single isklyuchietlno initialization and external access.
		/// </summary>
		public Func<LpText, TResult> Parser
		{
			get { return m_parser; }
			set
			{
				if (m_parser != null)
					throw new ArgumentException("Property \"Parser\" already initialized.");
				m_parser = value;
			}
		}

		/// <summary>
        /// Control option for recursion, control over a stack overflow.
        /// This is a custom property.
		/// </summary>
		public bool Recurse
		{
			get { return m_stack != null; }
			set
			{
				if (m_stack != null)
					throw new ArgumentException("The 'Recurse' property already initialized.");
				m_stack = value ? new LinkedList<LpText>() : null;
			}
		}

		/// <summary>
		/// Typed object cloning.
		/// </summary>
		/// <returns>Branch.</returns>
		public override TDerived  Copy()
		{
			if (m_parser == null)
			{
				if (Identifier != null)
					throw new NullReferenceException(string.Format("The parser named '{0}' has not been initialized completely.", Identifier));
				else
					throw new NullReferenceException("The 'Parser' property has not been initialized.");
			}
			LpBaseParser<TResult, TDerived> c = base.Copy();
			c.m_parser = m_parser;
			return (TDerived)c;
		}
	}
}
