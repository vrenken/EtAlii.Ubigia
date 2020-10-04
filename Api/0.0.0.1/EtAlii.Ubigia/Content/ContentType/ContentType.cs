namespace EtAlii.Ubigia
{
    using System;

    public class ContentType
    {
        public string Id { get; }

        public static StructureContentType Structure { get; } = new StructureContentType();

        public static ImageContentType Image { get; } = new ImageContentType();

        public static TimeContentType Time { get; } = new TimeContentType();

        internal ContentType(params string[] contentTypeParts)
        {
            Id = String.Join(@"\", contentTypeParts);
        }

        public override string ToString()
        {
            return Id;
        }
    }
}