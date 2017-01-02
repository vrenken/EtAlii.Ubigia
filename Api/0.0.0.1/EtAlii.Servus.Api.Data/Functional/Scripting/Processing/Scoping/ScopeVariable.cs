namespace EtAlii.Servus.Api.Data
{
    using System;

    /// <summary>
    /// A ScopeVariable instance is used to cache the value of a variable in the scope of a script.
    /// </summary>
    public class ScopeVariable
    {
        /// <summary>
        /// The current value of the variable within the scope of the script.
        /// </summary>
        public object Value { get; private set; }
        
        /// <summary>
        /// The source of the variable. I.e. what script action created the value and assigned it to a variable?
        /// </summary>
        public string Source { get; private set; }

        /// <summary>
        /// The type of the variable. Use this for situations where the Value property returns null.
        /// </summary>
        public Type Type { get; private set; }

        public ScopeVariable(object value, string source)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            Value = value;
            Source = source;
            Type = value.GetType();
        }


        public ScopeVariable(object value, string source, Type type) 
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            Value = value;
            Source = source;
            Type = type;
        }
    }
}
