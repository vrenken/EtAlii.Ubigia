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

1. [Ubigia Graph Context Language - Introduction](Introduction.md)
2. Schema concepts
   - [Result Structuring](ResultStructuring.md)
   - [Graph Mapping](GraphMapping.md)
   - [Graph Traversal](Gtl.Introduction.md)
   - [Graph Mutation](GraphMutation.md)
   - [Additional examples](AdditionalExamples.md)
3. Code generation
   - Implementation
4. Usage
5. Additional examples
   - [0. Introduction - Compact - v2](<Examples/0. - Introduction - compact - v2.md>)
   - [0. Introduction - Elaborate - v2](<Examples/0. - Introduction - elaborate - v2.md>)
   - [1. Queries](<Examples/1. - Queries.md>)
   - [2. Nodes - Compact - v2](<Examples/2. - Nodes - compact - v2.md>)
   - [2. Nodes - Elaborate - v2](<Examples/2. - Nodes - elaborate - v2.md>)
   - [3. Mutations - Properties](<Examples/3. - Mutations - Properties.md>)
   - [3. Mutations - Properties - v2](<Examples/3. - Mutations - Properties - v2.md>)
   - [3. Mutations - Properties - Compact - v2](<Examples/3. - Mutations - Properties - compact - v2.md>)
   - [3. Mutations - Properties - Elaborate - v2](<Examples/3. - Mutations - Properties - elaborate - v2.md>)
   - [3. Mutations - Values](<Examples/3. - Mutations - Values.md>)
   - [3. Mutations - Values - v2](<Examples/3. - Mutations - Values - v2.md>)
   - [4. Linking](<Examples/4. - Linking.md>)
   - [4. Linking - v2](<Examples/4. - Linking - v2.md>)
   - [4. Linking - Alternatives](<Examples/4. - Linking - Alternatives.md>)
   - [5. Variables](<Examples/5. - Variables - v2.md>)
   - [6. Experimental scribbles](<Examples/7. - Scribbles.md>)
   - [8. Typed results](<Examples/8. - Typed results.md>)
6. Tips 'n tricks

More reasoning about the GCL and its formal specification can be found [here](Gcl.Specification.md).



