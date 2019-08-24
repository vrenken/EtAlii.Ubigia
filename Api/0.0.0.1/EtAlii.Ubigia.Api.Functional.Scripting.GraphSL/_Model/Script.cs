﻿namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The Script class contains all information needed to execute actions on the current infrastructureClient.
    /// </summary>
    public class Script
    {
        public IEnumerable<Sequence> Sequences { get; }

        internal Script(Sequence sequences)
        {
            Sequences = new[] { sequences };
        }

        internal Script(IEnumerable<Sequence> sequences)
        {
            Sequences = sequences;
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Sequences);
        }
    }
}
