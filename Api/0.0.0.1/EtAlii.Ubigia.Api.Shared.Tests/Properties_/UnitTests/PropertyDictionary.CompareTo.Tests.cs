namespace EtAlii.Ubigia.Api.Tests.UnitTests
{
    using System;
    using Xunit;

    public class PropertyDictionary_CompareTo_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_CompareTo_Same()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();

            first["Name"] = "John";
            first["Birthdate"] = new DateTime(1978, 07, 29);

            second["Name"] = "John";
            second["Birthdate"] = new DateTime(1978, 07, 29);

            // Act.
            var result1 = first.CompareTo(second);
            var result2 = second.CompareTo(first);

            // Assert.
            Assert.Equal(0, result1);//, "result1");
            Assert.True(result1 == result2, "result2");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_CompareTo_Different_Name()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();

            first["Name"] = "John";
            first["Birthdate"] = new DateTime(1978, 07, 29);

            second["Name"] = "Jane";
            second["Birthdate"] = new DateTime(1978, 07, 29);

            // Act.
            var result1 = first.CompareTo(second);
            var result2 = second.CompareTo(first);

            // Assert.
            Assert.Equal(+1, result1);//, "result1");
            Assert.Equal(-1, result2);//, "result2");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_CompareTo_Different_Value()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();

            first["Name"] = "John";
            second["Birthdate"] = new DateTime(1978, 07, 29);

            // Act.
            var result1 = first.CompareTo(second);
            var result2 = second.CompareTo(first);

            // Assert.
            Assert.Equal(+1, result1);//, "result1");
            Assert.Equal(-1, result2);//, "result2");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_CompareTo_Different_Birthdate_Future()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Name"] = "John";
            first["Birthdate"] = new DateTime(1978, 07, 29);

            second["Name"] = "John";
            second["Birthdate"] = new DateTime(1981, 03, 12);

            // Act.
            var result1 = first.CompareTo(second);
            var result2 = second.CompareTo(first);

            // Assert.
            Assert.Equal(-1, result1);//, "result1");
            Assert.Equal(+1, result2);//, "result2");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_CompareTo_Different_Birthdate_Past()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();

            first["Name"] = "John";
            first["Birthdate"] = new DateTime(1978, 07, 29);

            second["Name"] = "John";
            second["Birthdate"] = new DateTime(1974, 05, 21);

            // Act.
            var result1 = first.CompareTo(second);
            var result2 = second.CompareTo(first);

            // Assert.
            Assert.Equal(+1, result1);//, "result1");
            Assert.Equal(-1, result2);//, "result2");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_CompareTo_With_Different_Typed_Values_Int_And_Float()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Key"] = 20;
            second["Key"] = 20.0f;

            // Act.
            var result1 = first.CompareTo(second);
            var result2 = second.CompareTo(first);

            // Assert.
            Assert.Equal(-1, result1);//, "result1");
            Assert.Equal(+1, result2);//, "result2");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_CompareTo_With_Different_Typed_Values_Float_And_Int()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Key"] = 20.0f;
            second["Key"] = 20;

            // Act.
            var result1 = first.CompareTo(second);
            var result2 = second.CompareTo(first);

            // Assert.
            Assert.Equal(+1, result1);//, "result1");
            Assert.Equal(-1, result2);//, "result2");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_CompareTo_With_Different_Typed_Values_Int_And_String()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Key"] = 20;
            second["Key"] = "20";

            // Act.
            var result1 = first.CompareTo(second);
            var result2 = second.CompareTo(first);

            // Assert.
            if (Environment.Is64BitProcess)
            {
                Assert.Equal(-1, result1);//, "result1");
                Assert.Equal(+1, result2);//, "result2");
            }
            else
            {
                // On 64 bit unit tests (Resharper).
                Assert.Equal(+1, result1);//, "result1");
                Assert.Equal(-1, result2);//, "result2");
            }
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_CompareTo_With_Different_Typed_Values_String_And_Int()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Key"] = "20";
            second["Key"] = 20;

            // Act.
            var result1 = first.CompareTo(second);
            var result2 = second.CompareTo(first);

            // Assert.
            if (Environment.Is64BitProcess)
            {
                Assert.Equal(+1, result1);//, "result1");
                Assert.Equal(-1, result2);//, "result2");
            }
            else
            {
                // On 64 bit unit tests (Resharper).
                Assert.Equal(-1, result1);//, "result1");
                Assert.Equal(+1, result2);//, "result2");
            }
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_CompareTo_With_Different_Typed_Values_Int_And_Null()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Key"] = 20;
            second["Key"] = null;

            // Act.
            var result1 = first.CompareTo(second);
            var result2 = second.CompareTo(first);

            // Assert.
            Assert.Equal(+1, result1);//, "result1");
            Assert.Equal(-1, result2);//, "result2");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_CompareTo_With_Different_Typed_Values_Null_And_Int()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Key"] = null;
            second["Key"] = 20;

            // Act.
            var result1 = first.CompareTo(second);
            var result2 = second.CompareTo(first);

            // Assert.
            Assert.Equal(-1, result1);//, "result1");
            Assert.Equal(+1, result2);//, "result2");
        }
        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_CompareTo_With_Different_Typed_Values_String_And_Null()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Key"] = "20";
            second["Key"] = null;

            // Act.
            var result1 = first.CompareTo(second);
            var result2 = second.CompareTo(first);

            // Assert.
            Assert.Equal(+1, result1);//, "result1");
            Assert.Equal(-1, result2);//, "result2");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_CompareTo_With_Different_Typed_Values_Null_And_String()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Key"] = null;
            second["Key"] = "20";

            // Act.
            var result1 = first.CompareTo(second);
            var result2 = second.CompareTo(first);

            // Assert.
            Assert.Equal(-1, result1);//, "result1");
            Assert.Equal(+1, result2);//, "result2");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_CompareTo_Different_Null()
        {
            // Arrange.
            var first = new PropertyDictionary();
            PropertyDictionary second = null;
            first["Name"] = "John";
            first["Birthdate"] = new DateTime(1978, 07, 29);

            // Act.
            var result = first.CompareTo(second);

            // Assert.
            Assert.Equal(+1, result);//, "result");
        }
    }
}
