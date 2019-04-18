namespace EtAlii.Ubigia.Api.Functional
{
    using System.Collections.Generic;
    using Moppet.Lapa;

    internal class NodeFinder : INodeFinder
    {
        public LpNode FindFirst(LpNode node, string id)
        {
            return FindFirst(new[] { node }, id);
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
            return FindAll(new[] { node }, id);
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
        //    return FindFirst(new LpNode[] { node })
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
    }
}
