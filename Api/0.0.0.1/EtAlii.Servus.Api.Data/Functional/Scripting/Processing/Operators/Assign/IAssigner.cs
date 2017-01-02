namespace EtAlii.Servus.Api.Data
{
    using System.Collections;
    using EtAlii.Servus.Api.Data.Model;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    internal interface IAssigner
    {
        object Assign(ProcessParameters<Operator, SequencePart> parameters);
    }
}
