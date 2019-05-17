namespace SimpleJson
{
    using System.Globalization;

    public abstract partial class JsonNode
    {

        public virtual double AsDouble
        {
            get => double.TryParse(Value,NumberStyles.Float, CultureInfo.InvariantCulture, out var v) ? v : 0.0;
            set => Value = value.ToString(CultureInfo.InvariantCulture);
        }

        public virtual int AsInt { get => (int)AsDouble; set => AsDouble = value; }

        public virtual float AsFloat { get => (float)AsDouble; set => AsDouble = value; }

        public virtual bool AsBool
        {
            get
            {
                if (bool.TryParse(Value, out var v))
                    return v;
                return !string.IsNullOrEmpty(Value);
            }
            set => Value = (value) ? "true" : "false";
        }

        public virtual long AsLong { get => long.TryParse(Value, out var val) ? val : 0L; set => Value = value.ToString(); }

        public virtual JsonArray AsArray => this as JsonArray;

        public virtual JsonObject AsObject => this as JsonObject;

    }
}