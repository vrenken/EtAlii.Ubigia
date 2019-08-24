namespace EtAlii.Ubigia.Api.Functional 
{
    using EtAlii.Ubigia.Api.Functional.Scripting;

    internal interface IPathCorrecter
    {
        PathSubject Correct(Annotation annotation, PathSubject path);
    }
}
