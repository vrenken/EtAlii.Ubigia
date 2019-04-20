//namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR.AspNetCore
//[
//    using System
//    using System.Collections.Generic
//    using System.IO
//    using System.Reflection
//    using EtAlii.Ubigia.Api.Transport
//    using Microsoft.AspNet.SignalR.Hubs
//    using Microsoft.AspNetCore.Mvc.Abstractions
//    using Microsoft.AspNetCore.SignalR.Hubs
//    using Microsoft.AspNetCore.SignalR.Json

//    public class DefaultParameterResolver : IParameterResolver
//    [
//        /// <summary>
//        /// Resolves a parameter value based on the provided object.
//        /// </summary>
//        /// <param name="descriptor">Parameter descriptor.</param>
//        /// <param name="value">Value to resolve the parameter value from.</param>
//        /// <returns>The parameter value.</returns>
//        public virtual object ResolveParameter(ParameterDescriptor descriptor, IJsonValue value)
//        [
//            if [descriptor == null]
//                throw new ArgumentNullException(nameof(descriptor))
//            if [value == null]
//                throw new ArgumentNullException(nameof(value))
//            if [value.GetType[] == descriptor.ParameterType]
//                return (object)value
//            return value.ConvertTo(descriptor.ParameterType)
//        ]
//        /// <summary>
//        /// Resolves method parameter values based on provided objects.
//        /// </summary>
//        /// <param name="method">Method descriptor.</param>
//        /// <param name="values">List of values to resolve parameter values from.</param>
//        /// <returns>Array of parameter values.</returns>
//        public virtual IList<object> ResolveMethodParameters(MethodDescriptor method, IList<IJsonValue> values)
//        [
//            if [method == null]
//                throw new ArgumentNullException(nameof(method))
//            return (IList<object>)method.Parameters.Zip<ParameterDescriptor, IJsonValue, object>((IEnumerable<IJsonValue>)values, new Func<ParameterDescriptor, IJsonValue, object>(this.ResolveParameter)).ToArray<object>()
//        ]
//    ]
//]