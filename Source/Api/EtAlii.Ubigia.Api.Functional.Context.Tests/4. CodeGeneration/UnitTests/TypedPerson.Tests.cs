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
            Assert.Null(person.FirstName);
            Assert.Null(person.LastName);
            Assert.Equal(DateTime.MinValue, person.BirthDate);
            Assert.Equal(0, person.NumberOfChildren);
            Assert.Equal(0f, person.Height);
            Assert.False(person.IsMale);
        }

        [Fact]
        public async Task TypedPerson_GraphContext_Process()
        {
            // Arrange.
            var context = (IGraphContext)null;

            // Act.
            var act = new Func<Task>(async () =>
            {
                // ReSharper disable once ExpressionIsAlwaysNull
                var result = await context
                    .ProcessTypedPerson()
                    .ConfigureAwait(false);

                Assert.IsType<TypedPerson>(result);
            });

            // Assert.
            await Assert.ThrowsAsync<NullReferenceException>(act).ConfigureAwait(false);
        }
    }
}
