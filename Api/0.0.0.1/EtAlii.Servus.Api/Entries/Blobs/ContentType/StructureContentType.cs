namespace EtAlii.Servus.Api
{
    using System;

    public class StructureContentType : ContentType
    {
        private const string _structureContentTypeId = "Structure";

        public ContentType Hierarchy { get { return _hierarchy; } }
        private readonly ContentType _hierarchy = new ContentType(_structureContentTypeId, "Hierarchy");

        internal StructureContentType()
            : base(_structureContentTypeId)
        {
        }
    }
}