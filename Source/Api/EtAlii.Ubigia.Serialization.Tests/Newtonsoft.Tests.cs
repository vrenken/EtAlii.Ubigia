// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Serialization.Tests
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class NewtonsoftTests
    {
        [Fact]//, ExpectedException(typeof(AssertFailedException), "JSON support should be disabled")]
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

        [Fact]//, ExpectedException(typeof(AssertFailedException), "JSON support should be disabled")]
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


        [Fact]//, ExpectedException(typeof(AssertFailedException), "JSON support should be disabled")]
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

        [Fact]//, ExpectedException(typeof(AssertFailedException), "JSON support should be disabled")]
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

        [Fact]//, ExpectedException(typeof(AssertFailedException), "JSON support should be disabled")]
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

        [Fact]//, ExpectedException(typeof(AssertFailedException), "JSON support should be disabled")]
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


        [Fact]//, ExpectedException(typeof(AssertFailedException), "JSON support should be disabled")]
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

        [Fact]
        public void Newtonsoft_JsonNet_DictionaryReplacement_String_Guid_1()
        {
            // Arrange.
            var dictionary = new DictionaryReplacement
            {
                A = "text",
                D = Guid.NewGuid()
            };
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            };

            // Act.
            var json = JsonConvert.SerializeObject(dictionary, settings);
            var deserializedDictionary = JsonConvert.DeserializeObject<DictionaryReplacement>(json, settings);

            // Assert.
            Assert.Equal(dictionary.A, deserializedDictionary.A);
            Assert.Equal(dictionary.D, deserializedDictionary.D);
        }

        [Fact]//, ExpectedException(typeof(AssertFailedException), "JSON support should be disabled")]
        public void Newtonsoft_JsonNet_DictionaryReplacement_String_Guid_2()
        {
            // Arrange.
            var dictionary = new DictionaryReplacement2
            {
                A = "text",
                D = Guid.NewGuid()
            };

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            };
            var serializer = new JsonSerializer
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full,
            };
            var o = JObject.FromObject(dictionary, serializer);

            // Act.
            var json = JsonConvert.SerializeObject(dictionary, settings);
            var deserializedDictionary = JsonConvert.DeserializeObject<DictionaryReplacement2>(json, settings);

            // Assert.
            Assert.NotNull(o);
            Assert.Equal(dictionary.A, deserializedDictionary.A);
            Assert.NotEqual(dictionary.D, deserializedDictionary.D);
        }

        [Fact]//, ExpectedException(typeof(AssertFailedException), "JSON support should be disabled")]
        public void Newtonsoft_JsonNet_DictionaryReplacement_String_Guid_3()
        {
            // Arrange.
            var dictionary = new DictionaryReplacement2
            {
                D = Guid.NewGuid(),
                A = "text"
            };

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            };
            var serializer = new JsonSerializer
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full,
            };
            var o = JObject.FromObject(dictionary, serializer);

            // Act.
            var json = JsonConvert.SerializeObject(dictionary, settings);
            var deserializedDictionary = JsonConvert.DeserializeObject<DictionaryReplacement2>(json, settings);

            // Assert.
            Assert.NotNull(o);
            Assert.Equal(dictionary.A, deserializedDictionary.A);
            Assert.NotEqual(dictionary.D, deserializedDictionary.D);
        }
    }

    public class DictionaryReplacement
    {
        public string A;
        public Guid D;
    }

    public class DictionaryReplacement2
    {
        public string A;
        public object D;
    }
}
