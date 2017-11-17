namespace Bogosoft.Serialization
{
    public interface ISerializer<T>
    {
        void Serialize(object data, T destination);
    }

    public interface ISerializer<in TIn, TDestination>
    {
        void Serialize(TIn data, TDestination destination);
    }
}