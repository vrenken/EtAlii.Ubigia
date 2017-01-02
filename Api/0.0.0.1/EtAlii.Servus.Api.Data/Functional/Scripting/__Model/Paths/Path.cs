namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Path
    {
        public IEnumerable<PathComponent> Components { get; private set; }

        public Path(IEnumerable<PathComponent> components)
        {
            this.Components = components;
        }

        public override string ToString()
        {
            return String.Join("/", Components.Select(c => c.ToString()));
        }
    }
}
