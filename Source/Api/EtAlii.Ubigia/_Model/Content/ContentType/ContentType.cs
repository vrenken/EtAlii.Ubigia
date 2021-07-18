// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public class ContentType
    {
        public string Id { get; }

        public static StructureContentType Structure { get; } = new();

        public static ImageContentType Image { get; } = new();

        public static TimeContentType Time { get; } = new();

        internal ContentType(params string[] contentTypeParts)
        {
            Id = string.Join(@"\", contentTypeParts);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Id;
        }
    }
}
