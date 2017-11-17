namespace Bogosoft.Serialization
{
    /// <summary>
    /// A template for any type capable of serializing data.
    /// </summary>
    /// <typeparam name="T">The type of the destination to serialize to.</typeparam>
    public interface ISerializer<T>
    {
        /// <summary>
        /// Serialize a given object to a given destination.
        /// </summary>
        /// <param name="data">An object to serialize.</param>
        /// <param name="destination">A destination to serialize to.</param>
        void Serialize(object data, T destination);
    }

    /// <summary>
    /// A template for any type capable of serializing only objects of a specified type.
    /// </summary>
    /// <typeparam name="TIn">The type of the object that can be serialized.</typeparam>
    /// <typeparam name="TDestination">The type of the destination to serialize to.</typeparam>
    public interface ISerializer<in TIn, TDestination>
    {
        /// <summary>
        /// Serialize a given object to a given destination.
        /// </summary>
        /// <param name="data">An object to serialize.</param>
        /// <param name="destination">A destination to serialize to.</param>
        void Serialize(TIn data, TDestination destination);
    }
}