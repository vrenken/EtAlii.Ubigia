namespace EtAlii.Ubigia.Api.Functional.Context
{
    /// <inheritdoc />
    internal class PropertiesValueGetter : IPropertiesValueGetter
    {
        /// <inheritdoc />
        public Value Get(string valueName, Structure structure)
        {
            var properties = structure.Node.GetProperties();
            return properties.TryGetValue(valueName, out var value)
                ? new Value(valueName, value)
                : null;
        }
    }
}
