namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;
    using global::GraphQL.Language.AST;
    using global::GraphQL.Resolvers;
    using global::GraphQL.Types;

    public class FieldTypeBuilder : IFieldTypeBuilder
    {
        
        public FieldType Build(PropertyDictionary properties, string path, Directive directive, out DynamicObjectGraphType fieldTypeInstance)
        {
            var fieldTypeInstanceType = BuildInstanceType();
            fieldTypeInstance = (DynamicObjectGraphType)Activator.CreateInstance(fieldTypeInstanceType);
            DynamicObjectGraphType.Setup(fieldTypeInstance, "person", path, properties);

            var instance = fieldTypeInstance;
            var fieldType = new FieldType()
            {
                Name = fieldTypeInstance.Name,
                Description = $"Person field created for the Ubigia path: {path}",
                Type = fieldTypeInstanceType,
                Arguments = null,
                Resolver = new FuncFieldResolver<object, object>(_ => instance)
            };
            return fieldType;
        }

        
        private readonly Type _baseType = typeof(DynamicObjectGraphType);
        private TypeInfo BuildInstanceType()
        {
            var assemblyName = new AssemblyName($"DynamicAssembly_{Guid.NewGuid():N}");
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule("DynamicModule");
            var typeName = $"{_baseType.Name}_{Guid.NewGuid():N}";
            var typeBuilder = moduleBuilder.DefineType(typeName, TypeAttributes.Public, _baseType);
            return typeBuilder.CreateTypeInfo();
        }
    }
}
