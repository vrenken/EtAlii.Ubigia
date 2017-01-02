namespace EtAlii.Servus.Api.Data
{
    using System;
    using Moppet.Lapa;
    using System.Collections.Generic;

    internal interface IConstantHelper
    {
        bool IsValidConstantCharacter(char c);
        bool IsValidQuotedConstantCharacter(char c);
    }
}
