namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api.Data.Model;
    using Moppet.Lapa;

    internal interface IOperatorParser
    {
        LpsParser Parser { get; }
        bool CanParse(LpNode node);
        Operator Parse(LpNode node);
        bool CanValidate(Operator @operator);
        void Validate(SequencePart before, Operator @operator, int partIndex, SequencePart after);
    }
}
