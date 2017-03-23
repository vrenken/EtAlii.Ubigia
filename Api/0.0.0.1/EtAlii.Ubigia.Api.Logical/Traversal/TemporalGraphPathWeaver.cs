namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;
    using System.Linq;

    public class TemporalGraphPathWeaver : ITemporalGraphPathWeaver
    {
        public GraphPath Weave(GraphPath path)
        {
            var result = new List<GraphPathPart>();

            for (int i = 0; i < path.Length; i++)
            {
                var currentPart = path[i];
                var nextPart = i < path.Length - 1 ? path[i + 1] : null;

                result.Add(currentPart);

                if (currentPart != GraphRelation.Downdate &&
                    currentPart != GraphRelation.Update &&
                    currentPart != GraphRelation.Original &&
                    currentPart != GraphRelation.Final &&
                    nextPart != GraphRelation.Downdate &&
                    nextPart != GraphRelation.Update &&
                    nextPart != GraphRelation.Original &&
                    nextPart != GraphRelation.Final)
                {
                    result.Add(GraphRelation.Final);
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
