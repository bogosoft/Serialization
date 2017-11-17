using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Serialization
{
    /// <summary>
    /// A template for any type capable of serializing data.
    /// </summary>
    /// <typeparam name="T">The type of the destination to serialize to.</typeparam>
    public interface IAsyncSerializer<T>
    {
        /// <summary>
        /// Serialize a given object to a given destination.
        /// </summary>
        /// <param name="data">An object to serialize.</param>
        /// <param name="destination">A destination to serialize to.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        Task SerializeAsync(object data, T destination, CancellationToken token);
    }

    /// <summary>
    /// A template for any type capable of serializing only objects of a specified type.
    /// </summary>
    /// <typeparam name="TIn">The type of the object that can be serialized.</typeparam>
    /// <typeparam name="TDestination">The type of the destination to serialize to.</typeparam>
    public interface IAsyncSerializer<in TIn, TDestination>
    {
        /// <summary>
        /// Serialize a given object of the specified type to a given destination.
        /// </summary>
        /// <param name="data">An object to serialize.</param>
        /// <param name="destination">A destination to serialize to.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>A task representing an asynchronous operation.</returns>
        Task SerializeAsync(TIn data, TDestination destination, CancellationToken token);
    }
}