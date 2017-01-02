﻿namespace EtAlii.Servus.Api
{
    using System;

    public partial class Node : IInternalNode, INode, IEquatable<Node>
    {
        public override bool Equals(object obj)
        {
            // If parameter is null, return false. 
            if (Object.ReferenceEquals(obj, null))
            {
                return false;
            }

            // Optimization for a common success case. 
            if (Object.ReferenceEquals(this, obj))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false. 
            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            return this.Equals((Node)obj);
        }

        public bool Equals(Node node)
        {
            // If parameter is null, return false. 
            if (Object.ReferenceEquals(node, null))
            {
                return false;
            }

            // Optimization for a common success case. 
            if (Object.ReferenceEquals(this, node))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false. 
            if (this.GetType() != node.GetType())
            {
                return false;
            }

            // Return true if the fields match. 
            // Note that the base class is not invoked because it is 
            // System.Object, which defines Equals as reference equality. 
            if (this._entry.Id != ((IInternalNode)node).Entry.Id)
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
            return this._entry.Id.GetHashCode();
        }

        #endregion Hashing
    }
}