// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;
    using System.Linq;

    public class TemporalGraphPathWeaver : ITemporalGraphPathWeaver
    {
        public GraphPath Weave(GraphPath path)
        {
            var result = new List<GraphPathPart>();

            for (var i = 0; i < path.Length; i++)
            {
                var currentPart = path[i];
                var nextPart = i < path.Length - 1 ? path[i + 1] : null;

                result.Add(currentPart);

                if (currentPart != GraphRelation.Original &&
                    currentPart != GraphRelation.AllDowndates &&
                    currentPart != GraphRelation.Downdate &&
                    currentPart != GraphRelation.Updates &&
                    currentPart != GraphRelation.AllUpdates &&
                    currentPart != GraphRelation.Final &&
                    nextPart != GraphRelation.Original &&
                    nextPart != GraphRelation.AllDowndates &&
                    nextPart != GraphRelation.Downdate &&
                    nextPart != GraphRelation.Updates &&
                    nextPart != GraphRelation.AllUpdates &&
                    nextPart != GraphRelation.Final)
                {
                    result.Add(GraphRelation.Final);
                }
                else
                {
                    // TODO: BIG One: Improve the Temporal Weaver so that it weaves in temporal 'last' relations
                    // until the first temporal directive is spotted.
                    result.AddRange(path.Skip(i + 1));
                    break;
                }

            }

            return new GraphPath(result.ToArray());
        }

        public GraphPath Weave_Original(GraphPath path)
        {
            var pathParts = path.Select(p => new[] { p, GraphRelation.Final })
                .SelectMany(m => m)
                .ToList();

            GraphPathPart[] gpps;
            if (pathParts.Count > 1)
            {
                gpps = pathParts.ToArray();
            }
            else
            {
                gpps = new GraphPathPart[] { GraphRelation.Final };
            }
            return new GraphPath(gpps);
        }
    }
}
