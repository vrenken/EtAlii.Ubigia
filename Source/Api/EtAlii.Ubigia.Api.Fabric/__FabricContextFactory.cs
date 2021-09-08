// // Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia
//
// namespace EtAlii.Ubigia.Api.Fabric
// {
//     using EtAlii.xTechnology.MicroContainer;
//
//     public class FabricContextFactory : Factory<IFabricContext, FabricOptions, IExtension>
//     {
//         protected override IScaffolding[] CreateScaffoldings(FabricOptions options)
//         {
//             return new IScaffolding[]
//             {
//                 new ContextScaffolding(options),
//                 new EntryContextScaffolding(options.CachingEnabled),
//                 new ContentContextScaffolding(options.CachingEnabled),
//                 new PropertyContextScaffolding(options.CachingEnabled),
//                 new RootContextScaffolding(),
//             };
//         }
//     }
// }
