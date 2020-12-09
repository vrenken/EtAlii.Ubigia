﻿namespace EtAlii.Ubigia.Api.Logical
{
    using System.IO;

    public class ContentPartQuery
    {
        public readonly Stream Stream;
        public readonly Identifier Identifier;
        public readonly Content Content;

        public ContentPartQuery(Stream stream, in Identifier identifier, Content content)
        {
            Stream = stream;
            Identifier = identifier;
            Content = content;
        }
    }
}
