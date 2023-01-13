// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia;

using System;

public partial struct Relation
{

    public override bool Equals(object obj)
    {
        // If parameter is null, return false.
        if (ReferenceEquals(obj, null))
        {
            return false;
        }

        // If run-time types are not exactly the same, return false.
        if (GetType() != obj.GetType())
        {
            return false;
        }

        return Equals((Relation)obj);
    }

    public bool Equals(Relation other)
    {
        // If run-time types are not exactly the same, return false.
        if (GetType() != other.GetType())
        {
            return false;
        }

        // Return true if the fields match.
        // Note that the base class is not invoked because it is
        // System.Object, which defines Equals as reference equality.
        if (other.Id != Id)
        {
            return false;
        }

        if (other.Moment != Moment)
        {
            return false;
        }

        return true;
    }

    public static bool operator ==(Relation first, Relation second)
    {
        var equals = first.Equals(second);
        return equals;
    }

    public static bool operator !=(Relation first, Relation second)
    {
        var equals = first == second;
        return !equals;
    }

    public override int GetHashCode() => HashCode.Combine(Id, Moment);
}
