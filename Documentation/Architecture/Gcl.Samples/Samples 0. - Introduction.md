# Introduction - Schema's

01. Traverses to a node and returns this as a named element.
All properties that can be found will be returned.
```gcl
Person = @node(Person:Doe/John)
{
}
```

02.a Traverses to a node and returns it as a named element.
Only the properties that can be found will be returned. No error is given if a property cannot be found.
Properties can be separated by comma's or by newlines.
```gcl
Person = @node(Person:Doe/John)
{
    FirstName,
    LastName,
    NickName
}
```

02.b Traverses to a node and returns it as a named element.
Only the properties that can be found will be returned. No error is given if a property cannot be found.
Properties can be separated by comma's or by newlines.
```gcl
Person = @node(Person:Doe/John)
{
    FirstName
    LastName
    NickName
}
```

03. Property identifiers can be quoted for more accurate control.
```gcl
Person = @node(Person:Doe/John)
{
    "FirstName",
    "LastName",
    "NickName"
}
```

04.a Both quoted and unquoted property identifiers can be comma separated, newline separated or a combination of the two.
```gcl
Person = @node(Person:Doe/John)
{
    FirstName,
    LastName,
    NickName
}
```

04.b Both quoted and unquoted property identifiers can be comma separated, newline separated or a combination of the two.
```gcl
Person = @node(Person:Doe/John)
{
    "FirstName",
    "LastName",
    "NickName"
}
```

04.c Both quoted and unquoted property identifiers can be comma separated, newline separated or a combination of the two.
```gcl
Person = @node(Person:Doe/John)
{
    FirstName,
    LastName,
    NickName
}
```

04.d Both quoted and unquoted property identifiers can be comma separated, newline separated or a combination of the two.
```gcl
Person = @node(Person:Doe/John) { FirstName, LastName, NickName }
```

04.e Both quoted and unquoted property identifiers can be comma separated, newline separated or a combination of the two.
```gcl
Person = @node(Person:Doe/John) { "FirstName", "LastName", "NickName" }
```

05.a Traverses to a node and returns it as a named element.
An error is given when any of the the properties marked as mandatory cannot be found.
```gcl
Person = @node(Person:Doe/John)
{
    !"FirstName",
    !"LastName",
    "NickName"
}
```

06. Traverses to a node and returns it as a named element.
An error is given when any of the the properties marked as mandatory cannot be found.
```gcl
Person = @node(Person:Doe/John)
{
    !FirstName,
    !LastName,
    NickName
}
```

07. Traverses to a node and returns it as a named element.
An error is given when any of the the properties marked as mandatory cannot be found.
```gcl
Person = @node(Person:Doe/John)
{
    !FirstName,
    !LastName,
    NickName
}
```

08. Traverses to a node and returns it as a named element.
An error is given when any of the the properties not marked as optional cannot be found.
```gcl
Person = @node(Person:Doe/John)
{
    FirstName,
    LastName,
    ?NickName
}
```

09. Traverses to a node and returns it as a named element.
The result will be enriched using the node, nodes and value annotations added to the property identifiers.
Only the properties that can be found will be returned. No error is given if a property cannot be found.
```gcl
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
}
```

10. Traverses to a node and returns it as a named element.
The result will be enriched using the node, nodes and value annotations added to the property identifiers.
An error is given when any of the the properties marked as mandatory cannot be found.
```gcl
Person = @node(Person:Doe/John)
{
    !FirstName = @node(),
    !LastName = @node(\#FamilyName),
    NickName,
    Friends = @nodes(/Friends)
    {
        FirstName = @node(),
        LastName = @node(\#FamilyName)
    }
}
```

11. Traverses to a node and returns it as a named element.
The result will be enriched using the node, nodes and value annotations added to the property identifiers.
An error is given when any of the the properties not marked as optional cannot be found.
```gcl
Person = @node(Person:Doe/John)
{
    FirstName = @node(),
    LastName = @node(\#FamilyName),
    ?NickName,
    ?Friends = @nodes(/Friends)
    {
        FirstName = @node(),
        LastName = @node(\#FamilyName)
    }
}
```

12. Traverses to a node and returns it as a named element.
The result will be enriched using the node, nodes and value annotations added to the property identifiers.
An error is given when any of the the properties not marked as optional cannot be found.
```gcl
Person = @node(Person:Doe/John)
{
    FirstName = @node(),
    LastName = @node(\#FamilyName),
    ?NickName,
    ?Friends = @nodes(/Friends)
    {
        FirstName = @node(),
        LastName = @node(\#FamilyName)
    }
}
```
