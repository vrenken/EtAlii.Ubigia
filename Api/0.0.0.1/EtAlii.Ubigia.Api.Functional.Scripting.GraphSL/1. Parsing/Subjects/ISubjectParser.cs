namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal interface ISubjectParser
    {
        LpsParser Parser { get; }
        Subject Parse(LpNode node);
        bool CanParse(LpNode node);
        void Validate(SequencePart before, Subject subject, int subjectIndex, SequencePart after);
        bool CanValidate(Subject subject);
    }
}
