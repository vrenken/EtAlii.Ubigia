﻿namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Logical;

    public class ProfilingLogicalNodeSet : ILogicalNodeSet
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


        public async Task<INode> AssignProperties(Identifier location, IPropertyDictionary properties, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Assign properties");
            profile.Properties = properties.ToString();
            profile.Location = location.ToString();

            var result = await _decoree.AssignProperties(location, properties, scope);

            _profiler.End(profile);

            return result;
        }

        // TODO: The Assignment result should be a IReadOnlyEntry and not an INode.
        public async Task<INode> AssignTag(Identifier location, string tag, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Assign tag");
            profile.Tag = tag;
            profile.Location = location.ToString();

            var result = await _decoree.AssignTag(location, tag, scope);

            _profiler.End(profile);

            return result;
        }

        // TODO: The Assignment result should be a IReadOnlyEntry and not an INode.
        public async Task<INode> AssignNode(Identifier location, INode node, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Assign node");
            profile.Node = node;
            profile.Location = location.ToString();

            var result = await _decoree.AssignNode(location, node, scope);

            _profiler.End(profile);

            return result;
        }

        // TODO: The Assignment result should be a IReadOnlyEntry and not an INode.
        public async Task<INode> AssignDynamic(Identifier location, object o, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Assign dynamic");
            profile.Object = o.ToString();
            profile.Location = location.ToString();

            var result = await _decoree.AssignDynamic(location, o, scope);

            _profiler.End(profile);

            return result;
        }

        public async Task<IReadOnlyEntry> Add(Identifier parent, string child, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Add: " + child);
            profile.Parent = parent.ToString();
            profile.Child = child;

            var result = await _decoree.Add(parent, child, scope);

            _profiler.End(profile);

            return result;
        }

        public async Task<IReadOnlyEntry> Add(Identifier parent, Identifier child, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Add");
            profile.Parent = parent.ToString();
            profile.Child = child.ToString();

            var result = await _decoree.Add(parent, child, scope);

            _profiler.End(profile);

            return result;
        }

        public async Task<IReadOnlyEntry> Remove(Identifier parent, string child, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Remove: " + child);
            profile.Parent = parent.ToString();
            profile.Child = child;

            var result = await _decoree.Remove(parent, child, scope);

            _profiler.End(profile);

            return result;
        }

        public async Task<IReadOnlyEntry> Remove(Identifier parent, Identifier child, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Remove");
            profile.Parent = parent.ToString();
            profile.Child = child.ToString();

            var result = await _decoree.Remove(parent, child, scope);

            _profiler.End(profile);

            return result;
        }

        public async Task<IReadOnlyEntry> Rename(Identifier item, string newName, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Rename: " + newName);
            profile.Item = item.ToString();
            profile.NewName = newName;

            var result = await _decoree.Rename(item, newName, scope);

            _profiler.End(profile);

            return result;
        }

        public async Task<IReadOnlyEntry> Link(Identifier location, string itemName, Identifier item, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Link: " + itemName);
            profile.Location = location.ToString();
            profile.ItemName = itemName;
            profile.Item = item.ToString();

            var result = await _decoree.Link(location, itemName, item, scope);

            _profiler.End(profile);

            return result;
        }

        public async Task<IReadOnlyEntry> Unlink(Identifier location, string itemName, Identifier item, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Unlink: " + itemName);
            profile.Location = location.ToString();
            profile.ItemName = itemName;
            profile.Item = item.ToString();

            var result = await _decoree.Unlink(location, itemName, item, scope);

            _profiler.End(profile);

            return result;
        }

        public async Task<IEditableEntry> Prepare()
        {
            dynamic profile = _profiler.Begin("Prepare");

            var result = await _decoree.Prepare();

            _profiler.End(profile);

            return result;
        }

        public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Change");

            var result = await _decoree.Change(entry, scope);

            _profiler.End(profile);

            return result;
        }
    }
}