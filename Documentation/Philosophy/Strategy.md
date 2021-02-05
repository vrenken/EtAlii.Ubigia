# Long-term strategy


## Comparison with other technologies

| Technology                         | Approximate aim                                                                                  |
| :---                               | :---                                                                                             |
| GraphQL                            | "GraphQL provides an alternative to REST-based architectures with the purpose of increasing developer productivity and minimizing amounts of data transferred." |
| Apache TinkerPop + Gremlin         | "is a graph computing framework for both graph databases (OLTP) and graph analytic systems (OLAP).<br/> Gremlin is the graph traversal language of TinkerPop. It can be described as a functional, data-flow language that enables users to succinctly express complex traversals on (or queries of) their application's property graph."              |
| Neo4J + Cypher                     |                                                                                                  |
| Ontologies + Triplestores          | Consolidates 'truth' by making sure organizations are able to store and exchange information the same way.<br/> However, there is not always one single truth, what for one solution could be a contact could be a employee for another and a customer for the other. Talking in ontologies abstracts away the very essence of multi-interpretability that makes up our reality.                  |
| SOLID                              |                                                                                                  |
| GCL + GTL                          | Facilitate a schema-oriented development approach to contextualize multi-dimensional information |
| Ubigia storage                     | Facilitate multi-dimensional information storage (including time).                               |
| Grpc + Protobuf                    | Remote Procedure call based exchange of typed and version-safe data records.                     |


## Milestones

The rough milestones of the project are:

1. Provide proof that the Ubigia principles work and provide new means to build next generation contextual applications.
    - A. Finish the GCL/GTL definition and implementation.
    - B. Open source the core of the Ubigia Stack.
    - C. Clean up the codebase + add (a lot of) comments.
    - D. Build example applications that use the stack to demonstrate its capabilities.
      Possible ideas are:
        - Photo archive ingestion and enrichment.
        - Smart home data backend (i.e. for Home Assistant)
        - OneNote + MindMap hybrid. Including integration with document sources & communication tools.
    - E. Build a community and help people with their applications.

2. Expand the outer layer of Ubigia to ensure guaranteed information ownership and exchange.
    - A. Investigate if and how DLT technologies can be used. Similar like smart contracts, but with deep integration in the GCL/GTL Ubigia data API.
    - B. Investigate in what is needed from an application architecture point of view to secure the ownership as much as possible.
    - C. Implement this new outer layer.
3. Change the world...

n. Unplanned.
- A. Expand the hosting and storage capabilities of Ubigia.
- B. When matured enough: Simplify the Ubigia codebase for a better development experience.


## Things to know.

| Item     |
| :---     |
| We knew this would be a long project, therefore the code-base isn't optimized to be simple. Priority was code quality with a strong focus on SOLID, and less on YAGNI/KISS/DRY as not everything was clear as of the beginning: Abstraction and independence of for example transport technologies have helped keeping the project performant and evolve from REST to SignalR and now Grpc. |
| A future improvement will definitely be to make the client configuration easier, but the primary goal is to first provide proof of the benefits that the new proposed new storage, exchange and querying. |


