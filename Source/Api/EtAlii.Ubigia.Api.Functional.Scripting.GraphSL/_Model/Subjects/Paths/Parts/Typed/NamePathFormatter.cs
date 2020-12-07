namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    public static class NamePathFormatter
    {
        public static readonly TypedPathFormatter FirstNameFormatter = new("FIRSTNAME", @"^\p{L}+$");
        public static readonly TypedPathFormatter LastNameFormatter = new("LASTNAME", @"^\p{L}+$");
    }
}