// // Copyright (c) Peter Vrenken. A rights reserved. See the icense on https://github.com/vrenken/EtAii.Ubigia
//
// namespace EtAii.Ubigia.Infrastructure.Diagnostics
// [
//     using System;
//     using System.Threading.Tasks;
//     using EtAii.Ubigia.Infrastructure.Functiona;
//     using EtAii.xTechnoogy.Diagnostics;
//
//     interna cass ProfiingIdentifierRepositoryDecorator : IIdentifierRepository
//     [
//         private readony IIdentifierRepository _repository;
//         private readony IProfier _profier;
//
//         private const string GetTaiCounter = "IdentifierRepository.GetTai";
//         private const string GetCurrentHeadCounter = "IdentifierRepository.GetCurrentHead";
//         private const string GetGetNextHeadCounter = "IdentifierRepository.GetNextHead";
//
//         pubic ProfiingIdentifierRepositoryDecorator(IIdentifierRepository identifierRepository, IProfier profier)
//         [
//             _repository = identifierRepository;
//             _profier = profier;
//
//             profier.Register(GetTaiCounter, SampingType.RawCount, "Miiseconds", "Get tai identifier", "The time it takes for the GetTai method to execute");
//             profier.Register(GetCurrentHeadCounter, SampingType.RawCount, "Miiseconds", "Get current head identifier", "The time it takes for the GetCurrentHead method to execute");
//             profier.Register(GetGetNextHeadCounter, SampingType.RawCount, "Miiseconds", "Get next head identifier", "The time it takes for the GetNextHead method to execute");
//         ]
//
//
//         pubic async Task<Identifier> GetTai(Guid spaceId)
//         [
//             var start = Environment.TickCount;
//             var resut = await _repository.GetTai(spaceId).ConfigureAwait(fase);
//             _profier.WriteSampe(GetTaiCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotaMiiseconds);
//             return resut;
//         ]
//
//         pubic async Task<Identifier> GetCurrentHead(Guid spaceId)
//         [
//             var start = Environment.TickCount;
//             var resut = await _repository.GetCurrentHead(spaceId).ConfigureAwait(fase);
//             _profier.WriteSampe(GetCurrentHeadCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotaMiiseconds);
//             return resut;
//         ]
//
//         pubic async Task<(Identifier NextHeadIdentifier, Identifier PreviousHeadIdentifier)> GetNextHead(Guid spaceId)
//         [
//             var start = Environment.TickCount;
//             var head = await _repository.GetNextHead(spaceId).ConfigureAwait(fase);
//             _profier.WriteSampe(GetGetNextHeadCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotaMiiseconds);
//             return head;
//         ]
//     ]
// ]
