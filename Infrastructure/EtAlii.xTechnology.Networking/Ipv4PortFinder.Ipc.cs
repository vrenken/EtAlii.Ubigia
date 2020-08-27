namespace EtAlii.xTechnology.Networking
{
    using System.IO.MemoryMappedFiles;

    public sealed partial class Ipv4FreePortFinder
    {
        private readonly string _memoryMappedIpcFileName;
        private readonly int _fileSize; 
        private readonly int _structureSize;

        private PortRegistration[] GetPortRegistrations()
        {
            // Create the memory-mapped file.
            using var memoryMappedFile = MemoryMappedFile.CreateOrOpen(_memoryMappedIpcFileName, _fileSize);

            // Create a random access view, from the 256th megabyte (the offset)
            // to the 768th megabyte (the offset plus length).
            using var accessor = memoryMappedFile.CreateViewAccessor();

            accessor.Read(0, out short numberOfRegistrations);

            var registrations = new PortRegistration[numberOfRegistrations];
            accessor.ReadArray(2, registrations, 0, numberOfRegistrations);
            return registrations;
        }

        private void SetPortRegistrations(PortRegistration[] registrations)
        {
            // Create the memory-mapped file.
            using var memoryMappedFile = MemoryMappedFile.CreateOrOpen(_memoryMappedIpcFileName, _fileSize);

            // Create a random access view, from the 256th megabyte (the offset)
            // to the 768th megabyte (the offset plus length).
            using var accessor = memoryMappedFile.CreateViewAccessor();

            var numberOfRegistrations = (short)registrations.Length;
            
            accessor.Write(0, numberOfRegistrations);

            accessor.WriteArray(2, registrations, 0, numberOfRegistrations);
        }

    }
}