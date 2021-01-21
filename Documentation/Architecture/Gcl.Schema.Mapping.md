# Graph traversal mapping

## Traversal
To relate graph information to structures and fields [GTL traversal paths](/PathToBeDetermined) need to be used. For queries (i.e. only the fetching of data) these need to be prefixed with the assignment operator <=. The topmost traversal path used in any structure should be a [rooted](/PathToBeDetermined), [absolute](/PathToBeDetermined) or [variable-based](/PathToBeDetermined) path. This will be the starting point of all graph traversals to come.

Example:
```
Person <= Person:Start/Tony
{
    FirstName,
    LastName
}
```

The name of the root structure can be ommited for brevety. This is especially handy for code generation. For all child structures names are mandatory as there else is no way to identify and access them.

Example:
```
<= Person:Start/Tony
{
    FirstName,
    LastName
}
```

When nested structures are used and a parent structure is already mapped using a GTL path, a child structure can use a [relative GTL path](/PathToBeDetermined). During querying the traversal data from the parent path will be used to continue the relative path traversal with.

Example:
```
<= Person:Start/Tony
{
    FirstName,
    LastName,
    Friends[] <= /Friends/
    {
        FirstName,
        LastName
    }
}
```


When nested structures are used and a parent structure is already mapped using a GTL path, a child structure can use an [absolute GTL path](/PathToBeDetermined). This allows unrelated information to be composed together in ways that best fit the applications. During querying the traversal data from the parent path will *NOT* used. The absolute path traversal will overrule this and provide a different set of data.

Example:
```
<= Person:Start/Tony
{
    FirstName,
    LastName,
    Locations[] <= Location:Germany/NRW/#Cities
    {
        Name,
        Latitude,
        Longitude
    }
}
```


## Mutations - setting values
TODO

## Mutations - adding entities
TODO

