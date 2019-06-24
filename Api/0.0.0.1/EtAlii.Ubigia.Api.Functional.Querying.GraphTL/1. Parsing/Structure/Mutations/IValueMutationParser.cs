namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal interface IValueMutationParser
    {
        LpsParser Parser { get; }
        string Id { get; }

        ValueMutation Parse(LpNode node);
        bool CanParse(LpNode node);
    }
}