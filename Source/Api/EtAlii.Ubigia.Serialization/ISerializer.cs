// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Serialization
{
    using System;
    using System.IO;
    using Newtonsoft.Json;
    
    public interface ISerializer
    {
        T Deserialize<T>(JsonReader reader);
        object Deserialize(JsonReader reader, Type objectType);
        void Serialize(TextWriter textWriter, object value);
        void Serialize(JsonWriter jsonWriter, object value);
    }
}