// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.MemoryMappedFiles;
    using System.Linq;
    using System.Runtime.InteropServices;

    public sealed partial class Ipv4FreePortFinder
    {
        public static Ipv4FreePortFinder Current => _current.Value;
        private static readonly Lazy<Ipv4FreePortFinder> _current = new(() =>
        {
            AppDomain.CurrentDomain.UnhandledException += (_, _) => _current.Value.Dispose();
            AppDomain.CurrentDomain.DomainUnload += (_, _) => _current.Value.Dispose();

            return new Ipv4FreePortFinder();
        });

        public Ipv4FreePortFinder()
        {
            _memoryMappedIpcFileName = $"{nameof(Ipv4FreePortFinder)}_IpcFile";
            _structureSize = Marshal.SizeOf(typeof(PortRegistration));
            _fileSize =
                Marshal.SizeOf(typeof(ushort)) + // A short to indicate the current number of registrations stored in the file.
                _structureSize * 0xFFFF; // We want to have at max 0xFF (=65535) registrations (there are no more ports.

            // Create the memory-mapped file.
            _memoryMappedFile = MemoryMappedFile.CreateFromFile(_memoryMappedIpcFileName, FileMode.OpenOrCreate, null, _fileSize);
        }

        /// <summary>
        /// As a default we don't want to use 49152 - 65535 (IANA) nor 1025 - 5000.
        /// So let's stay between 5200 and 8000.
        /// </summary>
        /// <param name="numberOfPorts"></param>
        /// <returns></returns>
        public PortRange Get(ushort numberOfPorts) => Get(new PortRange(5200, 8000), numberOfPorts, TimeSpan.FromSeconds(10));

        public PortRange Get(PortRange fromRange, ushort numberOfPorts) => Get(fromRange, numberOfPorts, TimeSpan.FromSeconds(10));

        // ReSharper disable once MemberCanBePrivate.Global
        public PortRange Get(PortRange fromRange, ushort numberOfPorts, TimeSpan lease)
        {
            // We want to allow only one thread access to the port range magic at any given moment.
            using (var _ = new SystemSafeExecutionScope(_uniqueId))
            {
                var result = GetInternal(fromRange, numberOfPorts, lease);
                return result;
            }
        }

        private PortRange GetInternal(PortRange fromRange, ushort numberOfPorts, TimeSpan lease)
        {
            var registrations = GetPortRegistrations();

            var now = DateTime.UtcNow;
            var leaseExpirationMoment = DateTime.UtcNow - lease;
            var registrationsAsList = CleanupExpiredPortRegistrations(registrations, leaseExpirationMoment);

            var freePorts = FindOnSystem(fromRange, numberOfPorts, registrationsAsList);

            registrationsAsList.AddRange(freePorts.Select(port => new PortRegistration { Port = port, RegisteredAt = now}));

            registrations = registrationsAsList.ToArray();
            SetPortRegistrations(registrations);

            return new PortRange(freePorts[0], freePorts[^1]);
        }

        private List<PortRegistration> CleanupExpiredPortRegistrations(PortRegistration[] registrations, DateTime leaseExpirationMoment)
        {
            var registrationsAsList = new List<PortRegistration>(registrations);

            for (var i = registrations.Length - 1; i >= 0; i--)
            {
                if (registrations[i].RegisteredAt < leaseExpirationMoment)
                {
                    registrationsAsList.RemoveAt(i);
                }
            }

            return registrationsAsList;
        }
    }
}
