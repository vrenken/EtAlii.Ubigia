# Graph Context Language - Introduction

The principles that power the GCL can be distinctly split into four separate views. First, we have **Result structuring**,
which ensures that the complex data from the graph storage gets quantified into simple, understandable chunks of data that a
application function can process. Secondly, there is the so called method of **Graph mapping**, with which parts of the graph
can be mapped onto the defined result structure. These mappings in essence project **Graph Traversal** Language (GTL) sequences onto
the output fields and structures. Finally, as data access cannot be solely read-only there is the concept of **Graph mutation**,
which makes the CRUD activities of the Ubigia system complete.

1. [Results Structuring](Gcl.Schema.Results.md)
2. [Graph Mapping](Gcl.Schema.Mapping.md)
3. [Graph Traversal](Gtl.Basics.md)
4. [Graph Mutation](Gcl.Schema.Mutations.md)

More reasoning about the GCL and its formal specification can be found [here](Gcl.Specification.md).



