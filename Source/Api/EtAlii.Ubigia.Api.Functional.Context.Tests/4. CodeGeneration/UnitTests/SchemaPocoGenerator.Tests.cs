namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using System.Linq;
    using System.Reflection;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Xunit;

    public class SchemaPocoGeneratorTests
    {
        [Fact]
        public void SchemaPocoGenerator_Create()
        {
            // Create the 'input' compilation that the generator will act on
            var inputCompilation = CreateCompilation(@"
            namespace TestCode
            {
                [UbigiaPoco]
                public partial class UserPocoObject
                {
                }
            }");

            // directly create an instance of the generator
            // (Note: in the compiler this is loaded from an assembly, and created via reflection at runtime)
            var generator = new SchemaPocoGenerator();

            // Create the driver that will control the generation, passing in our generator
            var driver = (GeneratorDriver)CSharpGeneratorDriver.Create(generator);

            // Run the generation pass
            // (Note: the generator driver itself is immutable, and all calls return an updated version of the driver that you should use for subsequent calls)
            driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out var diagnostics);

            // We can now assert things about the resulting compilation:
            Assert.True(diagnostics.IsEmpty); // there were no diagnostics created by the generators
            Assert.True(outputCompilation.SyntaxTrees.Count() == 2); // we have two syntax trees, the original 'user' provided one, and the one added by the generator
            var outputCompilationDiagnostics = outputCompilation.GetDiagnostics();
            Assert.NotEmpty(outputCompilationDiagnostics); // verify the compilation with the added source has no diagnostics

            // Or we can look at the results directly:
            var runResult = driver.GetRunResult();

            // The runResult contains the combined results of all generators passed to the driver
            Assert.Single(runResult.GeneratedTrees);
            Assert.Empty(runResult.Diagnostics);

            // Or you can access the individual results on a by-generator basis
            var generatorResult = runResult.Results[0];
            Assert.Equal(generator, generatorResult.Generator);
            Assert.Empty(generatorResult.Diagnostics);
            Assert.Single(generatorResult.GeneratedSources);
            Assert.Null(generatorResult.Exception);
        }

        [Fact]
        public void SchemaPocoGenerator_Create_UserPocoObject_Correct()
        {
            // Arrange.
            var inputCompilation = CreateCompilation(@"
            UserPocoObject = @nodes(Person:Stark/Tony)
            {
                FirstName @node()
                LastName @node(\#FamilyName)
            }");
            var generator = new SchemaPocoGenerator();
            var driver = (GeneratorDriver)CSharpGeneratorDriver.Create(generator);

            // Act.
            driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out var diagnostics);

            // Assert.
            Assert.True(diagnostics.IsEmpty); // there were no diagnostics created by the generators
            Assert.True(outputCompilation.SyntaxTrees.Count() == 2); // we have two syntax trees, the original 'user' provided one, and the one added by the generator
            var outputCompilationDiagnostics = outputCompilation.GetDiagnostics();
            Assert.NotEmpty(outputCompilationDiagnostics); // verify the compilation with the added source has no diagnostics
            var runResult = driver.GetRunResult();
            Assert.Single(runResult.GeneratedTrees);
            Assert.Empty(runResult.Diagnostics);
            var generatorResult = runResult.Results[0];
            Assert.Equal(generator, generatorResult.Generator);
            Assert.Empty(generatorResult.Diagnostics);
            Assert.Single(generatorResult.GeneratedSources);
            Assert.Null(generatorResult.Exception);
        }

        [Fact]
        public void SchemaPocoGenerator_Create_UserPocoObject_Incorrect_NonPartial()
        {
            // Arrange.
            var inputCompilation = CreateCompilation(@"
            UserPocoObject = @nodes(Person:Stark/Tony)
            {
                FirstName @node()
                LastName @node(\#FamilyName)
            }");
            var generator = new SchemaPocoGenerator();
            var driver = (GeneratorDriver)CSharpGeneratorDriver.Create(generator);

            // Act.
            driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var _, out var diagnostics);

            // Assert.
            var diagnostic = Assert.Single(diagnostics);
            Assert.NotNull(diagnostic);
            Assert.Equal(SchemaPocoGenerator.UbigiaPocoMustBePartialDiagnosticId, diagnostic.Id);
        }


        private Compilation CreateCompilation(string source)
        {
            var syntaxTrees = new[] {CSharpSyntaxTree.ParseText(source)};
            var references = new[]
            {
                MetadataReference.CreateFromFile(typeof(Binder).GetTypeInfo().Assembly.Location),
                MetadataReference.CreateFromFile(typeof(SchemaProcessor).GetTypeInfo().Assembly.Location)
            };
            var options = new CSharpCompilationOptions(OutputKind.ConsoleApplication);
            return CSharpCompilation.Create("compilation", syntaxTrees, references, options);
        }
    }
}
