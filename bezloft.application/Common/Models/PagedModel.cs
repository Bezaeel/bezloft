namespace bezloft.application.Common.Models;

public class PagedModel<TModel>
{
    public PageMeta meta { get; set; }
    public IList<TModel> Items { get; set; }

    public PagedModel()
    {
        Items = new List<TModel>();
        meta = new PageMeta();
    }
}

public class PageMeta
{
    const int MaxPageSize = 500;
    private int _pageSize;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }

    public int CurrentPage { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }
    public bool HasMore => CurrentPage < TotalPages;
}