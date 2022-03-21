// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.IO.MemoryMappedFiles;

    public sealed partial class Ipv4FreePortFinder
    {
        private static readonly Guid _uniqueId = Guid.Parse("0EFB08CD-7EBF-4D6E-9D91-D73C3CA98CAC"); 
        
        private readonly string _memoryMappedIpcFileName;
        private readonly int _fileSize; 
        private readonly int _structureSize;
        private readonly MemoryMappedFile _memoryMappedFile;

        private PortRegistration[] GetPortRegistrations()
        {
            // Create a random access view.
            using var accessor = _memoryMappedFile.CreateViewAccessor();

            accessor.Read(0, out short numberOfRegistrations);

            var registrations = new PortRegistration[numberOfRegistrations];
            accessor.ReadArray(2, registrations, 0, numberOfRegistrations);
            return registrations;
        }

        private void SetPortRegistrations(PortRegistration[] registrations)
        {
            // Create a random access view.
            using var accessor = _memoryMappedFile.CreateViewAccessor();

            var numberOfRegistrations = (short)registrations.Length;
            
            accessor.Write(0, numberOfRegistrations);

            accessor.WriteArray(2, registrations, 0, numberOfRegistrations);
            
            accessor.Flush();
        }
    }
}