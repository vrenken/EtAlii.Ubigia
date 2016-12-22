namespace EtAlii.Servus.Api.Diagnostics.Profiling
{
    using System.IO;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Logical;

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

        public async Task Upload(Stream localDataStream, ulong sizeInBytes, Identifier identifier)
        {
            dynamic profile = _profiler.Begin("Upload");
            profile.SizeInBytes = sizeInBytes;

            await _decoree.Upload(localDataStream, sizeInBytes, identifier);

            _profiler.End(profile);
        }

        public async Task Download(Stream localDataStream, Identifier identifier, bool validateChecksum = false)
        {
            dynamic profile = _profiler.Begin("Download");
            profile.ValidateChecksum = validateChecksum;

            await _decoree.Download(localDataStream, identifier, validateChecksum);

            _profiler.End(profile);
        }

        public async Task<bool> HasContent(Identifier identifier)
        {
            var profile = _profiler.Begin("HasContent");

            var result = await _decoree.HasContent(identifier);

            _profiler.End(profile);

            return result;
        }
    }
}
