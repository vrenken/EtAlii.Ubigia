// // Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia
//
// namespace EtAlii.Ubigia.Api.Logical
// {
//     using EtAlii.Ubigia.Api.Fabric;
//     using EtAlii.xTechnology.MicroContainer;
//
//     public class PathTraversalContextFactory : Factory<IPathTraversalContext>, IPathTraversalContextFactory
//     {
//         private readonly IFabricContext _fabricContext;
//
//         public PathTraversalContextFactory(IFabricContext fabricContext)
//         {
//             _fabricContext = fabricContext;
//         }
//
//         protected override IScaffolding[] CreateScaffoldings()
//         {
//             return new IScaffolding[]
//             {
//                 new TraversalContextScaffolding(_fabricContext),
//             };
//         }
//     }
// }
