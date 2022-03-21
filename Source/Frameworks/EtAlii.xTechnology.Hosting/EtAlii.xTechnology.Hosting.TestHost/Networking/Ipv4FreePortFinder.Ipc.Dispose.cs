// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;

    public sealed partial class Ipv4FreePortFinder : IDisposable
    {
        private bool _disposed;

        ~Ipv4FreePortFinder()
        {
            // Do not re-create Dispose clean-up code here. 
            // Calling Dispose(false) is optimal in terms of 
            // readability and maintainability.
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);

            // Use SuppressFinalize in case a subclass 
            // of this type implements a finalizer.
            GC.SuppressFinalize(this);   
        }
        private void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called. 
            if(!_disposed)
            {
                // If disposing equals true, dispose all managed and unmanaged resources. 
                if(disposing)
                {
                    // Dispose managed resources.
                    _memoryMappedFile.Dispose();
                }

                // Call the appropriate methods to clean up unmanaged resources here. 
                // If disposing is false, only the following code is executed.

                // Note disposing has been done.
                _disposed = true;
            }
        }
    }
}