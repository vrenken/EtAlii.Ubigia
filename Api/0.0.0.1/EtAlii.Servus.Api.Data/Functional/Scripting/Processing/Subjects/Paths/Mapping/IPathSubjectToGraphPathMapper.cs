namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Servus.Api.Data.Model;

    internal interface IPathSubjectToGraphPathMapper
    {
        GraphPath Map(PathSubject pathSubject);
    }
}
