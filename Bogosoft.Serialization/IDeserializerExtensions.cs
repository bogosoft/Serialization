namespace Bogosoft.Serialization
{
    public static class IDeserializerExtensions
    {
        public static TOut Deserialize<TOut, TSource>(this IDeserializer<TSource> deserializer, TSource source)
        {
            return (TOut)deserializer.Deserialize(source);
        }
    }
}