# Ubigia Graph Context Language - Additional examples

## Traversal

### Example 1: Traverses to a node and returns this as a named element.
All properties that can be found will be returned.

```
Person = @node(Person:Doe/John)
{
}
```

### Example 2.a: Traverses to a node and returns it as a named element.
Only the properties that can be found will be returned. No error is given if a property cannot be found.
Properties can be separated by comma's or by newlines.

```
Person = @node(Person:Doe/John)
{
    FirstName,
    LastName,
    NickName
}
```

### Example 2.b: Traverses to a node and returns it as a named element.
Only the properties that can be found will be returned. No error is given if a property cannot be found.
Properties can be separated by comma's or by newlines.

```
Person = @node(Person:Doe/John)
{
    FirstName
    LastName
    NickName
}
```

### Example 3: Property identifiers can be quoted for more accurate control.

```
Person = @node(Person:Doe/John)
{
    "FirstName",
    "LastName",
    "NickName"
}
```

### Example 4.a: Both quoted and unquoted property identifiers can be comma separated, newline separated or a combination of the two.

```
Person = @node(Person:Doe/John)
{
    FirstName,
    LastName,
    NickName
}
```

### Example 4.b: Both quoted and unquoted property identifiers can be comma separated, newline separated or a combination of the two.

```
Person = @node(Person:Doe/John)
{
    "FirstName",
    "LastName",
    "NickName"
}
```

### Example 4.c: Both quoted and unquoted property identifiers can be comma separated, newline separated or a combination of the two.

```
Person = @node(Person:Doe/John)
{
    FirstName,
    LastName,
    NickName
}
```

### Example 4.d: Both quoted and unquoted property identifiers can be comma separated, newline separated or a combination of the two.

```
Person = @node(Person:Doe/John) { FirstName, LastName, NickName }
```

### Example 4.e: Both quoted and unquoted property identifiers can be comma separated, newline separated or a combination of the two.

```
Person = @node(Person:Doe/John) { "FirstName", "LastName", "NickName" }
```

### Example 5.a: Traverses to a node and returns it as a named element.
An error is given when any of the the properties marked as mandatory cannot be found.

```
Person = @node(Person:Doe/John)
{
    !"FirstName",
    !"LastName",
    "NickName"
}
```

### Example 6: Traverses to a node and returns it as a named element.
An error is given when any of the the properties marked as mandatory cannot be found.

```
Person = @node(Person:Doe/John)
{
    !FirstName,
    !LastName,
    NickName
}
```

### Example 7: Traverses to a node and returns it as a named element.
An error is given when any of the the properties marked as mandatory cannot be found.

```
Person = @node(Person:Doe/John)
{
    !FirstName,
    !LastName,
    NickName
}
```

### Example 8: Traverses to a node and returns it as a named element.
An error is given when any of the the properties not marked as optional cannot be found.

```
Person = @node(Person:Doe/John)
{
    FirstName,
    LastName,
    ?NickName
}
```

### Example 09: Traverses to a node and returns it as a named element.
The result will be enriched using the node, nodes and value annotations added to the property identifiers.
Only the properties that can be found will be returned. No error is given if a property cannot be found.

```
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

### Example 10: Traverses to a node and returns it as a named element.
The result will be enriched using the node, nodes and value annotations added to the property identifiers.
An error is given when any of the the properties marked as mandatory cannot be found.

```
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

### Example 11: Traverses to a node and returns it as a named element.
The result will be enriched using the node, nodes and value annotations added to the property identifiers.
An error is given when any of the the properties not marked as optional cannot be found.

```
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

### Example 12: Traverses to a node and returns it as a named element.
The result will be enriched using the node, nodes and value annotations added to the property identifiers.
An error is given when any of the the properties not marked as optional cannot be found.

```
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
