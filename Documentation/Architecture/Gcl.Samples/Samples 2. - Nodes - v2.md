01. Traverse to a single node and returns it as a named item.
Throws an error if multiple nodes are found.
```gcl
= Person:Doe/John
{
    FirstName,
    LastName,
    NickName
}
```

02. Traverse to multiple nodes and returns them as an array.
Does not throw an error when no node or only one node is found.
```gcl
= Person:Doe/
{
    FirstName,
    LastName,
    NickName
}
```

03. Returns both an item and an array in one single response.
```gcl
Data
{
    Person = Person:Doe/John
    {
        FirstName = @,
        LastName = \#FamilyName,
        NickName,
        Friends = /Friends/
        {
            FirstName = @,
            LastName = \#FamilyName
        }
    },
    Location[] = location:DE/Berlin//
    {
        FirstName = @,
        LastName = \#FamilyName,
        NickName,
        Friends[] = /Friends/
        {
            FirstName = @,
            LastName = \#FamilyName
        }
    }
}
```

04.a Returns two different arrays in one single response.
```gcl
Data
{
    Persons[] = Person:Doe/*
    {
        FirstName = @,
        LastName = \#FamilyName,
        NickName,
        Friends = /Friends/
        {
            FirstName = @,
            LastName = \#FamilyName
        }
    },
    Locations[] = location:DE/Berlin//
    {
        FirstName = @,
        LastName = \#FamilyName,
        NickName,
        Friends[] = /Friends/
        {
            FirstName = @,
            LastName = \#FamilyName
        }
    }
}
```

04.b Returns two different arrays in one single response.
```gcl
Data
{
    Persons[] = Person:Doe/*
    {
        FirstName = @,
        LastName  = \#FamilyName,
        NickName,
        Friends[] = /Friends/
        {
            FirstName = @,
            LastName = \#FamilyName
        }
    }
    Locations[] = location:DE/Berlin//
    {
        Name[] = @,
        Latitude = \#FamilyName,
        Longitude
    }
}
```

06.a Nesting is always possible.
```gcl
DataOuter
{
    DataInner
    {
        Person = Person:Doe/John
        {
            FirstName = @,
            LastName = \#FamilyName,
            NickName
        }
    }
}
```

06.b Nesting is always possible.
```gcl
Data
{
    Person = Person:Doe/John
    {
        Name
        {
            First = @,
            Last = \LastName,
            NickName
        }
    }
}
```

06.c Nest traversal queries to explore additional context.
```gcl
Data
{
    Person = Person:Doe/John
    {
        FirstName = @,
        LastName = \#FamilyName,
        NickName,
        Friends = /Friends/
        {
            FirstName = @,
            LastName = \#FamilyName,
            NickName
        }
    }
}
```
