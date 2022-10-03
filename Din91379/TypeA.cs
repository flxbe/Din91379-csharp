namespace Din91379
{
    public class TypeA
    {
        readonly string _value;

        private TypeA(string value)
        {
            this._value = value;
        }

        public static TypeA FromString(string value)
        {
            return new TypeA(value);
        }

        public static implicit operator string(TypeA d)
        {
            return d._value;
        }

        private static bool IsValidTypeA(string value)
        {
            return false;
        }
    }
}
