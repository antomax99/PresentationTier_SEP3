namespace Entities;

public class Product
{
    public int productId { get; set; }
    public string name { get; set; }
    
    public string brand { get; set; }
    public string description { get; set; }
    public double value { get; set; }


    public Product()
    {
    }

    public Product(int productId, string name,string brand,string description, double value)
    {
        this.productId = productId;
        this.name = name;
        this.brand = brand;
        this.description = description;
        this.value = value;
    }

    public Product(string name,string brand,string description, double value)
    {
        this.name = name;
        this.brand = brand;
        this.description = description;
        this.value = value;


    }
}