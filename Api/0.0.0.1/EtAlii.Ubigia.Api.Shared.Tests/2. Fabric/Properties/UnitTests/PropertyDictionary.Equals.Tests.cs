﻿namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using System;
    using EtAlii.Ubigia.Api.Tests;
    using Xunit;

    
    public class PropertyDictionary_Equals_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
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
            var result = first.Equals(second);

            // Assert.
            Assert.Equal(true, result);//, "result");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
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
            var result = first.Equals(second);

            // Assert.
            Assert.Equal(false, result);//, "result");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_Equals_Different_Value()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();

            first["Name"] = "John";
            second["Birthdate"] = new DateTime(1978, 07, 29);

            // Act.
            var result = first.Equals(second);

            // Assert.
            Assert.Equal(false, result);//, "result");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
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
            var result = first.Equals(second);

            // Assert.
            Assert.Equal(false, result);//, "result");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
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
            Assert.Equal(false, result);//, "result");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
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
            Assert.Equal(false, result);//, "result");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
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
            Assert.Equal(true, result);//, "result");
            // Unexpected i know, but it's true: http://stackoverflow.com/questions/28602941/datetime-compare-ignores-kind
        }

        [Fact, Trait("Category", TestAssembly.Category)]
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
            var result = first.Equals(second);

            // Assert.
            Assert.Equal(false, result);//, "result");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_Equals_With_Different_Typed_Values_Int_And_Float()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Key"] = 20;
            second["Key"] = 20.0f;

            // Act.
            var result = first.Equals(second);

            // Assert.
            Assert.Equal(false, result);//, "result");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_Equals_With_Different_Typed_Values_Float_And_Int()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Key"] = 20.0f;
            second["Key"] = 20;

            // Act.
            var result = first.Equals(second);

            // Assert.
            Assert.Equal(false, result);//, "result");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_Equals_With_Different_Typed_Values_Int_And_String()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Key"] = 20;
            second["Key"] = "20";

            // Act.
            var result = first.Equals(second);

            // Assert.
            Assert.Equal(false, result);//, "result");
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_Equals_With_Different_Typed_Values_String_And_Int()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Key"] = "20";
            second["Key"] = 20;

            // Act.
            var result = first.Equals(second);

            // Assert.
            Assert.Equal(false, result);//, "result");
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_Equals_With_Different_Typed_Values_Int_And_Null()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Key"] = 20;
            second["Key"] = null;

            // Act.
            var result = first.Equals(second);

            // Assert.
            Assert.Equal(false, result);//, "result");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_Equals_With_Different_Typed_Values_Null_And_Int()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Key"] = null;
            second["Key"] = 20;

            // Act.
            var result = first.Equals(second);

            // Assert.
            Assert.Equal(false, result);//, "result");
        }
        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_Equals_With_Different_Typed_Values_String_And_Null()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Key"] = "20";
            second["Key"] = null;

            // Act.
            var result = first.Equals(second);

            // Assert.
            Assert.Equal(false, result);//, "result");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_Equals_With_Different_Typed_Values_Null_And_String()
        {
            // Arrange.
            var first = new PropertyDictionary();
            var second = new PropertyDictionary();
            first["Key"] = null;
            second["Key"] = "20";

            // Act.
            var result = first.Equals(second);

            // Assert.
            Assert.Equal(false, result);//, "result");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionary_Equals_Different_Null()
        {
            // Arrange.
            var first = new PropertyDictionary();
            PropertyDictionary second = null;
            first["Name"] = "John";
            first["Birthdate"] = new DateTime(1978, 07, 29);

            // Act.
            var result = first.Equals(second);

            // Assert.
            Assert.Equal(false, result);//, "result");
        }
    }
}
