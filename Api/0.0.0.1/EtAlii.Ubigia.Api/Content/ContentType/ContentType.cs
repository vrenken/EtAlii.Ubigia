namespace EtAlii.Ubigia.Api
{
    using System;

    public class ContentType
    {
        public string Id => _id;
        private readonly  string _id;

        public static StructureContentType Structure => _structure;
        private static readonly StructureContentType _structure = new StructureContentType();

        public static ImageContentType Image => _image;
        private static readonly ImageContentType _image = new ImageContentType();

        public static TimeContentType Time => _time;
        private static readonly TimeContentType _time = new TimeContentType();

        internal ContentType(params string[] contentTypeParts)
        {
            _id = String.Join(@"\", contentTypeParts);
        }

        public override string ToString()
        {
            return _id;
        }
    }
}