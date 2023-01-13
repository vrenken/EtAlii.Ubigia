// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.MicroContainer.Tests;

public class FactoryImplementation : Factory<IFourthParent>
{
    protected override IScaffolding[] CreateScaffoldings() => new IScaffolding[] { new ScaffoldingImplementation() };
}
