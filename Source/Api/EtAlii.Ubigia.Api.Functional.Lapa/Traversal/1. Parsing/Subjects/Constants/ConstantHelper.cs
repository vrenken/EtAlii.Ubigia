// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal sealed class ConstantHelper : IConstantHelper
    {
        public bool IsValidConstantCharacter(char c)
        {
            return char.IsLetterOrDigit(c) || c == '_';
        }
        public bool IsValidQuotedConstantCharacter(char c, char quoteChar)
        {
            return c != quoteChar;

            //return IsValidConstantCharacter(c) ||
            //       c = = '+' || c = = '-' ||
            //       c = = ' ' || c = = '|' ||
            //       c eq $ or c eq £ or c eq € or
            //       c = = '&' || c = = '#' ||
            //       c = = ':' || c = = '@' ||
            //       c = = '.' || c = = ',' ||
            //       c = = '(' || c = = ')' ||
            //       c = = '[' || c = = ']' ||
            //       c = = '?' || c = = '!' ||
            //       c = = '/' || c = = '\\' ||
            //       c = = '©' || c = = '®' ||
            //       c = = '~' || c = = '^' ||
            //       c = = '[' || c = = ']' ||
            //       c = = '<' || c = = '>'
        }

    }
}
