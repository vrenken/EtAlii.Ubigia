01 Returns a query for the given variables. The query won't execute if the variables aren't provided.
```gcl
Person = Person:$LastName/$FirstName
{
    FirstName = @node(),
    LastName = \#FamilyName,
    NickName,
    Friends[] = /Friends/
    {
    }
}
```
