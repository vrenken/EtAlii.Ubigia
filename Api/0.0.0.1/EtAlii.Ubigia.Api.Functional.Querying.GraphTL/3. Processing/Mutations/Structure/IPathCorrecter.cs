namespace EtAlii.Ubigia.Api.Functional 
{
    internal interface IPathCorrecter
    {
        PathSubject Correct(Annotation annotation, PathSubject path);
    }
}
