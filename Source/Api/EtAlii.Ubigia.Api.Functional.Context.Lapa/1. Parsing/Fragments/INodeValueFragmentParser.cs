namespace EtAlii.Ubigia.Api.Functional.Context
{
    using Moppet.Lapa;

    internal interface INodeValueFragmentParser
    {
        LpsParser Parser { get; }
        string Id { get; }

        ValueFragment Parse(LpNode node);
        bool CanParse(LpNode node);
    }
}
