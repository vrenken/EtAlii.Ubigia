namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal interface IConstantHelper
    {
        bool IsValidConstantCharacter(char c);
        bool IsValidQuotedConstantCharacter(char c, char quoteChar);
    }
}
