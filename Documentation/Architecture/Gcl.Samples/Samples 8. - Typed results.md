-- ==================================================================================================================
-- 01. Simple typing. Supported types are: string, bool, float, int, datetime.
Person = Person:Doe/John
{
    string FirstName
    string LastName
    string NickName
    datetime BirthDate
    int NumberOfChildren
    float Height
    bool IsMale
}

-- ==================================================================================================================
-- 02.a Simple typing - with conditions.
Person = Person:Doe/John
{
    string! FirstName
    string! LastName
    string? NickName
    datetime! BirthDate
    int NumberOfChildren
    float? Height
    bool IsMale
}


-- ==================================================================================================================
-- 02.b Simple typing - with conditions.
Person = Person:Doe/John
{
    string !FirstName
    string !LastName
    string ?NickName
    datetime !BirthDate
    int NumberOfChildren
    float ?Height
    bool IsMale
}

-- ==================================================================================================================
-- 03. Multiplicity
Person = Person:Doe/John
{
    string FirstName = @
    string LastName = \#FamilyName
    string NickName
    datetime BirthDate
    int NumberOfChildren
    Friends[] = /Friends/
    {
        string FirstName = @
        string LastName = \#FamilyName
    }
    Location = location:DE/Berlin//
    {
        string Name = @
        string Country = \#Country
        string GeoPosition = /Position
    }
}

-- ==================================================================================================================
-- 04. Namespaces.

[namespace=EtAlii.Ubigia.Api.Functional.Context.Tests.Model]
TypedPerson = @nodes(Person:Stark/Tony)
{
    string FirstName
    string LastName
    string NickName
    datetime BirthDate
    int NumberOfChildren
    float Height
    bool IsMale
}


-- ==================================================================================================================
-- 05. Make sure the code generator also creates a process extension method.

[CreateProcessMethod=true]
[namespace=EtAlii.Ubigia.Api.Functional.Context.Tests.Model]
TypedPerson = @nodes(Person:Stark/Tony)
{
    string FirstName
    string LastName
    string NickName
    datetime BirthDate
    int NumberOfChildren
    float Height
    bool IsMale
}

-- ==================================================================================================================
-- 06. Set this property to true to indicate that a dynamic result class type should be created.
-- Options are:
-- - [ResultType=static]
-- - [ResultType=observable]
-- - [ResultType=async_enumerable]

[ResultType=dynamic]
[namespace=EtAlii.Ubigia.Api.Functional.Context.Tests.Model]
TypedPerson = @nodes(Person:Stark/Tony)
{
    string FirstName
    string LastName
    string NickName
    datetime BirthDate
    int NumberOfChildren
    float Height
    bool IsMale
}