namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using System;
    using System.Threading.Tasks;
    using Xunit;
    using EtAlii.Ubigia.Api.Functional.Context.Tests.Model;

    public class TypedPersonTests
    {
        [Fact]
        public void TypedPerson_Create()
        {
            // Arrange.

            // Act.
            var person = new TypedPerson();

            // Assert.
            Assert.NotNull(person);
        }

        [Fact]
        public void TypedPerson_With_Properties()
        {
            // Arrange.
            var person = new TypedPerson();

            // Act.

            // Assert.
            Assert.NotNull(person);
            Assert.Equal(string.Empty, person.FirstName);
            Assert.Equal(string.Empty, person.LastName);
            Assert.True(person.BirthDate.Year > 2020);
            Assert.Equal(42, person.NumberOfChildren);
            Assert.True(person.Height > 42.41f);
            Assert.True(person.IsMale);
        }

        [Fact]
        public async Task TypedPerson_SchemaProcessor_Process()
        {
            // Arrange.
            var processor = (ISchemaProcessor)null;

            // Act.
            var act = new Func<Task>(async () =>
            {
                // ReSharper disable once ExpressionIsAlwaysNull
                var result = await processor
                    .ProcessTypedPerson()
                    .ConfigureAwait(false);

                Assert.IsType<TypedPerson>(result);
            });

            // Assert.
            await Assert.ThrowsAsync<NullReferenceException>(act).ConfigureAwait(false);
        }
    }
}
