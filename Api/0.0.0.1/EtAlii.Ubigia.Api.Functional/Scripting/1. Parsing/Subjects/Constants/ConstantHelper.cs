namespace EtAlii.Ubigia.Api.Functional
{
    internal class ConstantHelper : IConstantHelper
    {
        public ConstantHelper()
        {
        }

        public bool IsValidConstantCharacter(char c)
        {
            return char.IsLetterOrDigit(c) || c == '_';
        }
        public bool IsValidQuotedConstantCharacter(char c, char quoteChar)
        {
            return c != quoteChar;

            //return IsValidConstantCharacter(c) ||
            //       c == '+' || c == '-' ||
            //       c == ' ' || c == '|' ||
            //       c == '$' || c == '£' || c == '€' || 
            //       c == '&' || c == '#' ||
            //       c == ':' || c == '@' ||
            //       c == '.' || c == ',' ||
            //       c == '(' || c == ')' ||
            //       c == '[' || c == ']' ||
            //       c == '?' || c == '!' ||
            //       c == '/' || c == '\\' ||
            //       c == '©' || c == '®' ||
            //       c == '~' || c == '^' ||
            //       c == '{' || c == '}' ||
            //       c == '<' || c == '>';
        }

    }
}
