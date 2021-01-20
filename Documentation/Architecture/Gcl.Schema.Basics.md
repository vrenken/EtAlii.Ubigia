# Graph Context Language schema basics

The Graph Context Language is one of the key components that makes distributed traversal of immutable graph data a reality. 
It is designed using a few guiding principles:

1. We need to be able to traverse all dimensions of graph data, that is: hierarchical, temporal and sequential data. For this the GTL should be fully accepted.
2. Graph data results probably never are flat, therefore the GCL should allow results to be expressed using complex compositional patterns. 
3. Complexity should be reduced using code generators for all supported languages. 
4. The GCL should support expressing the plurality of data. 
5. The GCL should support expressing data to be both optional as wel as mandatory.
6. The GCL should optionally support expressing the atomic type of data - the default is reverting back to strings.

On an abstract leven the GCL is actually a schema with which data can both be queried or altered. As all data in a Ubigia store is immutable it is adviced to rather talk about 'mutations'.

## Comments
Add comments to a context schema by prefixing them with --. 
Comments can be upt at the start of the line or after structures, fields and paths. 

Example:
```
-- This is a comment.
```

## Structure
Use curly brackets to define the hierarchical structure of input and results. 
It defines scopes that make up logical groups of information that belong together. When working with data it becomes quickly obvious that structures relate to code data classes. The code generation subsystems uses GCL structure information to create corresponding classes, queries and composition hierarchies. 
At the root level of a GCL only one single structure is allowed.

Example of one level of data:
```
Person
{
}
```

Example of two levels of data:
```
Person
{
    Friends
    {
    }
    Meetings
    {
    }
}
```

## Fields
Fields can be separated by both newlines or comma's. If a comma is used within one structure to indicate multiple fields then it should be used for all fields at the same level in the structure. 
A field can also be a structure on itself, still the parsing demands the comma or newline separation to be honoured. Using a different separation pattern in child structures is allowed.

Example using newline-separation of fields:
```
Person
{
    FirstName
    LastName
}
```

Example using comma-separated fields:
```
Person
{
    FirstName,
    LastName
}
```
Example using comma-separated fields on the same line:
```
Person  { FirstName, LastName }
```

Example of a field that is a structure:
```
Person
{
    FirstName,
    LastName,
    Friends
    {
        FirstName,
        LastName
    }
}
```

## Optional
Fields and child structures can be labeled as optional using the question mark prefix. In this case missing data won't result in errors.

Example:
```
Person 
{
    !FirstName,
    !LastName,
    datetime Birthdate,
    IsHandsome
}
```

## Mandatory
Fields and child structures can be labeled as mandatory using the question mark prefix. In this case missing data will result in errors.

Example:
```
Person
{
    FirstName,
    LastName,
    Birthdate,
    ?IsHandsome
}
```

## Field types
Non-structural fields represent atomic values. When no type is defined the code generation and verification will revert back to process data as strings. 
The type of data cam be specified by using a type modifier prefix. 
Currently the type prefixes defined in the GCL are: string, bool, DateTime, float, int.

Example of fields with type modifier prefixes:
```
Person
{
    string FirstName,
    string LastName,
    datetime Birthdate,
    bool IsHandsome
}
```

When type modifier prefixes are used both the optional and mandatory prefixes can be added to the type modifier.

Example of fields with type modifier and optional prefixes:
```
Person
{
    string! FirstName,
    string! LastName,
    datetime Birthdate,
    bool? IsHandsome
}
```

## Plurality
Structures can be labeled as plural using (array) brackets. This will cause both the processing and code generation to adopt accordingly.
Please take notice that fields that aren't structures cannot be marked as plural. For these only atomic values are possible.

Example:
```
Person
{
    FirstName,
    LastName,
    Friends[]
    {
    }
}
```

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

