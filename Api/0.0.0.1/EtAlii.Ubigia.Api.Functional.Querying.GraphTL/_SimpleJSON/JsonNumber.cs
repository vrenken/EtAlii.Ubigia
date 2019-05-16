namespace SimpleJson
{
    using System;
    using System.Globalization;
    using System.Text;

    public partial class JsonNumber : JsonNode
    {
        private double _mData;

        public override JsonNodeType Tag { get { return JsonNodeType.Number; } }
        public override bool IsNumber { get { return true; } }
        public override Enumerator GetEnumerator() { return new Enumerator(); }

        public override string Value
        {
            get { return _mData.ToString(CultureInfo.InvariantCulture); }
            set
            {
                double v;
                if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out v))
                    _mData = v;
            }
        }

        public override double AsDouble
        {
            get { return _mData; }
            set { _mData = value; }
        }
        public override long AsLong
        {
            get { return (long)_mData; }
            set { _mData = value; }
        }

        public JsonNumber(double aData)
        {
            _mData = aData;
        }

        public JsonNumber(string aData)
        {
            Value = aData;
        }

        internal override void WriteToStringBuilder(StringBuilder aSb, int aIndent, int aIndentInc, JsonTextMode aMode)
        {
            aSb.Append(Value);
        }
        private static bool IsNumeric(object value)
        {
            return value is int || value is uint
                                || value is float || value is double
                                || value is decimal
                                || value is long || value is ulong
                                || value is short || value is ushort
                                || value is sbyte || value is byte;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (base.Equals(obj))
                return true;
            JsonNumber s2 = obj as JsonNumber;
            if (s2 != null)
                return _mData == s2._mData;
            if (IsNumeric(obj))
                return Convert.ToDouble(obj) == _mData;
            return false;
        }
        public override int GetHashCode()
        {
            return _mData.GetHashCode();
        }
    }
}