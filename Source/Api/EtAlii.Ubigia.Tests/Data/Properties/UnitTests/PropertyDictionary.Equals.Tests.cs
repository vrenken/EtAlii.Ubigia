namespace EtAlii.Ubigia.Tests
{
    using System;
    using Xunit;

    public class PropertyDictionaryEquals2Tests
    {
        [Fact]
        public void PropertyDictionary_Equals_Same()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();

            first["Name"] = "John";
            first["Birthdate"] = new DateTime(1978, 07, 29);

            second["Name"] = "John";
            second["Birthdate"] = new DateTime(1978, 07, 29);

            // Act.
            var result1 = first.Equals(second);
            var result2 = second.Equals(first);

            // Assert.
            Assert.True(result1);
            Assert.True(result1 == result2, "result2");
        }

        [Fact]
        public void PropertyDictionary_Equals_Different_Name()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();

            first["Name"] = "John";
            first["Birthdate"] = new DateTime(1978, 07, 29);

            second["Name"] = "Jane";
            second["Birthdate"] = new DateTime(1978, 07, 29);

            // Act.
            var result1 = first.Equals(second);
            var result2 = second.Equals(first);

            // Assert.
            Assert.False(result1);
            Assert.False(result2);
        }

        [Fact]
        public void PropertyDictionary_Equals_Same_With_PropertyDictionaryComparer()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();

            first["Name"] = "John";
            first["Birthdate"] = new DateTime(1978, 07, 29);

            second["Name"] = "John";
            second["Birthdate"] = new DateTime(1978, 07, 29);

            // Act.
            var areEqual = new PropertyDictionaryComparer().AreEqual(first, second);

            // Assert.
            Assert.True(areEqual);
        }

        [Fact]
        public void PropertyDictionary_Equals_Different_Name_With_PropertyDictionaryComparer()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();

            first["Name"] = "John";
            first["Birthdate"] = new DateTime(1978, 07, 29);

            second["Name"] = "Jane";
            second["Birthdate"] = new DateTime(1978, 07, 29);

            // Act.
            var areEqual = new PropertyDictionaryComparer().AreEqual(first, second);

            // Assert.
            Assert.False(areEqual);
        }

        [Fact]
        public void PropertyDictionary_Equals_Different_Value()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();

            first["Name"] = "John";
            second["Birthdate"] = new DateTime(1978, 07, 29);

            // Act.
            var result1 = first.Equals(second);
            var result2 = second.Equals(first);

            // Assert.
            Assert.False(result1);
            Assert.False(result2);
        }

        [Fact]
        public void PropertyDictionary_Equals_Different_Birthdate_Future()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Name"] = "John";
            first["Birthdate"] = new DateTime(1978, 07, 29);

            second["Name"] = "John";
            second["Birthdate"] = new DateTime(1981, 03, 12);

            // Act.
            var result1 = first.Equals(second);
            var result2 = second.Equals(first);

            // Assert.
            Assert.False(result1);
            Assert.False(result2);
        }

        [Fact]
        public void PropertyDictionary_Equals_Different_Birthdate_Past()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();

            first["Name"] = "John";
            first["Birthdate"] = new DateTime(1978, 07, 29);

            second["Name"] = "John";
            second["Birthdate"] = new DateTime(1974, 05, 21);

            // Act.
            var result1 = first.Equals(second);
            var result2 = second.Equals(first);

            // Assert.
            Assert.False(result1);
            Assert.False(result2);
        }

        [Fact]
        public void PropertyDictionary_Equals_With_Different_Typed_Values_Int_And_Float()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Key"] = 20;
            second["Key"] = 20.0f;

            // Act.
            var result1 = first.Equals(second);
            var result2 = second.Equals(first);

            // Assert.
            Assert.False(result1);
            Assert.False(result2);
        }

        [Fact]
        public void PropertyDictionary_Equals_With_Different_Typed_Values_Float_And_Int()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Key"] = 20.0f;
            second["Key"] = 20;

            // Act.
            var result1 = first.Equals(second);
            var result2 = second.Equals(first);

            // Assert.
            Assert.False(result1);
            Assert.False(result2);
        }

        [Fact]
        public void PropertyDictionary_Equals_With_Different_Typed_Values_Int_And_String()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Key"] = 20;
            second["Key"] = "20";

            // Act.
            var result1 = first.Equals(second);
            var result2 = second.Equals(first);

            // Assert.
            Assert.False(result2);
            Assert.False(result1);
        }

        [Fact]
        public void PropertyDictionary_Equals_With_Different_Typed_Values_String_And_Int()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Key"] = "20";
            second["Key"] = 20;

            // Act.
            var result1 = first.Equals(second);
            var result2 = second.Equals(first);

            // Assert.
            Assert.False(result2);
            Assert.False(result1);
        }

        [Fact]
        public void PropertyDictionary_Equals_With_Different_Typed_Values_Int_And_Null()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Key"] = 20;
            second["Key"] = null;

            // Act.
            var result1 = first.Equals(second);
            var result2 = second.Equals(first);

            // Assert.
            Assert.False(result1);
            Assert.False(result2);
        }


        [Fact]
        public void PropertyDictionary_Equals_With_Different_Typed_Values_Null_And_Int()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Key"] = null;
            second["Key"] = 20;

            // Act.
            var result1 = first.Equals(second);
            var result2 = second.Equals(first);

            // Assert.
            Assert.False(result1);
            Assert.False(result2);
        }

        [Fact]
        public void PropertyDictionary_Equals_With_Different_Typed_Values_String_And_Null()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Key"] = "20";
            second["Key"] = null;

            // Act.
            var result1 = first.Equals(second);
            var result2 = second.Equals(first);

            // Assert.
            Assert.False(result1);
            Assert.False(result2);
        }

        [Fact]
        public void PropertyDictionary_Equals_With_Different_Typed_Values_Null_And_String()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Key"] = null;
            second["Key"] = "20";

            // Act.
            var result1 = first.Equals(second);
            var result2 = second.Equals(first);

            // Assert.
            Assert.False(result1);
            Assert.False(result2);
        }

        [Fact]
        public void PropertyDictionary_Equals_Different_Null()
        {
            // Arrange.
            var first = new PropertyDictionary();
            first["Name"] = "John";
            first["Birthdate"] = new DateTime(1978, 07, 29);

            // Act.
            var result = first.Equals(null);

            // Assert.
            Assert.False(result);
        }

        [Fact]
        public void PropertyDictionary_Equals_Different_DateTime_MilliSeconds()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Name"] = "John";
            first["Birthdate"] = new DateTime(1978, 07, 29, 12, 13, 05, 29);

            second["Name"] = "John";
            second["Birthdate"] = new DateTime(1978, 07, 29, 12, 13, 05, 30);

            // Act.
            var result = first.Equals(second);

            // Assert.
            Assert.False(result);
        }

        [Fact]
        public void PropertyDictionary_Equals_Different_DateTime_Seconds()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Name"] = "John";
            first["Birthdate"] = new DateTime(1978, 07, 29, 12, 13, 05);

            second["Name"] = "John";
            second["Birthdate"] = new DateTime(1978, 07, 29, 12, 13, 06);

            // Act.
            var result = first.Equals(second);

            // Assert.
            Assert.False(result);
        }

        [Fact]
        public void PropertyDictionary_Equals_Different_DateTime_Utc_Local()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Name"] = "John";
            first["Birthdate"] = new DateTime(1978, 07, 29, 12, 13, 05, DateTimeKind.Utc);

            second["Name"] = "John";
            second["Birthdate"] = new DateTime(1978, 07, 29, 12, 13, 05, DateTimeKind.Local);

            // Act.
            var result = first.Equals(second);

            // Assert.
            Assert.True(result);
            // Unexpected i know, but it's true: http://stackoverflow.com/questions/28602941/datetime-compare-ignores-kind
        }
    }
}
