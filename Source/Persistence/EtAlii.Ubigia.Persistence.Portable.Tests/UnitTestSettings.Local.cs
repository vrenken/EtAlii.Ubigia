// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

using Xunit;

// TODO: The Portable libraries throw exceptions when tests are run in parallel.
// We could fix this but as the portable storage probably won't be used in the future it might not be worth the effort.
[assembly: CollectionBehavior(collectionBehavior: CollectionBehavior.CollectionPerAssembly, DisableTestParallelization = false)]
