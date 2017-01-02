namespace EtAlii.Servus.Api
{
    using System;

    public class ContentType
    {
        public string Id { get { return _id; } }
        private readonly  string _id;

        public static StructureContentType Structure { get { return _structure; } }
        private static readonly StructureContentType _structure = new StructureContentType();

        public static ImageContentType Image { get { return _image; } }
        private static readonly ImageContentType _image = new ImageContentType();

        public static TimeContentType Time { get { return _time; } }
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