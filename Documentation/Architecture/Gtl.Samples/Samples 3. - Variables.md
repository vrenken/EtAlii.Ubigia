﻿# GTL variables

Union of items
```gtl
$i3 <= $i1 & $i2

$emailsFromPeter <= $i4 & $i5

-- Combination of items
$e3 <= $e1 | $e2

-- Output of variable.
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
$i7 <= /&2932-232DA2-3F09D-45EC38-8530-A8312.45EC38-2932-232DA2-3F09D-32EC38.2932-232DA2-232DA2-3F09D.93212.54534.9423
```

Identifier started path variable assignment
```gtl
$i8 <= /&2932-232DA2-3F09D-45EC38-8530-A8312.45EC38-2932-232DA2-3F09D-32EC38.2932-232DA2-232DA2-3F09D.93212.54534.9423/Images
```

Identifier variable path variable assignment
```gtl
$i9 <= /&2932-232DA2-3F09D-45EC38-8530-A8312.45EC38-2932-232DA2-3F09D-32EC38.2932-232DA2-232DA2-3F09D.93212.54534.9423
$i10 <= /$i9/Images
```