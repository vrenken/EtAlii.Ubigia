namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    public class UserPocoObject
    {
        // ReSharper disable UnassignedGetOnlyAutoProperty
        public string FirstName2 { get; }
        public string LastName2 { get; }
    }

// <= Person:Stark/Tony
// {
//      FirstName <=
//      LastName <= \#FamilyName
// }

// <= Person:Stark += /$name
// {
// }

// [] <= Person:Stark/*
// {
//      FirstName <=
//      LastName <= \#FamilyName
// }

// Convert GCL to path-only approach.
// Add array modifier.
// Introduce unnamed root path traversal.

}
