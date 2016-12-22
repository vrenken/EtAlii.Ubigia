namespace EtAlii.Servus.Api.Diagnostics.Profiling
{
    using System;
    using System.Linq;

    public class Profiler : IProfiler
    {
        public IProfiler Parent { get {return _parentProfiler; } }
        private readonly Profiler _parentProfiler;

        public IProfiler Previous { get { return _previousProfiler; } }
        private Profiler _previousProfiler;

        public IProfilingResultStack ResultStack { get { return _profilingResultStack; } }
        private readonly IProfilingResultStack _profilingResultStack;

        private IParentProfileResultFinder _parentProfileResultFinder;

        public event Action<ProfilingResult> ProfilingStarted;
        public event Action<ProfilingResult> ProfilingEnded;

        public ProfilingAspect[] Aspects
        {
            get { return _aspects; }
            set
            {
                if (_aspects != value)
                {
                    _aspects = value;
                    AspectsChanged?.Invoke();
                }
            }
        }
        private ProfilingAspect[] _aspects = new ProfilingAspect[0];

        public event Action AspectsChanged;

        public ProfilingAspect Aspect { get { return _aspect; } }
        private readonly ProfilingAspect _aspect;

        public IProfiler Create(ProfilingAspect aspect)
        {
            return new Profiler(this, aspect, _profilingResultStack);
        }

        private Profiler(IProfiler parent, ProfilingAspect aspect, IProfilingResultStack profilingResultStack)
        {
            _aspect = aspect;
            _parentProfiler = (Profiler)parent;
            if (_parentProfiler != null)
            {
                _parentProfiler.AspectsChanged += OnParentProfilerAspectsChanged;
                _aspects = _parentProfiler.Aspects;

                ProfilingStarted += result => _parentProfiler.InvokeProfilingStarted(result);
                ProfilingEnded += result => _parentProfiler.InvokeProfilingEnded(result);
            }
            _profilingResultStack = profilingResultStack;
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
            var shouldPropagate = _aspects.Contains(_aspect);
            var parentProfile = _parentProfileResultFinder.Find(this);
            var profile = new ProfilingResult(parentProfile, _aspect.Id, _aspect.Layer, action, shouldPropagate);

            if (shouldPropagate)
            {
                _profilingResultStack.Push(profile);
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
            var shouldPropagate = _aspects.Contains(_aspect);
            if (shouldPropagate)
            {
                if (_profilingResultStack.Peek() != profile)
                {
                    throw new InvalidOperationException("Ending a profile action can only be done using the previous profiling result");
                }
                profile.Stop();
                _profilingResultStack.Pop();
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
