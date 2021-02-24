namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Threading.Tasks;

    internal class QueryValueProcessor : IQueryValueProcessor
    {
        private readonly IValueGetter _valueGetter;

        public QueryValueProcessor(IValueGetter valueGetter)
        {
            _valueGetter = valueGetter;
        }

        public async Task Process(
            ValueFragment fragment,
            FragmentMetadata fragmentMetadata,
            SchemaExecutionScope executionScope)
        {
            foreach (var structure in fragmentMetadata.Parent.Items)
            {
                var value = await _valueGetter.Get(fragment.Name, fragment.Annotation, executionScope, structure).ConfigureAwait(false);
                if(value != null)
                {
                    structure.EditableValues.Add(value);
                }
            }
        }
    }
}
