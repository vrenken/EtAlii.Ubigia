namespace EtAlii.Servus.Api.Data
{
    using System;
    using Moppet.Lapa;
    using System.Collections.Generic;

    internal class ParserHelper : IParserHelper
    {
        public LpNode FindFirst(LpNode node, string id)
        {
            return FindFirst(new LpNode[] { node }, id);
        }

        public LpNode FindFirst(IEnumerable<LpNode> nodes, string id)
        {
            LpNode result = null;

            if(nodes != null)
            {
                foreach(var node in nodes)
                {
                    if (node.Id == id)
                    {
                        result = node;
                    }
                    else
                    {
                        result = FindFirst(node.Children, id);
                    }
                    if (result != null)
                    {
                        break;
                    }
                }
            }
            return result;
        }

        public IEnumerable<LpNode> FindAll(LpNode node, string id)
        {
            return FindAll(new LpNode[] { node }, id);
        }

        public IEnumerable<LpNode> FindAll(IEnumerable<LpNode> nodes, string id)
        {
            var result = new List<LpNode>();

            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    if (node.Id == id)
                    {
                        result.Add(node);
                    }
                    else
                    {
                        var subResult = FindAll(node.Children, id);
                        result.AddRange(subResult);
                    }
                }
            }
            return result;
        }

        //public LpNode FindFirst(LpNode node)
        //{
        //    return FindFirst(new LpNode[] { node });
        //}

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

        public void EnsureSuccess(LpNode node, string nodeType, bool noRestExpected = false, string text = null)
        {
            var failedBecauseOfRest = noRestExpected && node.Rest.Length > 0;
            var failedBecauseOfSuccess = !node.Success;

            if (failedBecauseOfRest || failedBecauseOfSuccess)
            {
                var format = String.IsNullOrWhiteSpace(text)
                    ? "Unable to process {0}\r\nId={3}\r\nNode='{1}'\r\nRest='{2}'"
                    : "Unable to process {0}\r\nId={4}\r\nNode='{1}'\r\nRest='{2}'\r\nText='{3}'";
                var message = String.Format(format, nodeType, node.ToString(), node.Rest.ToString(), text, node.Id);

                throw new ScriptParserException(message);
            }
        }

        public void EnsureSuccess2(LpNode node, string requiredId, bool restIsAllowed = true)
        {
            var failedBecauseOfRest = restIsAllowed ? false : node.Rest.Length > 0;
            var failedBecauseOfSuccess = !node.Success;
            var failedBecauseOfId = node.Id != requiredId;

            if (failedBecauseOfRest || failedBecauseOfSuccess || failedBecauseOfId)
            {
                var format = "Unable to process {0}\nId={4}\nNode='{1}'\nRest='{2}'\nText='{3}'";
                var message = String.Format(format, requiredId, node.ToString(), node.Rest.ToString(), node.Match.ToString(), node.Id);

                throw new ScriptParserException(message);
            }
        }

        
    }
}
