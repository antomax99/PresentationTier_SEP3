namespace Entities;

public class Order
{


    public int orderId { get; set; }
    
    public int clientId { get; set; }
    public int Value{ get; set; }

    public IList<Product> _products{ get; set; }

    
    public bool iscompleted { get; set; }
    
    public Order() { }
    
    public Order(int clientId)
    {
        this.clientId = clientId;
    }

    public Order(IList<Product> products, int orderId, int clientId, int value, bool iscompleted)
    {
        _products = products;
        this.orderId = orderId;
        this.clientId = clientId;
        Value = value;
        this.iscompleted = iscompleted;
    }
    public override string ToString()
    {
        return "Order{" +
               "OrderId=" + orderId +
               ", ClientId=" + clientId +
               ", ISCompleted=" + iscompleted +
               '}';
    }
}