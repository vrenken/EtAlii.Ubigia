namespace EtAlii.Ubigia.Api.Logical
{
    using System.IO;

    public class ContentPartQuery
    {
        public readonly Stream Stream;
        public readonly Identifier Identifier;
        public readonly IReadOnlyContent Content;

        public ContentPartQuery(Stream stream, Identifier identifier, IReadOnlyContent content)
        {
            Stream = stream;
            Identifier = identifier;
            Content = content;
        }
    }
}
