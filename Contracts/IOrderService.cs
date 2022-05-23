using System.Collections;
using Entities;

namespace Contracts;

public interface IOrderService
{
    
    public Task<IList<Order>> GetAllOrdersAsync();
    public Task<Order> GetOrderById(int id);
    public Task RequestPurchase (Order order);
    public Task AddOrderAsync(Order order);
    public Task DeleteOrderByIdAsync(int id);
    public Task UpdateOrderAsync(Order order);
}