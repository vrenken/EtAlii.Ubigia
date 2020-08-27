namespace EtAlii.xTechnology.Networking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    public sealed partial class Ipv4FreePortFinder
    {
        public static Ipv4FreePortFinder Current => Instance.Value;
        private static readonly Lazy<Ipv4FreePortFinder> Instance = new Lazy<Ipv4FreePortFinder>(() => new Ipv4FreePortFinder());

        public Ipv4FreePortFinder()
        {
            _memoryMappedIpcFileName = $"{nameof(Ipv4FreePortFinder)}_IpcFile";
            _structureSize = Marshal.SizeOf(typeof(PortRegistration));
            _fileSize = 
                Marshal.SizeOf(typeof(ushort)) + // A short to indicate the current number of registrations stored in the file.
                _structureSize * 0xFFFF; // We want to have at max 0xFF (=65535) registrations (there are no more ports. 
        }

        public PortRange Get(ushort startingPort, ushort numberOfPorts) => Get(new PortRange(startingPort, ushort.MaxValue), numberOfPorts, TimeSpan.FromSeconds(30));

        public PortRange Get(PortRange range, ushort numberOfPorts) => Get(range, numberOfPorts, TimeSpan.FromSeconds(30));

        public PortRange Get(ushort startingPort, ushort numberOfPorts, TimeSpan lease) => Get(new PortRange(startingPort, ushort.MaxValue), numberOfPorts, lease);

        // ReSharper disable once MemberCanBePrivate.Global
        public PortRange Get(PortRange fromRange, ushort numberOfPorts, TimeSpan lease)
        {
            // We need to run the corresponding code without any conflicting access.   
            return SystemExclusiveExecution.Run(GetInternal, fromRange, numberOfPorts, lease);
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