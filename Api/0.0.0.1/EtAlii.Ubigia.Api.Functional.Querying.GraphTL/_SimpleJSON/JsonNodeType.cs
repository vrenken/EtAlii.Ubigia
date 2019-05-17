namespace SimpleJson
{
    public enum JsonNodeType
    {
        Array = 1,
        Object = 2,
        String = 3,
        Number = 4,
        NullValue = 5,
        Boolean = 6,
        None = 7,
        Annotation = 8,
        Custom = 0xFF,
    }
}