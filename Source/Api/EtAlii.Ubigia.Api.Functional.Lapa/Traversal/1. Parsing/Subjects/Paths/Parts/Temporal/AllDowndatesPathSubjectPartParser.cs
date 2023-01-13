// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using Moppet.Lapa;

internal sealed class AllDowndatesPathSubjectPartParser : IAllDowndatesPathSubjectPartParser
{
    public string Id => nameof(AllDowndatesPathSubjectPart);

    public LpsParser Parser { get; }

    private readonly INodeValidator _nodeValidator;

    private const string RelationId = @"{{";
    private const string RelationDescription = @"ALL_DOWNDATES_OF";

    public AllDowndatesPathSubjectPartParser(
        INodeValidator nodeValidator,
        IPathRelationParserBuilder pathRelationParserBuilder)
    {
        _nodeValidator = nodeValidator;

        var relationParser = pathRelationParserBuilder.CreatePathRelationParser(RelationDescription, RelationId);
        Parser = new LpsParser(Id, true, relationParser);
    }

    public PathSubjectPart Parse(LpNode node)
    {
        _nodeValidator.EnsureSuccess(node, Id);
        return new AllDowndatesPathSubjectPart();
    }

    public bool CanParse(LpNode node)
    {
        return node.Id == Id;
    }
}
