// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    public partial class Node
    {
        public override bool Equals(object obj)
        {
            return NodeEqualityComparer.Default.Equals(this, obj as Node);
        }
// TODO: Determine if the Node / DynamicNode classes should have their operators overloaded or not.
// SonarQube states that this shouldn't be the case.
//        public static bool operator ==(Node first, Node second)
//        [
//            return NodeEqualityComparer.Default.Equals(first, second)
//        ]
//
//        public static bool operator !=(Node first, Node second)
//        [
//            var equals = first == second
//            return !equals
//        ]

        #region Hashing

        #pragma warning disable S2328
        // TODO: Investigate Node.GetHashCode behavior.
        // Ok, calculating the hash from a non-readonly member is a bad thing, however in the case of a Node we use
        // a pattern of lazy-loading/updating for which it feels it is allowed to calculate the hash from the most
        // recent Identifier. However, we must investigate this further to see if it really is not a problem.
        // Thinking about it further it really might be a bad thing. However it is outside of the current scope
        // of activities (providing proof for Ubiquitous Information Systems). Therefore we convert the
        // SonarCube bug warning into a to-do. Below some more information:
        // http://sonarqube:54001/coding_rules?open=csharpsquid%3AS2328&rule_key=csharpsquid%3AS2328
        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return _entry.Id.GetHashCode();
        }
        #pragma warning restore S2328

        #endregion Hashing

    }
}
