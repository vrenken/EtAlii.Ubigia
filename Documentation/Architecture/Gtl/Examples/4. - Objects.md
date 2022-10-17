# GTL Objects

Property assignment

```
(NOT YET SUPPORTED)
/Document/story.Name <= peter
/Document/story.Name <= "peter"
/Document/story.Number <= 42
$story <= /Document/story
$story.Name <= peter
$story.Name <= "peter"
$story.Number <= 42
```

JSON Based property assignment.

```gtl
$story <= { Name: 'peter', Number: 42
}
```

```gtl
$story <= { Name: 'peter', Number: 42 }
```

```gtl
$story <= {
	Name: 'peter',
	Number: 42,
	Boolean: true,
	Float: 23.45,
	DateTime: 26-8-2015 11:23,
	TimeSpan: 10:3:30:28.134
}
```

```gtl
$story <= {
	Name: 'peter',
	Number: 42,
	Boolean: true,
	Float: 23.45,
	DateTime: 26-8-2015 11:23,
	TimeSpan: 10:3:30:28.134
}
```

Putting the bracket on the next line like in the example below isn't supported (yet).
```
$story <=
{
	Name: 'peter',
	Number: 42,
	Boolean: true,
	Float: 23.45,
	DateTime: 26-8-2015 11:23,
	TimeSpan: 10:3:30:28.134
}
```

```gtl
$story <= {
	"Name": "peter",
	"Number": 42,
	"Boolean": true,
	"Float": 23.45,
	"DateTime": 26-8-2015 11:23,
	"TimeSpan": 10:3:30:28.134
}
```

```gtl
$story <= {
	Name: 'peter',
	Number: 42
}
```

```
(NOT YET SUPPORTED)
$var1 += "story"
/Document/ += $var1
$var1.Name <= 'peter'
$var1.Name <= "peter"
$var1.Number <= 42
```
