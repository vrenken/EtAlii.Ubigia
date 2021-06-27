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

        // Is this GetHashCode behavior correct?
        // More details can be found in the Github issue below:
        // https://github.com/vrenken/EtAlii.Ubigia/issues/75
        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return _entry.Id.GetHashCode();
        }
    }
}
