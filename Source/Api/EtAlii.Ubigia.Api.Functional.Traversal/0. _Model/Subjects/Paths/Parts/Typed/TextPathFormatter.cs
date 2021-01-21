namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public static class TextPathFormatter
    {
        public static readonly TypedPathFormatter WordFormatter = new("WORD", @"^\p{L}+$");
        public static readonly TypedPathFormatter NumberFormatter = new("NUMBER", @"^[0123456789]+$");
    }
}
