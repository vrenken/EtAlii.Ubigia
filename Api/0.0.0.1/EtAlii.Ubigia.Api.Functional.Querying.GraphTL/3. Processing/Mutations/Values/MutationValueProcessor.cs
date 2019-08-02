namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Threading.Tasks;

    internal class MutationValueProcessor : IMutationValueProcessor
    {
        private readonly IValueSetter _valueSetter;

        public MutationValueProcessor(IValueSetter valueSetter)
        {
            _valueSetter = valueSetter;
        }

        public async Task Process(
            ValueMutation fragment,
            FragmentMetadata fragmentMetadata,
            QueryExecutionScope executionScope, 
            IObserver<Structure> fragmentOutput)
        {
            foreach (var structure in fragmentMetadata.Parent.Items)
            {
                var value = await _valueSetter.Set(fragment.Name, fragment.Value, fragment.Annotation, executionScope, structure);
                structure.EditableValues.Add(value);
            }
        }
    }
}