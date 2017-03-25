
namespace EtAlii.Ubigia.Storage.Tests.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization.Formatters;
    using Xunit;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    
    public class Newtonsoft_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]//, ExpectedException(typeof(AssertFailedException), "JSON support should be disabled")]
        public void Newtonsoft_JsonNet_Object_Guid()
        {
            // Arrange.
            object guid = Guid.NewGuid();

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            };

            // Act.
            var json = JsonConvert.SerializeObject(guid, settings);

            var deserializedGuid = JsonConvert.DeserializeObject<object>(json, settings);

            // Assert.
            Assert.NotEqual(guid, deserializedGuid);
        }

        [Fact, Trait("Category", TestAssembly.Category)]//, ExpectedException(typeof(AssertFailedException), "JSON support should be disabled")]
        public void Newtonsoft_JsonNet_Dictionary_String_Int_Bool_1()
        {
            // Arrange.
            var dictionary = new Dictionary<string, object> {
                { "a", "text" },
                { "b", 32 },
                { "c", false },
            };
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            };

            // Act.
            var json = JsonConvert.SerializeObject(dictionary, settings);
            var deserializedDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json, settings);

            // Assert.
            Assert.Equal(dictionary["a"], deserializedDictionary["a"]);
            Assert.NotEqual(dictionary["b"], deserializedDictionary["b"]);
            Assert.Equal(dictionary["c"], deserializedDictionary["c"]);
        }


        [Fact, Trait("Category", TestAssembly.Category)]//, ExpectedException(typeof(AssertFailedException), "JSON support should be disabled")]
        public void Newtonsoft_JsonNet_Dictionary_String_Int_Bool_2()
        {
            // Arrange.
            var dictionary = new Dictionary<string, object> {
                { "b", 32 },
                { "c", false },
                { "a", "text" },
            };
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            };

            // Act.
            var json = JsonConvert.SerializeObject(dictionary, settings);
            var deserializedDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json, settings);

            // Assert.
            Assert.Equal(dictionary["a"], deserializedDictionary["a"]);
            Assert.NotEqual(dictionary["b"], deserializedDictionary["b"]);
            Assert.Equal(dictionary["c"], deserializedDictionary["c"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]//, ExpectedException(typeof(AssertFailedException), "JSON support should be disabled")]
        public void Newtonsoft_JsonNet_Dictionary_String_Int_Bool_Guid_1()
        {
            // Arrange.
            var dictionary = new Dictionary<string, object> {
                { "a", "text" },
                { "b", 32 },
                { "c", false },
                { "d", Guid.NewGuid() }
            };
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            };

            // Act.
            var json = JsonConvert.SerializeObject(dictionary, settings);
            var deserializedDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json, settings);

            // Assert.
            Assert.Equal(dictionary["a"], deserializedDictionary["a"]);
            Assert.NotEqual(dictionary["b"], deserializedDictionary["b"]);
            Assert.Equal(dictionary["c"], deserializedDictionary["c"]);
            Assert.NotEqual(dictionary["d"], deserializedDictionary["d"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]//, ExpectedException(typeof(AssertFailedException), "JSON support should be disabled")]
        public void Newtonsoft_JsonNet_Dictionary_String_Int_Bool_Guid_2()
        {
            // Arrange.
            var dictionary = new Dictionary<string, object> {
                { "b", 32 },
                { "c", false },
                { "d", Guid.NewGuid() },
                { "a", "text" },
            };
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            };

            // Act.
            var json = JsonConvert.SerializeObject(dictionary, settings);
            var deserializedDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json, settings);

            // Assert.
            Assert.Equal(dictionary["a"], deserializedDictionary["a"]);
            Assert.NotEqual(dictionary["b"], deserializedDictionary["b"]);
            Assert.Equal(dictionary["c"], deserializedDictionary["c"]);
            Assert.NotEqual(dictionary["d"], deserializedDictionary["d"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]//, ExpectedException(typeof(AssertFailedException), "JSON support should be disabled")]
        public void Newtonsoft_JsonNet_Dictionary_String_Guid_1()
        {
            // Arrange.
            var dictionary = new Dictionary<string, object> {
                { "a", "text" },
                { "d", Guid.NewGuid() }
            };
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            };

            // Act.
            var json = JsonConvert.SerializeObject(dictionary, settings);
            var deserializedDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json, settings);

            // Assert.
            Assert.Equal(dictionary["a"], deserializedDictionary["a"]);
            Assert.NotEqual(dictionary["d"], deserializedDictionary["d"]);
        }


        [Fact, Trait("Category", TestAssembly.Category)]//, ExpectedException(typeof(AssertFailedException), "JSON support should be disabled")]
        public void Newtonsoft_JsonNet_Dictionary_String_Guid_2()
        {
            // Arrange.
            var dictionary = new Dictionary<string, object> {
                { "d", Guid.NewGuid() },
                { "a", "text" },
            };
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            };

            // Act.
            var json = JsonConvert.SerializeObject(dictionary, settings);
            var deserializedDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json, settings);

            // Assert.
            Assert.Equal(dictionary["a"], deserializedDictionary["a"]);
            Assert.NotEqual(dictionary["d"], deserializedDictionary["d"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Newtonsoft_JsonNet_DictionaryReplacement_String_Guid_1()
        {
            // Arrange.
            var dictionary = new DictionaryReplacement
            {
                a = "text",
                d = Guid.NewGuid()
            };
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            };

            // Act.
            var json = JsonConvert.SerializeObject(dictionary, settings);
            var deserializedDictionary = JsonConvert.DeserializeObject<DictionaryReplacement>(json, settings);

            // Assert.
            Assert.Equal(dictionary.a, deserializedDictionary.a);
            Assert.Equal(dictionary.d, deserializedDictionary.d);
        }

        [Fact, Trait("Category", TestAssembly.Category)]//, ExpectedException(typeof(AssertFailedException), "JSON support should be disabled")]
        public void Newtonsoft_JsonNet_DictionaryReplacement_String_Guid_2()
        {
            // Arrange.
            var dictionary = new DictionaryReplacement2
            {
                a = "text",
                d = Guid.NewGuid()
            };

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            };
            var serializer = new JsonSerializer
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Full,
            };
            // ReSharper disable once UnusedVariable
            var o = JObject.FromObject(dictionary, serializer);

            // Act.
            var json = JsonConvert.SerializeObject(dictionary, settings);
            var deserializedDictionary = JsonConvert.DeserializeObject<DictionaryReplacement2>(json, settings);

            // Assert.
            Assert.Equal(dictionary.a, deserializedDictionary.a);
            Assert.NotEqual(dictionary.d, deserializedDictionary.d);
        }

        [Fact, Trait("Category", TestAssembly.Category)]//, ExpectedException(typeof(AssertFailedException), "JSON support should be disabled")]
        public void Newtonsoft_JsonNet_DictionaryReplacement_String_Guid_3()
        {
            // Arrange.
            var dictionary = new DictionaryReplacement2
            {
                d = Guid.NewGuid(),
                a = "text"
            };

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            };
            var serializer = new JsonSerializer
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Full,
            };
            // ReSharper disable once UnusedVariable
            var o = JObject.FromObject(dictionary, serializer);

            // Act.
            var json = JsonConvert.SerializeObject(dictionary, settings);
            var deserializedDictionary = JsonConvert.DeserializeObject<DictionaryReplacement2>(json, settings);

            // Assert.
            Assert.Equal(dictionary.a, deserializedDictionary.a);
            Assert.NotEqual(dictionary.d, deserializedDictionary.d);
        }
    }

    public class DictionaryReplacement
    {
        public string a;
        public Guid d;
    }

    public class DictionaryReplacement2
    {
        public string a;
        public object d;
    }
}
