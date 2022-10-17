01. Sets the value of an existing node.
Throws an error if multiple nodes are found.
```gcl
Person = @node(Person:Doe/John)
{
    NickName = @node-set("Johnny")
}
```

02. Clear a property on an existing node.
Throws an error if multiple nodes are found.
```gcl
Person = @node(Person:Doe/John)
{
   FirstName = @node-clear()
}
```

03. Changes the name of an existing node.
Throws an error if multiple nodes are found.
```gcl
Person = @node(Person:Doe/Johhn)
{
    FirstName = @node-set("John")
}
```

04. Add a new node and give it some properties.
```gcl
Person = @node-add(Person:Doe, Jane)
{
    NickName = @node-set("Jahney")
}
```

05.a Add a child node and return everything.
```gcl
Person = @node(Person:Doe/John)
{
    FirstName = @node(),
    LastName = @node(\#FamilyName),
    NickName,
    Friend = @nodes-link(/Friends, Person:Stark/Tony, /Friends)
    {
        FirstName = @node(),
        LastName = @node(\#FamilyName),
        NickName
    }
}
```

05.b Add a child node and return everything.
```gcl
Data
{
    Person = @node(Person:Doe/John)
    {
        FirstName = @node(),
        LastName = @node(\#FamilyName),
        NickName,
        Friend = @nodes-link(/Friends, Person:Banner/Peter, /Friends)
        {
            FirstName = @node(),
            LastName = @node(\#FamilyName),
            NickName
        }
    }
}
```

06 Link two nodes and return everything.
```gcl
Data
{
    Person = @node(Person:Doe/John)
    {
        FirstName = @node(),
        LastName = @node(\#FamilyName),
        NickName,
        Friend = @nodes-link(/Friends, Person:Banner/Peter, /Friends)
        {
            FirstName = @node(),
            LastName = @node(\#FamilyName),
            NickName
        }
    },
    Person = @node(Person:Banner/Peter)
    {
        FirstName = @node(),
        LastName = @node(\#FamilyName),
        NickName,
        Friend = @nodes(/Friends)
        {
            FirstName = @node(),
            LastName = @node(\#FamilyName),
            NickName
        }
    }
}
```
