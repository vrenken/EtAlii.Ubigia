# GTL Operators
- ``<=   assign``
- ``+=   add``
- ``-=   remove``

Removing items.
```gtl
/Documents/Files -= /'Readme.txt'
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
/Documents/Files += folder('c:\Documents')
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
/&38A52BE4-9352-453E-AF97-5C3B448652F0.3F2504E0-4F89-41D3-9A0C-0305E82C3301.21EC2020-3AEA-4069-A2DD-08002B30309D.20.30.40 <= $image
```

Updating one single item by ID in a variable.
```gtl
$i7 <= /&38A52BE4-9352-453E-AF97-5C3B448652F0.3F2504E0-4F89-41D3-9A0C-0305E82C3301.21EC2020-3AEA-4069-A2DD-08002B30309D.20.30.40
/$i7 <= $image
```
