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
            ValueFragment fragment,
            FragmentMetadata fragmentMetadata,
            SchemaExecutionScope executionScope, 
            IObserver<Structure> fragmentOutput)
        {
            foreach (var structure in fragmentMetadata.Parent.Items)
            {
                var value = await _valueSetter.Set(fragment.Name, fragment.Mutation, fragment.Annotation, executionScope, structure);
                structure.EditableValues.Add(value);
            }
        }
    }
}