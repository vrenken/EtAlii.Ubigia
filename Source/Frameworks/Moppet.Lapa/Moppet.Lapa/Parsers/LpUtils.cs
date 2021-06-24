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

namespace Moppet.Lapa.Parsers
{
    /// <summary>
    /// All sorts of useful general purpose parsers.
    /// </summary>
    public class LpUtils
    {
        /// <summary>
        /// Parser for web links like: http://hostname.dom:80/Dir/Dir/?a=1&amp;b=2#anhor.
        ///
        /// The returned tree is marked with the following identifiers:
        /// Protocol - protocol;
        /// Host - hostname, for example localhost, yandex.ru, 127.0.0.1;
        /// Port - port number;
        /// Path - relative path to folder or file;
        /// Query - GET-query or parameter list (a=1&amp;b=2), where name and value are tagged as Name and Value;
        /// Anchor - anchor like #name;
        /// Root - root address, for example: http://hostname.dom:80.
        /// </summary>
        /// <param name="protocols">List of valid protocols. By default, only http and https are parsed.</param>
        /// <remarks>См. также: http://tools.ietf.org/html/rfc3986 </remarks>
        /// <returns>parser.</returns>
        public static LpsParser WebUrl(params string[] protocols)
        {
            if (protocols.Length == 0)
                protocols = new[] { "https", "http" };

            var protocol = Lp.Any(protocols).Id("Protocol");
            var hostPart = Lp.Name
                (
                    firstChars: c => char.IsLetterOrDigit(c),
                    dashChar  : '-',
                    lastChars : c => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || char.IsLetterOrDigit(c) || c == '_',
                    maxLength : 63
                );
            var hostName = Lp.List(hostPart, '.').Id("Host");
            var port     = Lp.Digits(2, 4).Id("Port");
            var pathPart = Lp.OneOrMore(c => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || char.IsLetterOrDigit(c) || c == '.' || c == '-');
#pragma warning disable S1075 - URIs should not be hardcoded
            var path     = ('/' + Lp.List(pathPart, '/').Id("Path")).ToParser();
#pragma warning restore S1075
            var paramName  = Lp.OneOrMore(c => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || (c != '=' && c != '&' && c != '#' && c != '/' && c != '?' && c != '<' && c != '>' && c != '\\' && c != '\"' && c != '\'')).Id("Name");
            var paramValue = Lp.OneOrMore(c => (c >= '0' && c <= '9') || (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c != '&' && c != '#' && c != '\r' && c != '\n' && c != '<' && c != '>' && c != '\\' && c != '\"' && c != '\'' && c != '(' && c != ')')).Id("Value");

            var parameter = (paramName + '=' + paramValue | paramName).TakeFirst().Id("Param");
            var parameters = Lp.List(parameter, '&').Id("Query");

            var anchor = ('#' + hostPart.Id("Anchor")).ToParser();
            var query = ('?' + parameters).ToParser();
            var root = (protocol + "://" + hostName + (':' + port).Maybe()).ToParser().Id("Root");

            var url = (root + path.Maybe() + Lp.Maybe('/') + query.Maybe() + anchor.Maybe()).Id("Url");
            return url;
        }


        #region NamedParams

        /// <summary>
        /// Generates parser that parses named parameters of the form: "param1 = value1, param2 = value2".
        /// The parameter list may be quoted or unquoted.
        /// Marks sites: Param - setting consisting of name and value; Name - name of the parameter; Value - value.
        /// </summary>
        /// <param name="maybeLeftQuote">Possible quote left. The default is double quote.</param>
        /// <param name="maybeRightQuote">Possible quote right. The default is double quote.</param>
        /// <returns>parser.</returns>
        private static LpsParser NamedParams(char maybeLeftQuote = '"', char maybeRightQuote = '"')
        {
            var name = Lp.Name
            (
                c => (char.IsLetter(c) || c == '_') && c != maybeRightQuote && c != maybeLeftQuote,
                c => c != '=' && c != maybeRightQuote
            ).
            Id("Name");

            var value = Lp.ZeroOrMore
            (
                c => c != ',' && c != '=' && c != maybeRightQuote
            ).
            Id("Value");


            var delim = Lp.Name
            (
                c => c == ',',
                c => char.IsSeparator(c)
            ).
            Id("Delim");

            return Lp.List
            (
                (name + '=' + value).Id("Param"), delim
            ).
            MaybeTails(Lp.Char(maybeLeftQuote), Lp.Char(maybeRightQuote));
        }

        /// <summary>
        /// Generates parser that parses named parameters of the form: "param1 = value1, param2 = value2".
        /// The parameter list may be quoted or unquoted.
        /// Marks sites: Param - setting consisting of name and value; Name - name of the parameter; Value - value.
        /// </summary>
        /// <param name="nameValueConverter">Converter results. It accepts the name and the value returned by the fact that they themselves want.</param>
        /// <returns>parser.</returns>
        public static Func<LpText, IEnumerable<TResult>> NamedParams<TResult>(Func<string, string, TResult> nameValueConverter)
        {
            return NamedParams('"', '"', nameValueConverter);
        }

        /// <summary>
        /// Генерирует парсер, который разбирает именованные параметры вида: "param1=value1, param2 =value2".
        /// Список параметров может быть в кавычках или без.
        /// Пометки узлов: Param - параметр, состоящий из имени и значения; Name - имя параметра; Value - значение параметра.
        /// </summary>
        /// <param name="maybeLeftQuote">Возможная кавычка слева. По умолчанию это двойная кавычка.</param>
        /// <param name="maybeRightQuote">Возможная кавычка справа. По умолчанию это двойная кавычка.</param>
        /// <param name="nameValueConverter">Конвертер результатов. Принимает имя и значение, возвращает то, что сами захотите.</param>
        /// <returns>parser.</returns>
        private static Func<LpText, IEnumerable<TResult>> NamedParams<TResult>(char maybeLeftQuote, char maybeRightQuote, Func<string, string, TResult> nameValueConverter)
        {
            var parser = NamedParams(maybeLeftQuote, maybeRightQuote);
            return text =>
            {
                var result = parser.Do(text);
                if (!result.Success)
                    return Array.Empty<TResult>();

                return result.Children.Where(n => n.Id == "Param").Select(n =>
                {
                    var data = n.Children.ToArray();
                    return nameValueConverter(data[0].Match.ToString(), data[2].Match.ToString());
                });
            };
        }

        #endregion NamedParams
    }
}
