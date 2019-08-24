namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    internal interface IConstantHelper
    {
        bool IsValidConstantCharacter(char c);
        bool IsValidQuotedConstantCharacter(char c, char quoteChar);
    }
}
