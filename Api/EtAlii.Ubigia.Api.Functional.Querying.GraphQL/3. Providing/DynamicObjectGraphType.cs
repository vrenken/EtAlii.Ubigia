namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using GraphQL.Types;

    public class DynamicObjectGraphType : ObjectGraphType<object>
    {
        private static readonly ModuleBuilder ModuleBuilder = CreateModuleBuilder(); 

        private static ModuleBuilder CreateModuleBuilder()
        {
            var assemblyName = new AssemblyName($"DynamicAssembly_{Guid.NewGuid():N}");
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            return assemblyBuilder.DefineDynamicModule($"DynamicModule_{nameof(DynamicObjectGraphType)}");
        }

        private static TypeInfo BuildInstanceType()
        {
            var typeName = $"{typeof(DynamicObjectGraphType).Name}_{Guid.NewGuid():N}";
            var typeBuilder = ModuleBuilder.DefineType(typeName, TypeAttributes.Public, typeof(DynamicObjectGraphType));
            return typeBuilder.CreateTypeInfo();
        }
        
 
        public static DynamicObjectGraphType[] Create(string path, string name, PropertyDictionary[] propertiesCollection)
        {
            return propertiesCollection
                .Select(properties => Create(path, name, properties))
                .ToArray();
        }

        public static DynamicObjectGraphType Create(string path, string name, PropertyDictionary properties)
        {
            var fieldTypeInstanceType = BuildInstanceType();
            
            var result = (DynamicObjectGraphType)Activator.CreateInstance(fieldTypeInstanceType);

            result.Name = name;
            result.Description = $"Dynamic {name} type created for the Ubigia path: {path}";
            AddFields(result, properties);
            
            return result;
        }

        public static ObjectGraphType CreateShallow(string path, string name, PropertyDictionary properties)
        {
            var result = new ObjectGraphType
            {
                Name = name, 
                Description = $"Shallow dynamic {name} type created for the Ubigia path: {path}"
            };

            foreach (var kvp in properties)
            {
                result.Field(kvp.Key, GraphTypeConverter.ToScalarGraphType(kvp.Value));
            }

            return result;
        }

        public static void AddFields(ComplexGraphType<object> instance, PropertyDictionary properties)
        {
            foreach (var kvp in properties)
            {
                var propertyName = kvp.Key.ToLower();
                var value = kvp.Value;
                var fieldType = new FieldType()
                {
                    Name = propertyName,
                    Description = $"Dynamic field {propertyName}",
                    DeprecationReason = null,
                    Type = GraphTypeConverter.ToGraphType(value),
                    Arguments = null,
                    Resolver = new InstanceFieldResolver(value)// GetResolver(value)
                };
                    
                instance.AddField(fieldType);
            }
        }
    }
}