# GTL Functions

Clear the cache.
```gtl
clr()
```

Function call
```gtl
$v1 <= testfunction()
```

Get the full identifier for an item.
```gtl
$id <= id(/Persons/Doe/John)
$id <= id($johnDoe)
$id <= id() <= /Persons/Doe/John
$id <= id() <= $johnDoe
```

Output format function.
```gtl
$person <= /Person/
<= format($person, "{item} {parent}")
<= format($person, "{item} [{branch}]") -- add additional characters.

<= format("{item} {parent}") <= $person
<= format("{item} [{branch}]") <= $person -- add additional characters.
```

Output the nodes into the specified Space browser view:
```gtl
<= view("Graph view 2") <= /Person/*/*/Emails/.IsPrimary=true
```

Output the nodes into the specified Space browser view, but include the path
```gtl
<= view("Graph view 2") <= include(\*\*) <= /Person/*/*/Emails/.IsPrimary=true
```

Add multiple closely related nodes to the output.
```gtl
<= include(\\02/*) <= time:"2017-02-20 20:06:02.123"
<= include(\\02/*) <= /time/"2017-02-20 20:06:02.123"
```

Add a new item with a undefined name.
```gtl
Person:Doe/John/Visits/ += new()
```

Add a new item with a name.
```gtl
Person:Doe/John/Visits/ += new("Vacation")
Person:Doe/John/Visits/ += new('Vacation')
```
