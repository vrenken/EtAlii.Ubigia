namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using Moppet.Lapa;

    internal interface IStructureFragmentParser
    {
        string Id { get; }
        
        LpsParser Parser { get; }
        StructureFragment Parse(LpNode node);
        bool CanParse(LpNode node);

        void Validate(SequencePart before, ConstantSubject item, int itemIndex, SequencePart after);
        bool CanValidate(ConstantSubject item);

    }
}