# Ubigia Graph Context Language - Result structuring

One perspective to understand database systems is to look at the way they get incorporated into applications.
There are plenty variations on how this can be done, but the two foremost ones are language specific [Object-relational mapping](https://en.wikipedia.org/wiki/Object%E2%80%93relational_mapping),
and the more generic JSON/XML return values. Surely there are plenty other variations possible but for the sake of simplicity
we use these two. The latter is more universal, as the responsibility of interpretation still needs to be done by the consuming application.
The first is more powerful as it can be complemented and fine-tuned to allow as optimal access to the underlying data storage, but
it comes with a cost: each supported language requires a dedicated implementation which need to be maintained.

Many already existing concepts that somehow relate to the Ubigia challenges have been reviewed, but most - if not all - of them
do approach the problems the right way. For example: GraphQL is great, especially for building API's. But what it does not
do is facilitate graph based 'magic'. Entity Framework (Core) is a solid ORM for relational data, and perfectly integrates in C#.
But it does not fit graph based querying or temporal data very well.

The above mentioned limitations got weighted and helped make the decision for the GCL, and make sure that the language was optimized for 'contextual' result interpretation.

In its simplest form the GCL structure has strong relations to writing [class](https://en.wikipedia.org/wiki/Class_(computer_programming)) definitions.
The reason for this is simple: The results of a database interaction should be seen as compositions of different [types](https://en.wikipedia.org/wiki/Type_system),
and writing classes is something most developers need to do today. Keeping the GCL close to defining class definitions, allows
developers to keep a tight relation between the application logic and the data.

What the Ubigia API libraries do is take a GCL definition (which is often stored inside a .gcl1 file), and convert it into
a language-typed data structure.

But let's not open up all magic directly at the beginning, and start with the basics. For this this page will show examples
of the structural principles that make up the GCL language. These examples show two important variations of the same concept,
namely **Value Fragments** and **Structure Fragments**. The idea is that any type can be composed using these two different types fragments.
In this Value Fragments indicate where atomic values like strings, booleans, integers, floats and even datetimes need to be positioned, and
by what identifier they can be accessed. Structure fragments on the other hand are the containers that can wrap different fragments
together, and by doing so also provide the mechanism through which complex types can be composed using nested hierarchies of fragments.

Of course on top of that there is even a more important aspect: The power to add comments.
Let's begin with those. :-)

## Comments
Add comments to a context schema by prefixing them with --. Comments can be made everywhere, but always after any 'operational' text or characters.

Example:
```gcl
-- This is a comment.
```

## Structure fragments
Use curly brackets to define the hierarchical structure of input and results.
It defines scopes that make up logical groups of information that belong together.
When working with data it becomes quickly obvious that structures relate to code data classes.
The code generation subsystems uses GCL structure information to create corresponding classes,
queries and composition hierarchies. At the root level of a GCL only one single structure fragment is allowed.

Example of one level of data:
```gcl
Person
{
}
```

Example of two levels of data:
```gcl
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

## Value fragments & fragment separation
All fragments within a structure fragment can be separated by both newlines or comma's.
If a comma is once used within a structure fragment to separate multiple child fragments then it should be used for
all fragments at the same level in the structure fragment.
When a fragment is not a structure fragment it is classified as a Value Fragment.
Either case the GCL parsing demands the comma or newline separation to be honoured.
Using a different separation pattern in child structures is allowed.

Example using newline-separation of fragments:
```gcl
Person
{
    FirstName
    LastName
}
```

Example using comma-separated fragments:
```gcl
Person
{
    FirstName,
    LastName
}
```
Example using comma-separated fragments on the same line:
```gcl
Person  { FirstName, LastName }
```

Example of a structure fragment that contains a mixture of both value and structure fragments:
```gcl
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

## Optional fragments
Both value and structure fragments can be labeled as optional using the question mark prefix.
In this case missing data won't result in errors.

Example:
```gcl
Person
{
    !FirstName,
    !LastName,
    datetime Birthdate,
    IsHandsome
}
```

## Mandatory fragments
Both value and structure fragments can be labeled as mandatory using the question mark prefix.
In this case missing data will result in errors.

Example:
```gcl
Person
{
    FirstName,
    LastName,
    Birthday,
    ?IsHandsome
}
```

## Typed value fragments
Value fragments represent atomic values. When no type is defined the code generation and verification will
revert back to process data as strings, however The type of data cam be specified by using a type modifier prefix.
Currently the type prefixes defined in the GCL are: string, bool, DateTime, float, int.

Example of value fragments with type modifier prefixes:
```gcl
Person
{
    string FirstName,
    string LastName,
    datetime Birthday,
    bool IsHandsome
}
```

When type modifier prefixes are used both the optional and mandatory prefixes can be added to the type modifier.

Example of value fragments with type modifier and optional prefixes:
```gcl
Person
{
    string! FirstName,
    string! LastName,
    datetime Birthday,
    bool? IsHandsome
}
```

## Structure fragment plurality
Only structure fragments can be labeled as plural using (array) brackets.
This will cause both the processing and code generation to adopt accordingly.
Please take notice that value fragments cannot be marked as plural. For these only atomic values are possible.

Example:
```gcl
Person
{
    FirstName,
    LastName,
    Friends[]
    {
    }
}
```
