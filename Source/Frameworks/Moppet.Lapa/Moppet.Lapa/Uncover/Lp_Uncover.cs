////////////////////////////////////////////////////////////////////////////////////////////////
//
// Copyright © Yaroslavov Alexander 2010
//
// Contacts:
// Phone: +7(906)827-27-51
// Email: x-ronos@yandex.ru
//
/////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace Moppet.Lapa
{
	/// <summary>
	/// Парсеры на лямбда-выражениях. Lp - сокращение от Lambda Parsers.
	/// </summary>
	public static partial class Lp
	{
        /// <summary>
        /// Оборачивает парсер, включая дополнительную опцию uncover.
        /// Эта опция говорит о том, что результат парсера будет развёрнут, т.е. вместо самого узла будут добавлены дочерние элементы узла, если они есть.
        /// </summary>
        /// <param name="parser">parser.</param>
        /// <returns>Парсер с опцией uncover.</returns>
        public static LpUncover<LpsParser, LpNode> Uncover(this LpsParser parser)
        {
            return new LpUncover<LpsParser, LpNode>(parser, true);
        }

        /// <summary>
        /// Оборачивает парсер, включая дополнительную опцию uncover.
        /// Эта опция говорит о том, что результат парсера будет развёрнут, т.е. вместо самого узла будут добавлены дочерние элементы узла, если они есть.
        /// </summary>
        /// <param name="parser">parser.</param>
        /// <returns>Парсер с опцией uncover.</returns>
        public static LpUncover<LpsParser, LpNode> Uncover(this LpsChain parser)
        {
            return new LpUncover<LpsParser, LpNode>(parser, true);
        }

        /// <summary>
        /// Оборачивает парсер, ВЫКЛЮЧАЯ дополнительную опцию uncover.
        /// Эта опция говорит о том, что результат парсера НЕ будет развёрнут.
        /// </summary>
        /// <param name="parser">parser.</param>
        /// <returns>Парсер с опцией uncover.</returns>
        public static LpUncover<LpsParser, LpNode> Cover(this LpsParser parser)
        {
            return new LpUncover<LpsParser, LpNode>(parser, false);
        }

        /// <summary>
        /// Wraps parser turn off the option uncover. 
        /// This option indicates that the result of the parser will NOT be deployed.
        /// </summary>
        /// <param name="parser">parser.</param>
        /// <returns>Parser to uncover opciej.</returns>
        public static LpUncover<LpsParser, LpNode> Cover(this LpsChain parser)
        {
            return new LpUncover<LpsParser, LpNode>(parser.ToParser(), false);
        }

        /// <summary>
        /// Wraps the parser, including an additional option uncover. 
        /// This option indicates that the result of the parser will be deployed, 
        /// ie instead of the node will be added to the child elements of the node, if any.
        /// </summary>
        /// <param name="parser">parser.</param>
        /// <returns>Парсер с опцией uncover.</returns>
        public static LpUncover<LpmParser, IEnumerable<LpNode>> Uncover(this LpmParser parser)
        {
            return new LpUncover<LpmParser, IEnumerable<LpNode>>(parser, true);
        }

        /// <summary>
        /// Wraps parser turn off the option uncover.
        /// This option indicates that the result of the parser will be deployed, 
        /// ie instead of the node will be added to the child elements of the node, if any.
        /// </summary>
        /// <param name="parser">parser.</param>
        /// <returns>Parser to uncover opciej.</returns>
        public static LpUncover<LpmParser, IEnumerable<LpNode>> Сover(this LpmParser parser)
        {
            return new LpUncover<LpmParser, IEnumerable<LpNode>>(parser, false);
        }
    }
}
