namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal interface IAnnotationParser
    {
        string Id { get; }
        
        LpsParser Parser { get; }
        Annotation Parse(LpNode node);
        
        // TODO: We should implement this on a similar way to how it is done in the ScriptParser.
        //bool CanParse(LpNode node)

        //void Validate(SequencePart before, ConstantSubject subject, int constantSubjectIndex, SequencePart after)
        //bool CanValidate(ConstantSubject constantSubject)

    }
}