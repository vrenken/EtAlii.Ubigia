// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Logical;

    internal class ItemToPathSubjectConverter : IItemToPathSubjectConverter
    {
        public PathSubject Convert(object items)
        {
            return items switch
            {
                PathSubject item => item,
                INode node => new RelativePathSubject(new ConstantPathSubjectPart(node.Type)), // << ????
                string s => new RelativePathSubject(new ConstantPathSubjectPart(s)),
                _ => throw new ScriptParserException($"Unable to convert path subject: {items ?? "NULL"}")
            };
        }

        public bool TryConvert(object items, out PathSubject pathSubject)
        {
            pathSubject = items switch
            {
                PathSubject item => item,
                INode node => new RelativePathSubject(new ConstantPathSubjectPart(node.Type)), // << ????
                string s => new RelativePathSubject(new ConstantPathSubjectPart(s)),
                _ => null
            };

            return pathSubject != null;
        }
    }
}
