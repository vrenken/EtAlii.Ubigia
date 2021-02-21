# GTL Operators
- ``<=   assign``
- ``+=   add``
- ``-=   remove``

Removing items.
```gtl
/Documents/Files -= /Readme.txt
/Documents/Files -= /Images
```

Adding one single item.
```gtl
/Documents/Files += file("Readme.txt", "c:\readme.txt")
/Documents/Files += file('Readme.txt', 'c:\readme.txt')
/Documents/Files += /Images
$i6 <= /Documents/Files += /Images
```

Add a complete path at once.
```gtl
/Person+=/Doe/John
```

Adding a complete folder.
```gtl
/Documents/Files += folder(c:\Documents)
```

Updating one single item by path.
```gtl
/Documents/Files/Image01 <= $image
```

Clearing the data assigned to an item.
```gtl
/Documents/Files/Image01 <=
```

Updating one single item by ID.
```gtl
/&2932-232DA2-3F09D-45EC38-8530-A8312.45EC38-2932-232DA2-3F09D-32EC38.2932-232DA2-232DA2-3F09D.93212.54534.9423 <= $image
```

Updating one single item by ID in a variable.
```gtl
$i7 <= /&2932-232DA2-3F09D-45EC38-8530-A8312.45EC38-2932-232DA2-3F09D-32EC38.2932-232DA2-232DA2-3F09D.93212.54534.9423
/$i7 <= $image
```
