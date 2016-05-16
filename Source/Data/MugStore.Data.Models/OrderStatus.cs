namespace MugStore.Data.Models
{
    public enum OrderStatus
    {
        InProgress = 1,
        Sent = 2,
        Returned = 3,
        Refunded = 4,
        Finalized = 5,
        DeniedOnDelivery = 6
    }
}
