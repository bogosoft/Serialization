namespace Bogosoft.Serialization
{
    public interface IDeserializer<T>
    {
        object Deserialize(T source);
    }

    public interface IDeserialize<out TOut, TSource>
    {
        TOut Deserialize(TSource source);
    }
}