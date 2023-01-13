// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Diagnostics.Profiling;

using System;

public partial class ProfilingAspect : IEquatable<ProfilingAspect>
{

    public override bool Equals(object obj)
    {
        // If parameter is null, return false.
        if (ReferenceEquals(obj, null))
        {
            return false;
        }

        // Cannot happen.
        // Optimization for a common success case.
        //if (Object.ReferenceEquals(this, obj))
        //[
        //    return true
        //]

        // If run-time types are not exactly the same, return false.
        if (GetType() != obj.GetType())
        {
            return false;
        }

        return Equals((ProfilingAspect)obj);
    }

    public bool Equals(ProfilingAspect other)
    {
        // Cannot happen.
        // If parameter is null, return false.
        if (ReferenceEquals(other, null))
        {
            return false;
        }

        // Cannot happen.
        // Optimization for a common success case.
        if (ReferenceEquals(this, other))
        {
            return true;
        }

        // Can happen, but is not problematic.
        // If run-time types are not exactly the same, return false.
        if (GetType() != other.GetType())
        {
            return false;
        }

        // Return true if the fields match.
        // Note that the base class is not invoked because it is
        // System.Object, which defines Equals as reference equality.
        if (other.Layer != Layer)
        {
            return false;
        }

        if (other.Id != Id)
        {
            return false;
        }

        return true;
    }

    public static bool operator ==(ProfilingAspect first, ProfilingAspect second)
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

    public static bool operator !=(ProfilingAspect first, ProfilingAspect second)
    {
        var equals = first == second;
        return !equals;
    }

    public override int GetHashCode() => HashCode.Combine(Layer, Id);
}
