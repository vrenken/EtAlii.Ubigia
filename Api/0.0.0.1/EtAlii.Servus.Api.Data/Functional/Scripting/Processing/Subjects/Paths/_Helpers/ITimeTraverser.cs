namespace EtAlii.Servus.Api.Data
{
    using System.Collections;
    using System.Collections.Generic;

    internal interface ITimeTraverser
    {
        IReadOnlyEntry Traverse(IReadOnlyEntry entry);
    }
}