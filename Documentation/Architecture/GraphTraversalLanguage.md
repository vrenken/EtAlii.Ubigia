# Ubigia Graph Traversal Language.

One of Ubigia's core intents is to enable multi-dimensional information entanglement. 
After investigating today's graph/document databases and the query languages they provide it became clear that they weren't quite capable of querying information linked on 3 dimensions.
Most of them did not even honour time as a first-class data citizen (i.e. mutations/version-control) and only provided the possibility to store dates and times as atomic values without any related capabilities.

What also became clear is that the first means to access a ubiquitous information store should not be a query-language, foremost important is a way to traverse the linked information in a structured and enabling way.
For this Ubigia was first designed to facilitate a "Graph Traversal Language". The Ubigia GTL is a language that understands the intrinsics of the 3 dimensions (time/hierarchy/sequence) and allows the user to define traversals over the linked information entities.

Surely good recipe's should be honoured, and for this reason the GTL definition closely relates to how URI (Unified Resource Identifiers) are built. 
A few simple example can be seen below:

```
// Find a person with the first name John and last name Doe.
$person => person:Doe/John

// Find all persons with the last name Doe.
$persons => person:Doe/*

// Get the current time.
$time => time:Now

// Get the time defined in the specified graph segment.
$time => time:2020/10/11/12/23
// Parse the time specified.
$time => time:2014-08-26T11:12:23

// Get the location defined in the specified graph segment.
$location => location:Netherlands/Overijssel/Enschede
$location => location:Germany/Berlin/Center/"Zoologischen Garten"
$location => location:DE/Berlin/Center/"Zoologischen Garten"
$location => location:NL/Enschede
$location => location:NL/Enschede/Lavenhorsthoek/23
// Parse the given location.
$location => location:52.2167/6.9000
```

These examples are fairly straightforward, but only cover a few of the most simple traversals possible. 

## Sequences.
A GTL script is made up of sequences. Most often a sequence is placed on a single line, but with object assignments it is possible that a sequence covers multiple lines.
Each sequence is build using _subjects_ and _operators_. Subjects are the content that the sequence works on according to the operators that separates them.

For example:
```
$var1   <=       "Hello World"
SUBJECT OPERATOR SUBJECT
```

In the example above the <= indicates the assignment operator, "Hello World" a constant subject and $var1 a variable subject.

A sequence is first parsed into its components. From these an execution plan is determined, which can then be run on the data storage. 
As in Ubigia information never gets destroyed the operators needed are relative few:

OPERATORS
<=
+= 
-=

SUBJECTS


## Different types of traversals.
Traversing information has one logical aspect that cannot be overlooked: it needs to start somewhere. In Ubigia there are two different "sequences" that both have a profound purpose:

- *Absolute paths*<br/>
  An absolute path has as purpose to start exploring linked information from the outside-in. It always starts at a root (i.e. time, person, location) and from there on traverses inwards. 
  
- *Relative paths*<br/>
  A relative path has as purpose to continue traversal from an already found information element or elements. It's purpose is to explore surroundings of related information items and by doing so facilitate better understanding (and build context). 

## Directional traversal.

- *Hierarchical*<br/>
  ...
  
- *Temporal*<br/>
  ...

- *Sequential*<br/>
  ...


## Roots
  

