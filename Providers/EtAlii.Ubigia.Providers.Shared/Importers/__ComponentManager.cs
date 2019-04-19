//namespace EtAlii.Ubigia.Provisioning
//[
//    using EtAlii.xTechnology.Logging
//    using System
//    using System.Collections.Generic

//    public class ComponentManager : IComponentManager
//    [
//        private readonly IProviderComponent[] _components
//        private readonly ILogger _logger

//        public ComponentManager(IProviderComponent[] components, ILogger logger)
//        [
//            _components = components
//            _logger = logger
//        ]
//        public void Start()
//        [
//            _logger.Info("Starting provisioning components")

//            foreach (var component in _components)
//            [
//                try
//                [
//                    component.Start()
//                ]
//                catch (Exception e)
//                [
//                    _logger.Critical("Unable to start component {0}", e, component.GetType())
//                ]
//            ]
//            _logger.Info("Started provisioning components")
//        ]
//        public void Stop()
//        [
//            _logger.Info("Stopping provisioning components")
//            foreach (var component in _components)
//            [
//                try
//                [
//                    component.Stop()
//                ]
//                catch (Exception e)
//                [
//                    _logger.Critical("Unable to start component {0}", e, component.GetType())
//                ]
//            ]
//            _logger.Info("Stopped provisioning components")
//        ]
//    ]
//]