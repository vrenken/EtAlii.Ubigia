namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Threading.Tasks;

    internal class QueryValueProcessor : IQueryValueProcessor
    {
        private readonly IValueGetter _valueGetter;

        public QueryValueProcessor(IValueGetter valueGetter)
        {
            _valueGetter = valueGetter;
        }

        public async Task Process(
            ValueQuery fragment,
            FragmentMetadata fragmentMetadata,
            QueryExecutionScope executionScope, 
            IObserver<Structure> fragmentOutput)
        {
            foreach (var structure in fragmentMetadata.Parent.Items)
            {
                var value = await _valueGetter.Get(fragment.Name, fragment.Annotation, executionScope, structure);
                if(value != null)
                {
                    structure.EditableValues.Add(value);
                }
            }
        }
    }
}