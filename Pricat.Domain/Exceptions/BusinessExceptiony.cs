using System.Runtime.Serialization;

namespace Pricat.Domain.Exceptions;

[Serializable]
public class BusinessException : ApplicationException
{
    public BusinessException()
    {
    }

    public BusinessException(string message) : base(message)
    {
    }

    public BusinessException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    // Without this constructor, deserialization will fail
    protected BusinessException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}