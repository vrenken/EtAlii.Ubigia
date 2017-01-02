namespace EtAlii.Servus.Api.Data
{
    using Moppet.Lapa;
    using System.Collections.Generic;
    using System.Linq;

    internal interface IActionParser
    {
        string ActionId { get; }
        Action Parse(LpNode node);

        LpsParser ExpressionParser { get; }
    }
}
