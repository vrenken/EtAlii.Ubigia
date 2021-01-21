# Graph Context Language results structuring

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
