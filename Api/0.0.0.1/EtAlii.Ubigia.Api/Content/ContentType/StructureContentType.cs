namespace EtAlii.Ubigia.Api
{
    public class StructureContentType : ContentType
    {
        private const string _structureContentTypeId = "Structure";

        public ContentType Hierarchy => _hierarchy;
        private readonly ContentType _hierarchy = new ContentType(_structureContentTypeId, "Hierarchy");

        internal StructureContentType()
            : base(_structureContentTypeId)
        {
        }
    }
}