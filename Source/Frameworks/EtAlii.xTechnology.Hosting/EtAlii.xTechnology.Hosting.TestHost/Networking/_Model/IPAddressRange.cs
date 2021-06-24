// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    // ReSharper disable once InconsistentNaming
    public readonly struct IPAddressRange : IEnumerable<IPAddress>
    {
        private readonly System.Net.Sockets.AddressFamily _addressFamily;
        private readonly byte[] _lowerBytes;
        private readonly byte[] _upperBytes;

        public IPAddress LowerAddress { get; }
        public IPAddress UpperAddress { get; }

        public IPAddressRange(IPAddress lowerAddressInclusive, IPAddress upperAddressInclusive)
        {
            if (lowerAddressInclusive.AddressFamily != upperAddressInclusive.AddressFamily)
            {
                throw new InvalidOperationException("Unable to create a range for the specified IP addresses");
            }

            LowerAddress = lowerAddressInclusive;
            UpperAddress = upperAddressInclusive;
            
            _addressFamily = lowerAddressInclusive.AddressFamily;
            _lowerBytes = lowerAddressInclusive.GetAddressBytes();
            _upperBytes = upperAddressInclusive.GetAddressBytes();
        }

        public bool IsInRange(IPAddress address)
        {
            if (address.AddressFamily != _addressFamily)
            {
                return false;
            }

            var addressBytes = address.GetAddressBytes();

            bool lowerBoundary = true, upperBoundary = true;

            for (var i = 0; i < _lowerBytes.Length && 
                            (lowerBoundary || upperBoundary); i++)
            {
                if ((lowerBoundary && addressBytes[i] < _lowerBytes[i]) ||
                    (upperBoundary && addressBytes[i] > _upperBytes[i]))
                {
                    return false;
                }

                lowerBoundary &= (addressBytes[i] == _lowerBytes[i]);
                upperBoundary &= (addressBytes[i] == _upperBytes[i]);
            }

            return true;
        }

        public IEnumerator<IPAddress> GetEnumerator()
        {
            var aRange = Enumerable.Range(_lowerBytes[0], _upperBytes[0] - _lowerBytes[0] + 1).ToArray();           
            var bRange = Enumerable.Range(_lowerBytes[1], _upperBytes[1] - _lowerBytes[1] + 1).ToArray();           
            var cRange = Enumerable.Range(_lowerBytes[2], _upperBytes[2] - _lowerBytes[2] + 1).ToArray();           
            var dRange = Enumerable.Range(_lowerBytes[3], _upperBytes[3] - _lowerBytes[3] + 1).ToArray();

            foreach (var a in aRange)
            {
                foreach (var b in bRange)
                {
                    foreach (var c in cRange)
                    {
                        foreach (var d in dRange)
                        {
                            yield return new IPAddress(new []{(byte)a,(byte)b,(byte)c,(byte)d});
                        }
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}