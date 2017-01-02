namespace EtAlii.Servus.Api.Data
{
    using System;
    using Moppet.Lapa;
    using System.Collections.Generic;

    internal class ConstantHelper : IConstantHelper
    {
        public ConstantHelper()
        {
        }

        public bool IsValidConstantCharacter(char c)
        {
            return char.IsLetterOrDigit(c) || c == '_';
        }
        public bool IsValidQuotedConstantCharacter(char c)
        {
            return IsValidConstantCharacter(c) ||
                   c == '+' || c == '-' ||
                   c == ' ' || c == '/' ||
                   c == '$' || c == '#' ||
                   c == '&' ||
                   c == ':' || c == '@';
        }

    }
}
