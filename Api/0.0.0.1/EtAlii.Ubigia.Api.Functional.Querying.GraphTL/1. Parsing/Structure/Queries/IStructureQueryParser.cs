namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal interface IStructureQueryParser
    {
        string Id { get; }
        
        LpsParser Parser { get; }
        StructureQuery Parse(LpNode node);
        bool CanParse(LpNode node);

        void Validate(SequencePart before, ConstantSubject subject, int constantSubjectIndex, SequencePart after);
        bool CanValidate(ConstantSubject constantSubject);

    }
}