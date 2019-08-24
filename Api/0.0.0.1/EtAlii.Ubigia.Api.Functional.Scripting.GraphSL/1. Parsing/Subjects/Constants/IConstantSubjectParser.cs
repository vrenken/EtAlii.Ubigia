namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using Moppet.Lapa;

    internal interface IConstantSubjectParser
    {
        LpsParser Parser { get; }
        ConstantSubject Parse(LpNode node);
        bool CanParse(LpNode node);

        void Validate(SequencePart before, ConstantSubject subject, int constantSubjectIndex, SequencePart after);
        bool CanValidate(ConstantSubject constantSubject);
    }
}
