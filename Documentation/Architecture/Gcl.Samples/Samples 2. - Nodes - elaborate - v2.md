01. Traverse to a single node and returns it as a named item.
Throws an error if multiple nodes are found.
```gcl
Person = @node(Person:Doe/John)
{
    FirstName,
    LastName,
    NickName
}
```

02. Traverse to multiple nodes and returns them as an array.
Does not throw an error when no node or only one node is found.
```gcl
Person = @nodes(Person:Doe/)
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
    Person = @node(Person:Doe/John)
    {
        FirstName = @node(),
        LastName = @node(\#FamilyName),
        NickName,
        Friends = @nodes(/Friends/)
        {
            FirstName = @node(),
            LastName = @node(\#FamilyName)
        }
    },
    Locations[] = @nodes(location:DE/Berlin//)
    {
        FirstName = @node(),
        LastName = @node(\#FamilyName),
        NickName,
        Friends[] = @nodes(/Friends/)
        {
            FirstName = @node(),
            LastName = @node(\#FamilyName)
        }
    }
}
```

04.a Returns two different arrays in one single response.
```gcl
Data
{
    Persons[] = @nodes(Person:Doe/*)
    {
        FirstName = @node(),
        LastName = @node(\#FamilyName),
        NickName,
        Friends[] = @nodes(/Friends/)
        {
            FirstName = @node(),
            LastName = @node(\#FamilyName)
        }
    },
    Locations[] = @nodes(location:DE/Berlin//)
    {
        FirstName = @node(),
        LastName = @node(\#FamilyName),
        NickName,
        Friends[] = @nodes(/Friends/)
        {
            FirstName = @node(),
            LastName = @node(\#FamilyName)
        }
    }
}
```

04.b Returns two different arrays in one single response.
```gcl
Data
{
    Persons[] = @nodes(Person:Doe/*)
    {
        FirstName = @node(),
        LastName  = @node(\#FamilyName),
        NickName,
        Friends[] = @nodes(/Friends/)
        {
            FirstName = @node(),
            LastName = @node(\#FamilyName)
        }
    }
    Locations[] = @nodes(location:DE/Berlin//)
    {
        Name = @node(),
        Latitude = @node(/Position/Latitude),
        Longitude = @node(/Position/Longitude)
    }
}
```

06.a Nesting is always possible.
```gcl
DataOuter
{
    DataInner
    {
        Person = @node(Person:Doe/John)
        {
            FirstName = @node(),
            LastName = @node(\#FamilyName),
            NickName
        }
    }
}
```

06.b Nesting is always possible.
```gcl
Data
{
    Person = @node(Person:Doe/John)
    {
        Name
        {
            First = @node(),
            Last = @node(\LastName),
            NickName
        }
    }
}
```

06.c Nest traversal queries to explore additional context.
```gcl
Data
{
    Person = @node(Person:Doe/John)
    {
        FirstName = @node(),
        LastName = @node(\#FamilyName),
        NickName,
        Friends[] = @nodes(/Friends/)
        {
            FirstName = @node(),
            LastName = @node(\#FamilyName),
            NickName
        }
    }
}
```
