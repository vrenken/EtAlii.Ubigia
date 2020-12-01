namespace EtAlii.Ubigia
{
    public class StructureContentType : ContentType
    {
        private const string _structureContentTypeId = "Structure";

        public ContentType Hierarchy { get; } = new(_structureContentTypeId, "Hierarchy");

        internal StructureContentType()
            : base(_structureContentTypeId)
        {
        }
    }
}