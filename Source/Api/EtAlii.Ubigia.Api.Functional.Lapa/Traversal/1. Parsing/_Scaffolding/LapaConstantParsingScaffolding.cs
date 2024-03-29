﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using EtAlii.xTechnology.MicroContainer;

internal sealed class LapaConstantParsingScaffolding : IScaffolding
{
    public void Register(IRegisterOnlyContainer container)
    {
        // Constant helpers
        container.Register<INodeValidator, NodeValidator>();
        container.Register<INodeFinder, NodeFinder>();
        container.Register<IConstantHelper, ConstantHelper>();
        container.Register<INewLineParser, NewLineParser>();
        container.Register<IWhitespaceParser, WhitespaceParser>();
        container.Register<IKeyValuePairParser, KeyValuePairParser>();
        container.Register<IQuotedTextParser, QuotedTextParser>();
        container.Register<IBooleanValueParser, BooleanValueParser>();
        container.Register<IIntegerValueParser, IntegerValueParser>();
        container.Register<IFloatValueParser, FloatValueParser>();
        container.Register<IDateTimeValueParser, DateTimeValueParser>();
        container.Register<ITimeSpanValueParser, TimeSpanValueParser>();
        container.Register<ITypeValueParser, TypeValueParser>();
    }
}
