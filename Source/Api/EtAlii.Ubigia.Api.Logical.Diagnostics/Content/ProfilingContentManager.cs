// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using System.IO;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Diagnostics.Profiling;

    public class ProfilingContentManager : IContentManager
    {
        private readonly IContentManager _decoree;
        private readonly IProfiler _profiler;

        public ProfilingContentManager(
            IContentManager decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Logical.Content);
        }

        public async Task Upload(Stream stream, ulong sizeInBytes, Identifier identifier)
        {
            dynamic profile = _profiler.Begin("Upload");
            profile.SizeInBytes = sizeInBytes;

            await _decoree.Upload(stream, sizeInBytes, identifier).ConfigureAwait(false);

            _profiler.End(profile);
        }

        public async Task Download(Stream stream, Identifier identifier, bool validateChecksum = false)
        {
            dynamic profile = _profiler.Begin("Download");
            profile.ValidateChecksum = validateChecksum;

            await _decoree.Download(stream, identifier, validateChecksum).ConfigureAwait(false);

            _profiler.End(profile);
        }

        public async Task<bool> HasContent(Identifier identifier)
        {
            var profile = _profiler.Begin("HasContent");

            var result = await _decoree.HasContent(identifier).ConfigureAwait(false);

            _profiler.End(profile);

            return result;
        }
    }
}
