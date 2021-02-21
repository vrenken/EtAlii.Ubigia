01. Simple typing. Supported types are: string, bool, float, int, datetime.
```gcl
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
```

02.a Simple typing - with conditions.
```gcl
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
```

02.b Simple typing - with conditions.
```gcl
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
```

03. Multiplicity
```gcl
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
```

04. Namespaces.
```gcl
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
```

05. Make sure the code generator also creates a process extension method.
```gcl
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
```

06. Set this property to true to indicate that a dynamic result class type should be created.
Options are:
- ``[ResultType=dynamic]``
- ``[ResultType=static]``
- ``[ResultType=observable]``
- ``[ResultType=async_enumerable]``

```gcl
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
```
