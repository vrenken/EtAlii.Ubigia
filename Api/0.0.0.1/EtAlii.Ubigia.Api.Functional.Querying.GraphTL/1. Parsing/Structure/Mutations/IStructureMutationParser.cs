namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal interface IStructureMutationParser
    {
        string Id { get; }
        
        LpsParser Parser { get; }
        StructureMutation Parse(LpNode node);
        bool CanParse(LpNode node);

        void Validate(SequencePart before, ConstantSubject subject, int constantSubjectIndex, SequencePart after);
        bool CanValidate(ConstantSubject constantSubject);

    }
}