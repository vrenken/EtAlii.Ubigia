namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal interface IValueQueryParser
    {
        LpsParser Parser { get; }
        string Id { get; }

        ValueFragment Parse(LpNode node);
        bool CanParse(LpNode node);
    }
}