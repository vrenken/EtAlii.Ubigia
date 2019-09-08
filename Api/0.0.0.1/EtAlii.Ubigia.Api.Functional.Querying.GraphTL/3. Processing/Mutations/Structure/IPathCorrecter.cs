namespace EtAlii.Ubigia.Api.Functional 
{
    using EtAlii.Ubigia.Api.Functional.Scripting;

    internal interface IPathCorrecter
    {
        PathSubject Correct(NodeAnnotation annotation, PathSubject path);
    }
}
