# GTL variables

Union of items
```
(NOT YET IMPLEMENTED)
$i3 <= $i1 & $i2
$emailsFromJohnDoe <= $i4 & $i5
```

Combination of items
```
(NOT YET IMPLEMENTED)
$e3 <= $e1 | $e2
```

Output of variable.
```gtl
$e3
```

Single Item variable assignment
```gtl
$e1 <= /Documents/Files1
$e2 <= /Documents/Files2
```

Multiple Items variable assignment
```gtl
$i1 <= /Documents/Files1/
$i2 <= /Documents/Files2/
$i4 <= /Communication/Email/Google/
$i5 <= /Person/Banner/Peter/
```

Identifier based variable assignment
```gtl
$i7 <= /&38A52BE4-9352-453E-AF97-5C3B448652F0.3F2504E0-4F89-41D3-9A0C-0305E82C3301.21EC2020-3AEA-4069-A2DD-08002B30309D.20.30.40
```

Identifier started path variable assignment
```gtl
$i8 <= /&38A52BE4-9352-453E-AF97-5C3B448652F0.3F2504E0-4F89-41D3-9A0C-0305E82C3301.21EC2020-3AEA-4069-A2DD-08002B30309D.20.30.40/Images
```

Identifier variable path variable assignment
```gtl
$i9 <= /&38A52BE4-9352-453E-AF97-5C3B448652F0.3F2504E0-4F89-41D3-9A0C-0305E82C3301.21EC2020-3AEA-4069-A2DD-08002B30309D.20.30.40
$i10 <= /$i9/Images
```
