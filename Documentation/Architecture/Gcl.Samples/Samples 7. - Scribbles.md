# Experimental scribbles

03. Query with nodes directives at multiple entity paths unioned together returning multiple entries and a normal property
```
@node(person:Banner/Peter", mode: "Union")
@node(person:Banner/Tanja)
{
    nickname
}
```

04. Query with multiple entity paths unioned together returning multiple entries and a normal property
```
@node(person:Banner/Peter", mode: "Union")
@node(person:Banner/Tanja)
{
    name = @id
}
```

05. Query with multiple entity paths intersected together returning two sets of entries.
The first for all persons and the second for all locations that match the intersection.
```
query data
@nodes(location:DE/Berlin//", mode: "Intersect")
@nodes(time:2012//)
@node(person:Banner/Peter)
{
    person = @nodes(\\Contacts\\)
    {
        firstname = @id
        lastname = @id(\\)
    },
    location = @nodes(\\Location\\)
    {
        name = @id(\\),
    }
}
```

06. Unnamed query.
```
data
{
    #location = @nodes(location:DE/Berlin//", mode: "Intersect")
    #time = @nodes(time:2012//)
    person = @nodes(person:Banner/Peter)
    {
        firstname
        lastname = @id(\\#FamilyName)
    }
}
```

07. Unnamed query.
```
data
{
    #location = @nodes(location:DE/Berlin/*", mode: "Intersect")
    #time = @nodes(time:2012/*)
    person = @nodes(person:Banner/*)
    {
        firstname
        lastname = @id(\\)
    }
}
```

08. Unnamed query.
query data
{
    id = @nodes(/&2932-232DA2-3F09D-45EC38-8530-A8312.45EC38-2932-232DA2-3F09D-32EC38.2932-232DA2-232DA2-3F09D.93212.54534.9423)
    {
        firstname = @id
        lastname = @id(\\)
    }
}
```

09. Unnamed query.
```
query data
{
    id = @nodes(/$id)
    {
        firstname = @id
        lastname = @id(\\)
    }
}
```

05. Merge two distinct sets of nodes traversed.
```
Data
{
    Person
    @node(person:Doe/John)
    @nodes(location:DE/Berlin//)
    @nodes(time:2009//)
    @union(/Locations/","/Visitors/", "/Events/)
    {
        FirstName = @node(),
        LastName = @node(\#FamilyName),
        NickName
    }
}
```

05. Merge two distinct sets of nodes traversed.
Data
{
    Person
    = person:Doe/John
    [] = location:DE/Berlin//
    [] = time:2009//
    @union(/Locations/","/Visitors/", "/Events/)
    {
        FirstName = @,
        LastName = \#FamilyName,
        NickName
    }
}
```

05. Merge two distinct sets of nodes traversed.
```
Data
{
    Person
    = @node(person:Doe/John)
    [] = @nodes(location:DE/Berlin//)
    [] = @nodes(time:2009//)
    @union(/Locations/","/Visitors/", "/Events/)
    {
        FirstName = @node(),
        LastName = @node(\#FamilyName),
        NickName
    }
}
```

02 Returns two different arrays in one single response. Limit the results to the union of the two.
```
Person = @node(Person:Doe/John)
{
    FirstName = @node(),
    LastName = @node(\#FamilyName),
    Track = @nodes(/Track/)
    {
        Name = @node(),
        Time = @where(Time:2000/**),
        Location = @where(Location:Europe/Germany/Berlin)
    }
}
```

02 Returns two different arrays in one single response. Limit the results to the union of the two.
```
Person = Person:Doe/John
{
    FirstName = @node(),
    LastName = @node(\#FamilyName),
    Track[] = @nodes(/Track/)
    {
        Name = @node(),
        Time = @where(Time:2000/**),
        Location = @where(Location:Europe/Germany/Berlin)
    }
}
```

05. Merge two distinct sets of nodes traversed.
```
Data
{
    Person
    = person:Doe/John
    [] = location:DE/Berlin//
    [] = time:2009//
    @union(/Locations/","/Visitors/", "/Events/)
    {
        FirstName = @,
        LastName = \#FamilyName,
        NickName
    }
}
```

01 Returns a query for the given variables. The query won't execute if the variables aren't provided.
```
Person = @nodes(Person:$LastName/$FirstName)
{
    FirstName = @node(),
    LastName = @node(\#FamilyName),
    NickName,
    Friends = @nodes(/Friends/)
}
```

01 Add a schema.
```
@schema-add("contact.names")
{
    FirstName = @node(),
    LastName = @node(\#FamilyName),
    NickName,
}
```
