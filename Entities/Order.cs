namespace Entities;

public class Order
{


    public int orderId { get; set; }
    
    public int customerId { get; set; }
    public double price{ get; set; }

    public IList<Product> products{ get; set; }
    
    public bool completed { get; set; }
    
    public Order() { }
    
    public Order(int customerId)
    {
        this.customerId = customerId;
    }

    public Order(IList<Product> products, int orderId, int customerId, int price, bool completed)
    {
        this.products = products;
        this.orderId = orderId;
        this.customerId = customerId;
        this.price = price;
        this.completed = completed;
    }

}