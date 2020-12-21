namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class PathSubjectToGraphPathConverter : IPathSubjectToGraphPathConverter
    {
        private readonly IPathSubjectPartToGraphPathPartConverterSelector _pathSubjectPartConverterSelector;

        public PathSubjectToGraphPathConverter(IPathSubjectPartToGraphPathPartConverterSelector pathSubjectPartConverterSelector)
        {
            _pathSubjectPartConverterSelector = pathSubjectPartConverterSelector;
        }

        public async Task<GraphPath> Convert(PathSubject pathSubject, ExecutionScope scope)
        {
            var builder = new GraphPathBuilder();

            //var result = new List<GraphPathPart>()

            for (var i = 0; i < pathSubject.Parts.Length; i++)
            {
                var part = pathSubject.Parts[i];
                var previousPart = i > 0 ? pathSubject.Parts[i - 1] : null;
                var nextPart = i < pathSubject.Parts.Length - 1 ? pathSubject.Parts[i + 1] : null;

                var converter = _pathSubjectPartConverterSelector.Select(part);
                var graphPathParts = await converter.Convert(part, i, previousPart, nextPart, scope).ConfigureAwait(false);

                builder.AddRange(graphPathParts);
            }

            return builder.ToPath();
        }
    }
}
