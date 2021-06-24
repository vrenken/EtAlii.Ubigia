// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Collections.Generic;
    using Moppet.Lapa;

    internal class NodeValidator : INodeValidator
    {
        public LpNode FindFirst(IEnumerable<LpNode> nodes)
        {
            LpNode result = null;

            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    if (node.Id != null)
                    {
                        result = node;
                    }
                    else
                    {
                        result = FindFirst(node.Children);
                    }
                    if (result != null)
                    {
                        break;
                    }
                }
            }
            return result;
        }

        public void EnsureSuccess(LpNode node, string requiredId, bool restIsAllowed = true)
        {
            var failedBecauseOfRest = !restIsAllowed && node.Rest.Length > 0;
            var failedBecauseOfSuccess = !node.Success;
            var failedBecauseOfId = node.Id != requiredId;

            if (failedBecauseOfRest || failedBecauseOfSuccess || failedBecauseOfId)
            {
                var format = "Unable to process {0}\nId={4}\nNode='{1}'\nRest='{2}'\nText='{3}'";
                var message = string.Format(format, requiredId, node, node.Rest, node.Match, node.Id);

                throw new ScriptParserException(message);
            }
        }
    }
}
