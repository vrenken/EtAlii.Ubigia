// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Diagnostics.Profiling
{
    using System;
    using System.Linq;

    public class Profiler : IProfiler
    {
        public IProfiler Parent => _parentProfiler;
        private readonly Profiler _parentProfiler;

        public IProfiler Previous => _previousProfiler;
        private Profiler _previousProfiler;

        public IProfilingResultStack ResultStack { get; }

        private readonly IParentProfileResultFinder _parentProfileResultFinder;

        public event Action<ProfilingResult> ProfilingStarted;
        public event Action<ProfilingResult> ProfilingEnded;

        public ProfilingAspect[] Aspects
        {
            get => _aspects;
            set
            {
                if (_aspects != value)
                {
                    _aspects = value;
                    AspectsChanged?.Invoke();
                }
            }
        }
        private ProfilingAspect[] _aspects = Array.Empty<ProfilingAspect>();

        public event Action AspectsChanged;

        public ProfilingAspect Aspect { get; }

        public IProfiler Create(ProfilingAspect aspect)
        {
            return new Profiler(this, aspect, ResultStack);
        }

        private Profiler(IProfiler parent, ProfilingAspect aspect, IProfilingResultStack profilingResultStack)
        {
            Aspect = aspect;
            _parentProfiler = (Profiler)parent;
            if (_parentProfiler != null)
            {
                _parentProfiler.AspectsChanged += OnParentProfilerAspectsChanged;
                _aspects = _parentProfiler.Aspects;

                ProfilingStarted += result => _parentProfiler.InvokeProfilingStarted(result);
                ProfilingEnded += result => _parentProfiler.InvokeProfilingEnded(result);
            }
            ResultStack = profilingResultStack;
            _parentProfileResultFinder = new ParentProfileResultFinder();
        }

        public Profiler(IProfiler parent, ProfilingAspect aspect)
            : this(parent, aspect, new ProfilingResultStack())
        {
        }

        public Profiler(ProfilingAspect aspect)
            : this(null, aspect, new ProfilingResultStack())
        {
        }

        private void OnParentProfilerAspectsChanged()
        {
            Aspects = _parentProfiler.Aspects;
        }

        public ProfilingResult Begin(string action)
        {
            var shouldPropagate = _aspects.Contains(Aspect);
            var parentProfile = _parentProfileResultFinder.Find(this);
            var profile = new ProfilingResult(parentProfile, Aspect.Id, Aspect.Layer, action, shouldPropagate);

            if (shouldPropagate)
            {
                ResultStack.Push(profile);
                profile.Start();
                if (parentProfile == null)
                {
                    InvokeProfilingStarted(profile);
                }
            }
            return profile;
        }

        public void End(ProfilingResult profile)
        {
            var shouldPropagate = _aspects.Contains(Aspect);
            if (shouldPropagate)
            {
                if (ResultStack.Peek() != profile)
                {
                    throw new InvalidOperationException("Ending a profile action can only be done using the previous profiling result");
                }
                profile.Stop();
                ResultStack.Pop();
                if (profile.Parent == null)
                {
                    InvokeProfilingEnded(profile);
                }
            }
        }

        public void SetPrevious(IProfiler previous)
        {
            _previousProfiler = (Profiler)previous;
        }

        private void InvokeProfilingStarted(ProfilingResult result)
        {
            ProfilingStarted?.Invoke(result);
        }
        private void InvokeProfilingEnded(ProfilingResult result)
        {
            ProfilingEnded?.Invoke(result);
        }
    }
}
