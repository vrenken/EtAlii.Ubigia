# Ubigia Graph Context Language - Introduction

Accessing databases is though. Especially when they incorporate multi-dimensional data.
Similar like other technologies Ubigia tries to make the development experience somewhat lighter.
For this it uses a combination of a expressive schema-language which gets converted into source code to simplify
client-side data interaction.

The principles that power the GCL can be distinctly split into four separate views. First, we have **Result structuring**,
which ensures that the complex data from the graph storage gets quantified into simple, understandable chunks of data that a
application function can process. Secondly, there is the so called method of **Graph mapping**, with which parts of the graph
can be mapped onto the defined result structure. These mappings in essence project **Graph Traversal** Language (GTL) sequences onto
the output fields and structures. Finally, as data access cannot be solely read-only there is the concept of **Graph mutation**,
which makes the CRUD activities of the Ubigia system complete.

1. [Ubigia Graph Context Language - Introduction](Gcl.Introduction.md)
2. Schema concepts
    - [Result Structuring](Gcl.ResultStructuring.md)
    - [Graph Mapping](Gcl.GraphMapping.md)
    - [Graph Traversal](Gtl.Introduction.md)
    - [Graph Mutation](Gcl.GraphMutation.md)
    - [Additional examples](Gcl.AdditionalExamples.md)
3. Code generation
    - Implementation
4. Usage
5. Tips 'n tricks

More reasoning about the GCL and its formal specification can be found [here](Gcl.Specification.md).



