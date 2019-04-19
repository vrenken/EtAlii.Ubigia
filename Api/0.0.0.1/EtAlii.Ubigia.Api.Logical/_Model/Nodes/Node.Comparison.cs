namespace EtAlii.Ubigia.Api.Logical
{
    using System;

    public partial class Node : IEquatable<Node>
    {
        public override bool Equals(object obj)
        {
            // If parameter is null, return false. 
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            // Optimization for a common success case. 
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false. 
            if (GetType() != obj.GetType())
            {
                return false;
            }

            return Equals((Node)obj);
        }

        public bool Equals(Node node)
        {
            // If parameter is null, return false. 
            if (ReferenceEquals(node, null))
            {
                return false;
            }

            // Optimization for a common success case. 
            if (ReferenceEquals(this, node))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false. 
            if (GetType() != node.GetType())
            {
                return false;
            }

            // Return true if the fields match. 
            // Note that the base class is not invoked because it is 
            // System.Object, which defines Equals as reference equality. 
            if (_entry.Id != ((IInternalNode)node).Entry.Id)
            {
                return false;
            }

            return true;
        }

        public static bool operator ==(Node first, Node second)
        {
            bool equals = false;
            if ((object)first != null && (object)second != null)
            {
                equals = first.Equals(second);
            }
            else if ((object)first == null && (object)second == null)
            {
                equals = true;
            }

            return equals;
        }

        public static bool operator !=(Node first, Node second)
        {
            bool equals = first == second;
            return !equals;
        }

        #region Hashing

        public override int GetHashCode()
        {
            #pragma warning disable S2328
            // TODO: Investigate Node.GetHashCode behavior.
            // Ok, calculating the hash from a non-readonly member is a bad thing, however in the case of a Node we use
            // a pattern of lazy-loading/updating for which it feels it is allowed to calculate the hash from the most
            // recent Identifier. However, we must investigate this further to see if it really is not a problem.
            // Thinking about it further it really might be a bad thing. However it is outside of the current scope
            // of activities (providing proof for Ubiquitous Information Systems). Therefore we convert the 
            // SonarCube bug warning into a TODO. Below some more information:
            // http://vrenken.duckdns.org:54001/coding_rules?open=csharpsquid%3AS2328&rule_key=csharpsquid%3AS2328 
            return _entry.Id.GetHashCode();
            #pragma warning restore S2328
        }

        #endregion Hashing
    }
}