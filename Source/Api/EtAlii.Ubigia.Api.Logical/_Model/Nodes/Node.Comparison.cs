// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;

    public partial class Node : IEquatable<Node>
    {

        // TODO: Determine if the Node / DynamicNode classes should have their operators overloaded or not.
        // SonarQube states that this shouldn't be the case.

        /// <inheritdoc />
        public override bool Equals(object obj) => NodeEqualityComparer.Default.Equals(this, obj as Node);

        /// <inheritdoc />
        public bool Equals(Node other) => NodeEqualityComparer.Default.Equals(this, other);

        public static bool operator ==(Node first, Node second) => NodeEqualityComparer.Default.Equals(first, second);

        public static bool operator !=(Node first, Node second) => !NodeEqualityComparer.Default.Equals(first, second);

        // Is this GetHashCode behavior correct?
        // More details can be found in the Github issue below:
        // https://github.com/vrenken/EtAlii.Ubigia/issues/75
        /// <inheritdoc />
        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return _entry.Id.GetHashCode();
        }
    }
}
