namespace Pricat.Domain.Dtos;

public class PaginationData
{
    public PaginationData(int totalCount, int currentPage, int limit)
    {
        First = 1;
        TotalCount = totalCount;
        Page= currentPage;
        Limit = limit;
        Last = (int) Math.Ceiling(totalCount/(double)limit);
        Next = currentPage < Last ? currentPage + 1 : Last;
        Previous = currentPage > First ? currentPage - 1 : First;
    }

    public int First { get; private set; } 
    public int Last { get; private set; }
    public int Limit { get; private set; }
    public int Next { get; private set; }
    public int Page { get; private set; }
    public int Previous { get; private set; }
    public int TotalCount { get; private set; } 
}