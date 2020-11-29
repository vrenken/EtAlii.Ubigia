namespace EtAlii.Ubigia
{
    public class Content : BlobBase, IReadOnlyContent
    {
        internal const string ContentName = "Content";

        protected internal override string Name => ContentName;
    }
}