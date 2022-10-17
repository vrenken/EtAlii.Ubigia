# Graph Context Language specification

The Graph Context Language is one of the key components that makes distributed traversal of immutable graph data a reality.
It is designed using a few guiding principles:

1. We need to be able to traverse all dimensions of graph data, that is: hierarchical, temporal and sequential data. For this the GTL should be fully accepted.
2. Graph data results probably never are flat, therefore the GCL should allow results to be expressed using complex compositional patterns.
3. Complexity should be reduced as much as possible using code generators for all supported languages.
4. The GCL should support expressing the plurality of expected data.
5. The GCL should support expressing data to be both optional as wel as mandatory.
6. The GCL should optionally support expressing the atomic type of data - the default is reverting back to strings.

On an abstract leven the GCL is actually a sort of micro-schema with which persisted data can both be queried or altered.

[IMAGE] to clarify

Thinking about micro-schema's has another consequence: running them after each other has similarities with a 'view' that
slowly but steadily adopts to the users' needs. for this it both **changes shape** (expanding the scope or reducing it), and
**moves to other parts of the data**, when the context of the user changes.

[IMAGE] to clarify



## Fragments
A GCL schema is build using fragments.

Each fragment can either be a value fragment or a structure fragment.

Each fragment has a name by which it can be identified.


## Annotations

Each fragment can have a optional annotation.

Each fragment can have a mandatory annotation.

A fragment can either have a optional or a mandatory annotation, not both.

Structure fragments can have a plurality annotation.

