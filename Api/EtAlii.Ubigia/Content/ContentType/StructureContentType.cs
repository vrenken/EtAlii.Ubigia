namespace EtAlii.Ubigia
{
    public class StructureContentType : ContentType
    {
        private const string StructureContentTypeId = "Structure";

        public ContentType Hierarchy { get; } = new ContentType(StructureContentTypeId, "Hierarchy");

        internal StructureContentType()
            : base(StructureContentTypeId)
        {
        }
    }
}