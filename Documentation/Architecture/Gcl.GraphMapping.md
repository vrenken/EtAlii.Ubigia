# Ubigia Graph Context Language - Graph mapping

The basic principle of graph mapping is simple: where needed the value and structure fragments get assigned a mapping
to the needed graph path or action.

- 2 variations.
- assignment splits results from graph operations.

## Traversal
To relate graph information to structures and fields [GTL traversal paths](/PathToBeDetermined) need to be used. For queries (i.e. only the fetching of data) these need to be prefixed with the assignment operator <=. The topmost traversal path used in any structure should be a [rooted](/PathToBeDetermined), [absolute](/PathToBeDetermined) or [variable-based](/PathToBeDetermined) path. This will be the starting point of all graph traversals to come.

Example:
```gcl
Person = Person:Start/Tony
{
    FirstName,
    LastName
}
```

The name of the root structure can be ommited for brevety. This is especially handy for code generation where then the .GCL filename will be used to define the root object name. For all child structures names are mandatory as there else is no way to identify and access them.

Example:
```gcl
= Person:Start/Tony
{
    FirstName,
    LastName
}
```

When nested structures are used and a parent structure is already mapped using a GTL path, a child structure can use a [relative GTL path](/PathToBeDetermined). During querying the traversal data from the parent path will be used to continue the relative path traversal with.

Example:
```gcl
= Person:Start/Tony
{
    FirstName,
    LastName,
    Friends[] = /Friends/
    {
        FirstName,
        LastName
    }
}
```


When nested structures are used and a parent structure is already mapped using a GTL path, a child structure can use an [absolute GTL path](/PathToBeDetermined). This allows unrelated information to be composed together in ways that best fit the applications. During querying the traversal data from the parent path will *NOT* used. The absolute path traversal will overrule this and provide a different set of data.

Example:
```gcl
= Person:Start/Tony
{
    FirstName,
    LastName,
    Locations[] = Location:Germany/NRW/#Cities
    {
        Name,
        Latitude,
        Longitude
    }
}
```

If a structure is mapped to a GTL path, the values in it will by default be filled with properties of the resulting graph nodes.
If explicit path assignments are added to fields, and they are not classified as structures themselves then the GCL processing and code generation except a value from the path provided. In that case the GTL path used will convert the resulting [node](/PathToBeDetermined), [property](/PathToBeDetermined), [tag](/PathToBeDetermined) or [blob](/PathToBeDetermined) to an atomic value (i.e. a string, bool, datetime, int, float, or in case of a blob a stream).

Example:
```gcl
= Person:Start/Tony
{
    FirstName,
    LastName = \#FamilyName,  -- Fetch a parent node with the tag lastname and assign ot to the property LastName
    Birthday = .Birthdate     -- Fetch the property birthdate and assign it to the Birthday value
}
```

The @ character can be used to assign the name of the current graph node to a value.
Example:
```gcl
= Person:Start/Tony
{
    FirstName @,
    LastName = \#FamilyName,
    Birthday = .Birthdate
}
```


## Mutations - setting values
TODO

## Mutations - adding entities
TODO
