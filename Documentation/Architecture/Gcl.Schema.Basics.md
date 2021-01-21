# Graph Context Language - Introduction

The Graph Context Language is one of the key components that makes distributed traversal of immutable graph data a reality. 
It is designed using a few guiding principles:

1. We need to be able to traverse all dimensions of graph data, that is: hierarchical, temporal and sequential data. For this the GTL should be fully accepted.
2. Graph data results probably never are flat, therefore the GCL should allow results to be expressed using complex compositional patterns. 
3. Complexity should be reduced using code generators for all supported languages. 
4. The GCL should support expressing the plurality of data. 
5. The GCL should support expressing data to be both optional as wel as mandatory.
6. The GCL should optionally support expressing the atomic type of data - the default is reverting back to strings.

On an abstract leven the GCL is actually a schema with which data can both be queried or altered. As all data in a Ubigia store is immutable it is adviced to rather talk about 'mutations'.


- [Results structuring](Gcl.Schema.Results.md)
- [Graph traversal mapping](Gcl.Schema.Mapping.md)
- [Graph mutations](Gcl.Schema.Mutations.md)




