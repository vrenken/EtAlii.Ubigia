namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class ConstantHelper : IConstantHelper
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
