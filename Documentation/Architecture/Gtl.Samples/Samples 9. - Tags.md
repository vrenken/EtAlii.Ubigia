# GTL Tags

Tag assignment.
```gtl
Person:Doe# <= FamilyName
/Person/Doe# <= FamilyName
Person:Doe/John# <= FirstName
/Person/Doe/John# <= FirstName
```

Tag query.
```gtl
Person:Doe#
/Person/Doe#
Person:Doe/John#
/Person/Doe/John#
```

Filter query
```gtl
Person:#FamilyName
/Person/#FamilyName
Person:Doe/*\#FamilyName
/Person/Doe/*\#FamilyName
```
