namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    internal interface IPathCorrecter
    {
        PathSubject Correct(NodeAnnotation annotation, PathSubject path);
    }
}
