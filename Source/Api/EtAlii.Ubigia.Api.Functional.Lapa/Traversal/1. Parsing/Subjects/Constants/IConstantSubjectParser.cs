namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal interface IConstantSubjectParser
    {
        LpsParser Parser { get; }
        ConstantSubject Parse(LpNode node);
        bool CanParse(LpNode node);
    }
}
