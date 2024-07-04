namespace SkylandStore.Helper
{
    public class Pagination<T>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }//number Of All Returned Products in every Request
        public IReadOnlyList<T> Data { get; set; }//Make it Generic Cause it maybe Return Brand Or Type Or Products not Only Products
    }
}
