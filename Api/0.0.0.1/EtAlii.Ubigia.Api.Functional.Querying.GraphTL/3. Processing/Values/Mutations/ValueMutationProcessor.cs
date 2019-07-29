namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Threading.Tasks;

    internal class ValueMutationProcessor : IValueMutationProcessor
    {
        private readonly IValueSetter _valueSetter;

        public ValueMutationProcessor(IValueSetter valueSetter)
        {
            _valueSetter = valueSetter;
        }

        public async Task Process(
            MutationFragment fragment, 
            QueryExecutionScope executionScope, 
            FragmentMetadata fragmentMetadata, 
            IObserver<Structure> fragmentOutput)
        {
            var valueMutation = (ValueMutation) fragment;

            foreach (var structure in fragmentMetadata.Parent.Items)
            {
                var value = await _valueSetter.Set(valueMutation.Name, valueMutation.Value, valueMutation.Annotation, executionScope, structure);
                structure.EditableValues.Add(value);
            }
        }
    }
}