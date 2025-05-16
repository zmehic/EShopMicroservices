namespace OrderingApplication.DTOs
{
    public record OrderItemDTO(Guid OrderId, Guid ProductId, int Quantity, decimal Price);
}
