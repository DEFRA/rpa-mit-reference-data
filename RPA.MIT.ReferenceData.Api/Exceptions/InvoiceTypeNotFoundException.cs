using System.Runtime.Serialization;
using RPA.MIT.ReferenceData.Data.Models.RouteComponents;

namespace RPA.MIT.ReferenceData.Api.Exceptions;

/// <summary>
/// The exception that is thrown if a <see cref="InvoiceType" /> has not been
/// found in the data store.
/// </summary>
[Serializable]
public class InvoiceTypeNotFoundException : Exception
{
    /// <summary>
    /// Creates new InvoiceTypeNotFound exception.
    /// </summary>
    public InvoiceTypeNotFoundException()
    {
    }

    /// <summary>
    /// Ceates new InvoiceTypeNotFound exception with supplied message.
    /// </summary>
    /// <param name="message">Exception message</param>
    public InvoiceTypeNotFoundException(string message) : base(message)
    {
    }

    /// <summary>
    /// Ceates new InvoiceTypeNotFound exception with supplied message and
    /// inner exception.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="inner">Inner exception</param>
    public InvoiceTypeNotFoundException(string message, Exception inner) : base(message, inner)
    {
    }

    /// <summary>Initializes a new instance of the InvoiceTypeNotFound exception with serialized data.</summary>
    /// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="StreamingContext" /> that contains contextual information about the source or destination.</param>
    protected InvoiceTypeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
