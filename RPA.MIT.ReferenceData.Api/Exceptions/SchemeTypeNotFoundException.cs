using System.Runtime.Serialization;
using RPA.MIT.ReferenceData.Data.Models.RouteComponents;

namespace RPA.MIT.ReferenceData.Api.Exceptions;

/// <summary>
/// The exception that is thrown if a <see cref="SchemeType" /> has not been
/// found in the data store.
/// </summary>
[Serializable]
public class SchemeTypeNotFoundException : Exception
{
    /// <summary>
    /// Creates new SchemeTypeNotFoundException exception.
    /// </summary>
    public SchemeTypeNotFoundException()
    {
    }

    /// <summary>
    /// Ceates new SchemeTypeNotFoundException exception with supplied message.
    /// </summary>
    /// <param name="message">Exception message</param>
    public SchemeTypeNotFoundException(string message) : base(message)
    {
    }

    /// <summary>
    /// Ceates new SchemeTypeNotFoundException exception with supplied message and
    /// inner exception.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="inner">Inner exception</param>
    public SchemeTypeNotFoundException(string message, Exception inner) : base(message, inner)
    {
    }

    /// <summary>Initializes a new instance of the SchemeTypeNotFoundException exception with serialized data.</summary>
    /// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="StreamingContext" /> that contains contextual information about the source or destination.</param>
    protected SchemeTypeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
