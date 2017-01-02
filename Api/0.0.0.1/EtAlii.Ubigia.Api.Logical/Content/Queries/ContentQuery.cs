namespace EtAlii.Ubigia.Api.Logical
{
    using System.Diagnostics;

    [DebuggerStepThrough]
    public class ContentQuery
    {
        public readonly Identifier Identifier;

        public ContentQuery(Identifier identifier)
        {
            Identifier = identifier;
        }
    }
}
