/* Copyright 2012 Marco Minerva, marco.minerva@gmail.com 
 
   Licensed under the Apache License, Version 2.0 (the "License"); 
   you may not use this file except in compliance with the License. 
   You may obtain a copy of the License at 
 
       http://www.apache.org/licenses/LICENSE-2.0 
 
   Unless required by applicable law or agreed to in writing, software 
   distributed under the License is distributed on an "AS IS" BASIS, 
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
   See the License for the specific language governing permissions and 
   limitations under the License. 
*/



// ReSharper disable all

namespace EtAlii.Ubigia.Windows.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO.MemoryMappedFiles;
    using System.Threading;

    public class MemoryMappedFileCommunicator : IDisposable
    {
        #region Constants

        private const int DATA_AVAILABLE_OFFSET = 0;
        private const int READ_CONFIRM_OFFSET = DATA_AVAILABLE_OFFSET + 1;
        private const int DATA_LENGTH_OFFSET = READ_CONFIRM_OFFSET + 1;
        private const int DATA_OFFSET = DATA_LENGTH_OFFSET + 10;

        #endregion

        #region Properties

        public MemoryMappedFile MappedFile { get; set; }
        public event EventHandler<MemoryMappedDataReceivedEventArgs> DataReceived;

        public int ReadPosition { get; set; }

        private int writePosition;
        public int WritePosition
        {
            get { return writePosition; }
            set
            {
                if (value != writePosition)
                {
                    writePosition = value;
                    _view.Write(WritePosition + READ_CONFIRM_OFFSET, true);
                }
            }
        }

        #endregion

        private MemoryMappedViewAccessor _view;
        private readonly AsyncOperation _operation;
        private readonly SendOrPostCallback _callback;
        private bool _started;
        private bool _disposed;

        private readonly List<byte[]> _dataToSend;
        private bool _writerThreadRunning;

        public MemoryMappedFileCommunicator(string mapName, long capacity)
            : this(MemoryMappedFile.CreateOrOpen(mapName, capacity), 0, 0, MemoryMappedFileAccess.ReadWrite)
        { }

        public MemoryMappedFileCommunicator(string mapName, long capacity, long offset, long size)
            : this(MemoryMappedFile.CreateOrOpen(mapName, capacity), offset, size, MemoryMappedFileAccess.ReadWrite)
        { }

        public MemoryMappedFileCommunicator(string mapName, long capacity, long offset, long size, MemoryMappedFileAccess access)
            : this(MemoryMappedFile.CreateOrOpen(mapName, capacity), offset, size, access)
        { }

        public MemoryMappedFileCommunicator(MemoryMappedFile mappedFile)
            : this(mappedFile, 0, 0, MemoryMappedFileAccess.ReadWrite)
        { }

        public MemoryMappedFileCommunicator(MemoryMappedFile mappedFile, long offset, long size)
            : this(mappedFile, offset, size, MemoryMappedFileAccess.ReadWrite)
        { }

        public MemoryMappedFileCommunicator(MemoryMappedFile mappedFile, long offset, long size, MemoryMappedFileAccess access)
        {
            MappedFile = mappedFile;
            _view = mappedFile.CreateViewAccessor(offset, size, access);

            ReadPosition = -1;
            writePosition = -1;
            _dataToSend = new List<byte[]>();

            _callback = new SendOrPostCallback(OnDataReceivedInternal);
            _operation = AsyncOperationManager.CreateOperation(null);
        }

        public void StartReader()
        {
            if (_started)
                return;

            if (ReadPosition < 0 || writePosition < 0)
                throw new ArgumentException("Unable to start reading: read or write position < 0");

            Thread t = new Thread(ReaderThread);
            t.IsBackground = true;
            t.Start();
            _started = true;
        }

        public void Write(string message)
        {
            var data = System.Text.Encoding.UTF8.GetBytes(message);
            Write(data);
        }

        public void Write(byte[] data)
        {
            if (ReadPosition < 0 || writePosition < 0)
                throw new ArgumentException("Unable to write: read or write position < 0");

            lock (_dataToSend)
                _dataToSend.Add(data);

            if (!_writerThreadRunning)
            {
                _writerThreadRunning = true;
                var writerThread = new Thread(WriterThread);
                writerThread.IsBackground = true;
                writerThread.Start();
            }
        }

        public void WriterThread(object stateInfo)
        {
            while (_dataToSend.Count > 0 && !_disposed)
            {
                byte[] data = null;
                lock (_dataToSend)
                {
                    data = _dataToSend[0];
                    _dataToSend.RemoveAt(0);
                }

                while (!_view.ReadBoolean(WritePosition + READ_CONFIRM_OFFSET))
                    Thread.Sleep(500);

                // Sets length and write data. 
                _view.Write(writePosition + DATA_LENGTH_OFFSET, data.Length);
                _view.WriteArray(writePosition + DATA_OFFSET, data, 0, data.Length);

                // Resets the flag used to signal that data has been read. 
                _view.Write(writePosition + READ_CONFIRM_OFFSET, false);
                // Sets the flag used to signal that there are data avaibla. 
                _view.Write(writePosition + DATA_AVAILABLE_OFFSET, true);
            }

            _writerThreadRunning = false;
        }

        public void CloseReader()
        {
            _started = false;
        }

        private void ReaderThread(object stateInfo)
        {
            while (_started)
            {
                // Checks if there is something to read. 
                var dataAvailable = _view.ReadBoolean(ReadPosition + DATA_AVAILABLE_OFFSET);
                if (dataAvailable)
                {
                    // Checks how many bytes to read. 
                    int availableBytes = _view.ReadInt32(ReadPosition + DATA_LENGTH_OFFSET);
                    var bytes = new byte[availableBytes];
                    // Reads the byte array. 
                    int read = _view.ReadArray(ReadPosition + DATA_OFFSET, bytes, 0, availableBytes);

                    // Sets the flag used to signal that there aren't available data anymore. 
                    _view.Write(ReadPosition + DATA_AVAILABLE_OFFSET, false);
                    // Sets the flag used to signal that data has been read.  
                    _view.Write(ReadPosition + READ_CONFIRM_OFFSET, true);

                    MemoryMappedDataReceivedEventArgs args = new MemoryMappedDataReceivedEventArgs(bytes, read);
                    _operation.Post(_callback, args);
                }

                Thread.Sleep(500);
            }
        }

        private void OnDataReceivedInternal(object state)
        {
            OnDataReceived(state as MemoryMappedDataReceivedEventArgs);
        }

        protected virtual void OnDataReceived(MemoryMappedDataReceivedEventArgs e)
        {
            if (e != null && DataReceived != null)
                DataReceived(this, e);
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _started = false;
            if (_view != null)
            {
                try
                {
                    _view.Dispose();
                    _view = null;
                }
                catch
                {
                    // TODO: [TO_REACTIVEUI] Rewrite this tool to ReactiveUI. This should make these kind of patterns easier to handle.
                }
            }

            if (MappedFile != null)
            {
                try
                {
                    MappedFile.Dispose();
                    MappedFile = null;
                }
                catch
                {
                    // TODO: [TO_REACTIVEUI] Rewrite this tool to ReactiveUI. This should make these kind of patterns easier to handle.
                }
            }

            _disposed = true;
        }

        ~MemoryMappedFileCommunicator()
        {
            Dispose(false);
        }

        #endregion
    }
}