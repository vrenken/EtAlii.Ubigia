namespace EtAlii.Ubigia.Provisioning.UnitTests
{
    using System.Linq;
    using Xunit;

    public class PersonModelBuilder_Tests
    {
        [Fact]
        public void PersonModelBuilder_Create()
        {
            // Arrange.

            // Act.
            var builder = new PersonModelBuilder();

            // Assert.
            Assert.NotNull(builder);
        }

        [Fact]
        public void PersonModelBuilder_Add_Empty()
        {
            // Arrange.
            var builder = new PersonModelBuilder();

            // Act.
            builder.Add("Last", "First", "first.last@nomail.com", "+316343434");

            // Assert.
        }

        [Fact]
        public void PersonModelBuilder_ToModel_Empty()
        {
            // Arrange.
            var builder = new PersonModelBuilder();
            LastName[] lastNames;
            Email[] emails;
            Phone[] phones;
            Photo[] photos;

            // Act.
            builder.ToModel(out lastNames, out emails, out phones, out photos);

            // Assert.
            Assert.NotNull(lastNames);
            Assert.Equal(0, lastNames.Length);
            Assert.NotNull(emails);
            Assert.Equal(0, emails.Length);
            Assert.NotNull(phones);
            Assert.Equal(0, phones.Length);
            Assert.NotNull(photos);
            Assert.Equal(0, photos.Length);
        }


        [Fact]
        public void PersonModelBuilder_Add_Single()
        {
            // Arrange.
            var builder = new PersonModelBuilder();
            LastName[] lastNames;
            Email[] emails;
            Phone[] phones;
            Photo[] photos;

            // Act.
            builder.Add("Last", "First", "first.last@nomail.com", "+316343434");
            builder.ToModel(out lastNames, out emails, out phones, out photos);

            // Assert.
            Assert.NotNull(lastNames);
            Assert.Equal(1, lastNames.Length);
            Assert.Equal(1, lastNames.SelectMany(ln => ln.FirstNames).Count());
            Assert.NotNull(emails);
            Assert.Equal(1, emails.Length);
            Assert.NotNull(phones);
            Assert.Equal(1, phones.Length);
            Assert.NotNull(photos);
            Assert.Equal(0, photos.Length);
        }

        [Fact]
        public void PersonModelBuilder_Add_Two_Same_LastName()
        {
            // Arrange.
            var builder = new PersonModelBuilder();
            LastName[] lastNames;
            Email[] emails;
            Phone[] phones;
            Photo[] photos;

            // Act.
            builder.Add("Last", "First1", "first1.last@nomail.com", "+316343434");
            builder.Add("Last", "First2", "first2.last@nomail.com", "+316343435");
            builder.ToModel(out lastNames, out emails, out phones, out photos);

            // Assert.
            Assert.NotNull(lastNames);
            Assert.Equal(1, lastNames.Length);
            Assert.NotNull(emails);
            Assert.Equal(2, emails.Length);
            Assert.NotNull(phones);
            Assert.Equal(2, phones.Length);
            Assert.NotNull(photos);
            Assert.Equal(0, photos.Length);
        }


        [Fact]
        public void PersonModelBuilder_Add_Two_Different_LastName_01()
        {
            // Arrange.
            var builder = new PersonModelBuilder();
            LastName[] lastNames;
            Email[] emails;
            Phone[] phones;
            Photo[] photos;

            // Act.
            builder.Add("Last1", "First", "first1.last@nomail.com", "+316343434");
            builder.Add("Last2", "First", "first2.last@nomail.com", "+316343435");
            builder.ToModel(out lastNames, out emails, out phones, out photos);

            // Assert.
            Assert.NotNull(lastNames);
            Assert.Equal(2, lastNames.Length);
            Assert.NotNull(emails);
            Assert.Equal(2, emails.Length);
            Assert.NotNull(phones);
            Assert.Equal(2, phones.Length);
            Assert.NotNull(photos);
            Assert.Equal(0, photos.Length);
        }


        [Fact]
        public void PersonModelBuilder_Add_Two_Different_LastName_02()
        {
            // Arrange.
            var builder = new PersonModelBuilder();
            LastName[] lastNames;
            Email[] emails;
            Phone[] phones;
            Photo[] photos;

            // Act.
            builder.Add("Last1", "First1", "first1.last@nomail.com", "+316343434");
            builder.Add("Last2", "First2", "first2.last@nomail.com", "+316343435");
            builder.ToModel(out lastNames, out emails, out phones, out photos);

            // Assert.
            Assert.NotNull(lastNames);
            Assert.Equal(2, lastNames.Length);
            Assert.Equal(2, lastNames.SelectMany(ln => ln.FirstNames).Count());
            Assert.NotNull(emails);
            Assert.Equal(2, emails.Length);
            Assert.NotNull(phones);
            Assert.Equal(2, phones.Length);
            Assert.NotNull(photos);
            Assert.Equal(0, photos.Length);
        }

        [Fact]
        public void PersonModelBuilder_Add_Two_Same_LastName_And_FirstName_Different_Phone()
        {
            // Arrange.
            var builder = new PersonModelBuilder();
            LastName[] lastNames;
            Email[] emails;
            Phone[] phones;
            Photo[] photos;

            // Act.
            builder.Add("Last", "First", "first.last@nomail.com", "+316343434");
            builder.Add("Last", "First", "first.last@nomail.com", "+316343435");
            builder.ToModel(out lastNames, out emails, out phones, out photos);

            // Assert.
            Assert.NotNull(lastNames);
            Assert.Equal(1, lastNames.Length);
            Assert.NotNull(emails);
            Assert.Equal(1, emails.Length);
            Assert.NotNull(phones);
            Assert.Equal(2, phones.Length);
            Assert.NotNull(photos);
            Assert.Equal(0, photos.Length);
        }

        [Fact]
        public void PersonModelBuilder_Add_Two_Same_LastName_And_FirstName_Different_Email()
        {
            // Arrange.
            var builder = new PersonModelBuilder();
            LastName[] lastNames;
            Email[] emails;
            Phone[] phones;
            Photo[] photos;

            // Act.
            builder.Add("Last", "First", "first1.last@nomail.com", "+316343434");
            builder.Add("Last", "First", "first2.last@nomail.com", "+316343434");
            builder.ToModel(out lastNames, out emails, out phones, out photos);

            // Assert.
            Assert.NotNull(lastNames);
            Assert.Equal(1, lastNames.Length);
            Assert.NotNull(emails);
            Assert.Equal(2, emails.Length);
            Assert.NotNull(phones);
            Assert.Equal(1, phones.Length);
            Assert.NotNull(photos);
            Assert.Equal(0, photos.Length);
        }

        [Fact(Skip = "Not working yet")]
        public void PersonModelBuilder_Add_Two_Different_LastNames_Different_Emails()
        {
            // Arrange.
            var builder = new PersonModelBuilder();
            LastName[] lastNames;
            Email[] emails;
            Phone[] phones;
            Photo[] photos;

            // Act.
            builder.Add("Last", "First", "first1.last@nomail.com", "+316343434");
            builder.Add("Last2", "First", "first2.last@nomail.com", "+316343434");
            builder.ToModel(out lastNames, out emails, out phones, out photos);

            // Assert.
            Assert.NotNull(lastNames);
            Assert.Equal(1, lastNames.Length);
            Assert.NotNull(emails);
            Assert.Equal(2, emails.Length);
            Assert.NotNull(phones);
            Assert.Equal(1, phones.Length);
            Assert.NotNull(photos);
            Assert.Equal(0, photos.Length);
        }

        [Fact(Skip = "Not working yet")]
        public void PersonModelBuilder_Add_Two_Different_LastNames_Different_Phones()
        {
            // Arrange.
            var builder = new PersonModelBuilder();
            LastName[] lastNames;
            Email[] emails;
            Phone[] phones;
            Photo[] photos;

            // Act.
            builder.Add("Last", "First", "first.last@nomail.com", "+316343434");
            builder.Add("Last2", "First", "first.last@nomail.com", "+316343435");
            builder.ToModel(out lastNames, out emails, out phones, out photos);

            // Assert.
            Assert.NotNull(lastNames);
            Assert.Equal(1, lastNames.Length);
            Assert.NotNull(emails);
            Assert.Equal(1, emails.Length);
            Assert.NotNull(phones);
            Assert.Equal(2, phones.Length);
            Assert.NotNull(photos);
            Assert.Equal(0, photos.Length);
        }

        [Fact(Skip = "Not working yet")]
        public void PersonModelBuilder_Add_Two_Different_LastNames_Different_Phones_And_Emails()
        {
            // Arrange.
            var builder = new PersonModelBuilder();
            LastName[] lastNames;
            Email[] emails;
            Phone[] phones;
            Photo[] photos;

            // Act.
            builder.Add("Last", "First", "first.last@nomail.com", "+316343434");
            builder.Add("Last2", "First", "first2.last@nomail.com", "+316343435");
            builder.ToModel(out lastNames, out emails, out phones, out photos);

            // Assert.
            Assert.NotNull(lastNames);
            Assert.Equal(1, lastNames.Length);
            Assert.NotNull(emails);
            Assert.Equal(2, emails.Length);
            Assert.NotNull(phones);
            Assert.Equal(2, phones.Length);
            Assert.NotNull(photos);
            Assert.Equal(0, photos.Length);
        }
    }
}
