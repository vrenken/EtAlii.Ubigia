01. Assign a constant to a graph node property and map it to a GCL property.
This query __will not__ trigger a data update graph query.
```gcl
Person = @node(Person:Doe/John)
{
    NickName = "Johnny"
}
```

02. Assign a variable to a graph node property and map it to a GCL property.
This query __will not__ trigger a data update graph query.
```gcl
Person = @node(Person:Doe/John)
{
    NickName = $nickName
}
```

03. Assign a variable to a graph node property and map it to a GCL property.
The type of the GCL property will be used to verify and process the variable.
This query __will__ trigger a data update graph query (due to the assignment of the birthday variable to the graph node property).
```gcl
Person = @node(Person:Doe/John)
{
    string NickName = $nickName
    datetime Birthday = .BirthDate <= $birthday
}
```

