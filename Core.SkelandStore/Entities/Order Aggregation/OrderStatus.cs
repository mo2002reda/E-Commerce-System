using System.Runtime.Serialization;

namespace SkelandStore.Core.Entities.Order_Aggregation
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]//to Store Value Of Enum in database as string not number of index 
        Pending,
        [EnumMember(Value = "PeymentReceived")]
        PeymentReceived,
        [EnumMember(Value = "PeymentFaild")]
        PeymentFaild,
    }
}
