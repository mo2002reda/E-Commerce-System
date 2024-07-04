namespace SkelandStore.Core.Entities.Order_Aggregation
{
    public class DeleveryMethod : BaseEntity
    {
        public DeleveryMethod()
        {

        }
        public DeleveryMethod(string shortName, string? discription, string deleveryTime, decimal cost)
        {
            ShortName = shortName;
            Description = discription;
            DeleveryTime = deleveryTime;
            Cost = cost;
        }

        public string ShortName { get; set; }
        public string? Description { get; set; }
        public string DeleveryTime { get; set; }
        public decimal Cost { get; set; }

    }
}
