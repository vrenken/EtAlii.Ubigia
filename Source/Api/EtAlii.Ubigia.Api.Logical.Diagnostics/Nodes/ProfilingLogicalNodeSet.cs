// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Diagnostics.Profiling;

    public sealed class ProfilingLogicalNodeSet : ILogicalNodeSet
    {
        private readonly ILogicalNodeSet _decoree;
        private readonly IProfiler _profiler;

        public ProfilingLogicalNodeSet(ILogicalNodeSet decoree, IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Logical.NodeSet);
        }

        public void SelectMany(GraphPath path, ExecutionScope scope, IObserver<object> output)
        {
            dynamic profile = _profiler.Begin("SelectMany: " + path);
            profile.Path = path.ToString();

            _decoree.SelectMany(path, scope, output);

            _profiler.End(profile);
        }

        public async Task<IReadOnlyEntry> SelectSingle(GraphPath path, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("SelectSingle: " + path);
            profile.Path = path.ToString();

            var result = await _decoree
                .SelectSingle(path, scope)
                .ConfigureAwait(false);

            _profiler.End(profile);

            return result;
        }

        public async Task<IReadOnlyEntry> AssignProperties(Identifier location, IPropertyDictionary properties, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Assign properties");
            profile.Properties = properties.ToString();
            profile.Location = location.ToString();

            var result = await _decoree.AssignProperties(location, properties, scope).ConfigureAwait(false);

            _profiler.End(profile);

            return result;
        }

        public async Task<IReadOnlyEntry> AssignTag(Identifier location, string tag, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Assign tag");
            profile.Tag = tag;
            profile.Location = location.ToString();

            var result = await _decoree.AssignTag(location, tag, scope).ConfigureAwait(false);

            _profiler.End(profile);

            return result;
        }

        public async Task<IReadOnlyEntry> AssignNode(Identifier location, Node node, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Assign node");
            profile.Node = node;
            profile.Location = location.ToString();

            var result = await _decoree.AssignNode(location, node, scope).ConfigureAwait(false);

            _profiler.End(profile);

            return result;
        }

        public async Task<IReadOnlyEntry> AssignDynamic(Identifier location, object o, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Assign dynamic");
            profile.Object = o.ToString();
            profile.Location = location.ToString();

            var result = await _decoree.AssignDynamic(location, o, scope).ConfigureAwait(false);

            _profiler.End(profile);

            return result;
        }

        public async Task<IReadOnlyEntry> Add(Identifier parent, string child, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Add: " + child);
            profile.Parent = parent.ToString();
            profile.Child = child;

            var result = await _decoree.Add(parent, child, scope).ConfigureAwait(false);

            _profiler.End(profile);

            return result;
        }

        public async Task<IReadOnlyEntry> Add(Identifier parent, Identifier child, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Add");
            profile.Parent = parent.ToString();
            profile.Child = child.ToString();

            var result = await _decoree.Add(parent, child, scope).ConfigureAwait(false);

            _profiler.End(profile);

            return result;
        }

        public async Task<IReadOnlyEntry> Remove(Identifier parent, string child, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Remove: " + child);
            profile.Parent = parent.ToString();
            profile.Child = child;

            var result = await _decoree.Remove(parent, child, scope).ConfigureAwait(false);

            _profiler.End(profile);

            return result;
        }

        public async Task<IReadOnlyEntry> Remove(Identifier parent, Identifier child, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Remove");
            profile.Parent = parent.ToString();
            profile.Child = child.ToString();

            var result = await _decoree.Remove(parent, child, scope).ConfigureAwait(false);

            _profiler.End(profile);

            return result;
        }

        public async Task<IReadOnlyEntry> Rename(Identifier item, string newName, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Rename: " + newName);
            profile.Item = item.ToString();
            profile.NewName = newName;

            var result = await _decoree.Rename(item, newName, scope).ConfigureAwait(false);

            _profiler.End(profile);

            return result;
        }

        public async Task<IReadOnlyEntry> Link(Identifier location, string itemName, Identifier item, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Link: " + itemName);
            profile.Location = location.ToString();
            profile.ItemName = itemName;
            profile.Item = item.ToString();

            var result = await _decoree.Link(location, itemName, item, scope).ConfigureAwait(false);

            _profiler.End(profile);

            return result;
        }

        public async Task<IReadOnlyEntry> Unlink(Identifier location, string itemName, Identifier item, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Unlink: " + itemName);
            profile.Location = location.ToString();
            profile.ItemName = itemName;
            profile.Item = item.ToString();

            var result = await _decoree.Unlink(location, itemName, item, scope).ConfigureAwait(false);

            _profiler.End(profile);

            return result;
        }

        public async Task<IEditableEntry> Prepare()
        {
            dynamic profile = _profiler.Begin("Prepare");

            var result = await _decoree.Prepare().ConfigureAwait(false);

            _profiler.End(profile);

            return result;
        }

        public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Change");

            var result = await _decoree.Change(entry, scope).ConfigureAwait(false);

            _profiler.End(profile);

            return result;
        }
    }
}
