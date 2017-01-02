namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;

    public class ContentQuery
    {
        public readonly Identifier Identifier;

        public ContentQuery(Identifier identifier)
        {
            Identifier = identifier;
        }
    }
}
