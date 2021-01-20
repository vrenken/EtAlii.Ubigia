namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    [UbigiaPoco]
    public partial class UserPocoObject
    {
        [UbigiaSchema]
        public string Schema { get; } =
            @"Person @nodes(Person:Stark/Tony)
            {
                FirstName @node()
                LastName @node(\#FamilyName)
            }";
    }

    public partial class UserPocoObject
    {
        // ReSharper disable UnassignedGetOnlyAutoProperty
        public string FirstName { get; }
        public string LastName { get; }
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
