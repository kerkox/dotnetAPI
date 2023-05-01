using Pricat.Domain.Common;

namespace Pricat.Domain.Dtos;

public class ResponseData<T> where T : EntityBase
{
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
    public PaginationData XPagination { get; set; } = null!;
}