// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public sealed class StructureContentType : ContentType
    {
        private const string StructureContentTypeId = "Structure";

        public ContentType Hierarchy { get; } = new(StructureContentTypeId, "Hierarchy");

        internal StructureContentType()
            : base(StructureContentTypeId)
        {
        }
    }
}
