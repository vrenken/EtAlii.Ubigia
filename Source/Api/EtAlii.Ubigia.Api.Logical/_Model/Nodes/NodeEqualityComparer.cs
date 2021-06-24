// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;

    public class NodeEqualityComparer : IEqualityComparer<Node>
    {
        // ReSharper disable once InconsistentNaming
        public static readonly NodeEqualityComparer Default = new();

        public bool Equals(Node x, Node y)
        {
            if (x == null && y != null || x != null && y == null)
            {
                return false;
            }

            // Optimization for a common success case.
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (x.GetType() != y.GetType())
            {
                return false;
            }

            // Return true if the fields match.
            // Note that the base class is not invoked because it is
            // System.Object, which defines Equals as reference equality.
            return ((IInternalNode)x).Entry.Id == ((IInternalNode)y).Entry.Id;
        }

        #pragma warning disable S2328
        // TODO: Investigate Node.GetHashCode behavior.
        // Ok, calculating the hash from a non-readonly member is a bad thing, however in the case of a Node we use
        // a pattern of lazy-loading/updating for which it feels it is allowed to calculate the hash from the most
        // recent Identifier. However, we must investigate this further to see if it really is not a problem.
        // Thinking about it further it really might be a bad thing. However it is outside of the current scope
        // of activities (providing proof for Ubiquitous Information Systems). Therefore we convert the
        // SonarCube bug warning into a to-do. Below some more information:
        // http://sonarqube/coding_rules?open=csharpsquid%3AS2328&rule_key=csharpsquid%3AS2328
        public int GetHashCode(Node obj)
        {
            return ((IInternalNode)obj).Entry.Id.GetHashCode();
        }
        #pragma warning restore S2328
    }
}
