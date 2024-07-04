namespace SkelandStore.Core
{
    public class ProductSpecParams
    {
        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }

        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }

        private int pageSize = 5; //Using Full properity To can Use Validation //Set 5 Products by default in every Page

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > 10 ? 10 : value; }//Void User Can't make value more Than 10
        }

        public int PageIndex { get; set; } = 1;//Using Automatic Properity Cause we don't need to Use Validations On it 

    }
}
