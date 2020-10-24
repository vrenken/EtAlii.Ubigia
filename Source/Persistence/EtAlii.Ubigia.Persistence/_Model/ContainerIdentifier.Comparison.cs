﻿namespace EtAlii.Ubigia.Persistence
{
    using System;

    public partial struct ContainerIdentifier : IEquatable<ContainerIdentifier>
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

            return Equals((ContainerIdentifier)obj);
        }

        public bool Equals(ContainerIdentifier other)
        {
            // Cannot happen: If parameter is null, return false. 
            // Cannot happen: Optimization for a common success case. 
            // Can happen, but is not problematic: If run-time types are not exactly the same, return false. 

            // Return true if the fields match. 
            // Note that the base class is not invoked because it is 
            // System.Object, which defines Equals as reference equality. 
            if (other.Paths.Length != Paths.Length)
            {
                return false;
            }

            for (var i = 0; i < Paths.Length; i++)
            {
                if (other.Paths[i] != Paths[i])
                {
                    return false;
                }
            }
            
            return true;
        }

        public static bool operator ==(ContainerIdentifier first, ContainerIdentifier second)
        {
            var equals = first.Equals(second);
            return equals;
        }

        public static bool operator !=(ContainerIdentifier first, ContainerIdentifier second)
        {
            var equals = first == second;
            return !equals;
        }

        #region Hashing

        public override int GetHashCode()
        {
            var result = 0;

            for (var i = 0; i < Paths.Length; i++)
            {
                var path = Paths[i];
                var power = 2 ^ i; 
                result ^= ShiftAndWrap(path.GetHashCode(), power);
            }

            return result;
        } 

        private int ShiftAndWrap(int value, int positions)
        {
            positions = positions & 0x1F;

            // Save the existing bit pattern, but interpret it as an unsigned integer. 
            var number = BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
            // Preserve the bits to be discarded. 
            var wrapped = number >> (32 - positions);
            // Shift and wrap the discarded bits. 
            return BitConverter.ToInt32(BitConverter.GetBytes((number << positions) | wrapped), 0);
        }

        #endregion Hashing
    }
}