// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Local
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class ResponseStreamTests
    {
        [Fact]
        public void ResponseStream_Create()
        {
            // Arrange.
            var onFirstWriteAsync = new Func<Task>(() => Task.Delay(TimeSpan.FromMilliseconds(100)));
            var abortRequest = new Action(() => Task.Delay(TimeSpan.FromMilliseconds(100)).Wait());
            
            // Act.
            using var stream = new ResponseStream(onFirstWriteAsync, abortRequest);

            // Assert.
            Assert.NotNull(stream);
            Assert.True(stream.CanRead);
            Assert.True(stream.CanWrite);
            Assert.False(stream.CanSeek);
        }
        
        [Fact]
        public void ResponseStream_WriteRead()
        {
            // Arrange.
            var onFirstWriteAsync = new Func<Task>(() => Task.Delay(TimeSpan.FromMilliseconds(100)));
            var abortRequest = new Action(() => Task.Delay(TimeSpan.FromMilliseconds(100)).Wait());
            using var stream = new ResponseStream(onFirstWriteAsync, abortRequest);
            var writeData = new byte[] {0, 1, 2, 3, 4, 5};
            var readData = new byte[6];
            
            // Act.
            stream.Write(writeData);
            stream.Flush();
            stream.Read(readData,0, readData.Length);

            // Assert.
            Assert.True(readData.SequenceEqual(writeData));
        }
    }
}
