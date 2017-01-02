namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;

    public class __ParameterAggregator
    {
        private readonly List<__NamedParameter> _parameters = new List<__NamedParameter>();

        public __NamedParameter AddParameter(object value)
        {
            var parameter = new __NamedParameter("p" + (_parameters.Count + 1), value);
            _parameters.Add(parameter);
            return parameter;
        }

        public __NamedParameter[] GetParameters()
        {
            return _parameters.ToArray();
        }
    }
}