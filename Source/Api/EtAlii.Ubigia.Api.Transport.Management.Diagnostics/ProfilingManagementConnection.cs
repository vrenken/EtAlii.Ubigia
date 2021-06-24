// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Diagnostics
{
    using System;
    using System.Threading.Tasks;

    //using EtAlii.xTechnology.Logging

    public class ProfilingManagementConnection : IManagementConnection
    {
        private readonly IManagementConnection _decoree;
        //private readonly IProfiler _profiler

        public ProfilingManagementConnection(
            IManagementConnection decoree
            //IProfiler profiler
            )
        {
            _decoree = decoree;
            //_profiler = profiler
        }

        public Storage Storage => _decoree.Storage;
        public IStorageContext Storages => _decoree.Storages;
        public IAccountContext Accounts => _decoree.Accounts;
        public ISpaceContext Spaces => _decoree.Spaces;
        public bool IsConnected => _decoree.IsConnected;

        public IStorageConnectionDetails Details => _decoree.Details;
        public IManagementConnectionConfiguration Configuration => _decoree.Configuration;

        public async Task Open()
        {
            await _decoree.Open().ConfigureAwait(false);
        }

        public async Task<IDataConnection> OpenSpace(Guid accountId, Guid spaceId)
        {
            return await _decoree.OpenSpace(accountId, spaceId).ConfigureAwait(false);
        }

        public async Task<IDataConnection> OpenSpace(Space space)
        {
            return await _decoree.OpenSpace(space).ConfigureAwait(false);
        }

        public async Task<IDataConnection> OpenSpace(string accountName, string spaceName)
        {
            return await _decoree.OpenSpace(accountName,spaceName).ConfigureAwait(false);
        }

        public async Task Close()
        {
            await _decoree.Close().ConfigureAwait(false);
        }

        #region Disposable

        private bool _disposed;

        //Implement IDisposable.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                    _decoree.Dispose();
                }
                // Free your own state (unmanaged objects).
                // Set large fields to null.
                _disposed = true;
            }
        }

        // Use C# destructor syntax for finalization code.
        ~ProfilingManagementConnection()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }

        #endregion Disposable

    }
}
