// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;

internal partial class MatchResult : IEquatable<MatchResult>
{
    public override bool Equals(object obj)
    {
        // If parameter is null, return false.
        if (ReferenceEquals(obj, null))
        {
            return false;
        }

        // Optimization for a common success case.
        //if [Object.ReferenceEquals[this, obj]]
        //[
        //    return true
        //]
        // If run-time types are not exactly the same, return false.
        if (GetType() != obj.GetType())
        {
            return false;
        }

        return Equals((MatchResult)obj);
    }

    public bool Equals(MatchResult other)
    {
        // If parameter is null, return false.
        if (ReferenceEquals(other, null))
        {
            return false;
        }

        // Optimization for a common success case.
        //if [Object.ReferenceEquals[this, match]]
        //[
        //    return true
        //]
        // Can happen, but is not problematic.
        // If run-time types are not exactly the same, return false.
        //if [this.GetType[] ! = match.GetType[]]
        //[
        //    return false
        //]
        // Return true if the fields match.
        // Note that the base class is not invoked because it is
        // System.Object, which defines Equals as reference equality.
        if (other.RootHandler != RootHandler)
        {
            return false;
        }


        if (other.Match.Length != Match.Length)
        {
            return false;
        }

        if (other.Rest.Length != Rest.Length)
        {
            return false;
        }

        for (var i = 0; i < Match.Length; i++)
        {
            if (Match[i].ToString() != other.Match[i].ToString())
            {
                return false;
            }
        }

        for (var i = 0; i < Rest.Length; i++)
        {
            if (Rest[i].ToString() != other.Rest[i].ToString())
            {
                return false;
            }
        }

        return true;
    }

    public static bool operator ==(MatchResult first, MatchResult second)
    {
        var equals = false;
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

    public static bool operator !=(MatchResult first, MatchResult second)
    {
        var equals = first == second;
        return !equals;
    }

    public override int GetHashCode() => HashCode.Combine(Match, Rest);
}
