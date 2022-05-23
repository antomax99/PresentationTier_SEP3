namespace Entities;

public class Product
{
    public int productId { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string url { get; set; }
    public double value { get; set; }
    public double weight { get; set; }
    public double width { get; set; }
    public double length { get; set; }
    public double height { get; set; }

    public Product()
    {
    }

    public Product(int productId, string name,string url,string description, double value, double weight)
    {
        this.productId = productId;
        this.name = name;
        this.description = description;
        this.url = url;
        this.value = value;
        this.weight = weight;
        
    }

    public Product(string name,string url,string description, double value, double weight, double width, double length, double height)
    {
        this.name = name;
        this.description = description;
        this.url = url;
        this.value = value;
        this.weight = weight;
        this.width = width;
        this.length = length;
        this.height = height;

    }
}