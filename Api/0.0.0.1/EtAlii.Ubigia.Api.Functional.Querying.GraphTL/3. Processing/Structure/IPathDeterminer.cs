namespace EtAlii.Ubigia.Api.Functional 
{
    internal interface IPathDeterminer
    {
        PathSubject Determine(FragmentMetadata fragmentMetadata, Annotation annotation, Identifier id);
    }
}
