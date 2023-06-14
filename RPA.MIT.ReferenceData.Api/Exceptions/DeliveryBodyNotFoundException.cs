using System.Runtime.Serialization;
using RPA.MIT.ReferenceData.Data.Models.RouteComponents;

namespace RPA.MIT.ReferenceData.Api.Exceptions;

/// <summary>
/// The exception that is thrown if a <see cref="Organisation" /> has not been
/// found in the data store.
/// </summary>
[Serializable]
public class DeliveryBodyNotFoundException : Exception
{
    /// <summary>
    /// Creates new DeliveryBodyNotFound exception.
    /// </summary>
    public DeliveryBodyNotFoundException()
    {
    }

    /// <summary>
    /// Ceates new DeliveryBodyNotFound exception with supplied message.
    /// </summary>
    /// <param name="message">Exception message</param>
    public DeliveryBodyNotFoundException(string message) : base(message)
    {
    }

    /// <summary>
    /// Ceates new DeliveryBodyNotFound exception with supplied message and
    /// inner exception.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="inner">Inner exception</param>
    public DeliveryBodyNotFoundException(string message, Exception inner) : base(message, inner)
    {
    }

    /// <summary>Initializes a new instance of the DeliveryBodyNotFound exception with serialized data.</summary>
    /// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="StreamingContext" /> that contains contextual information about the source or destination.</param>
    protected DeliveryBodyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
