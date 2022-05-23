namespace Entities;

public class Address
{
    public string firsLine { get; set; }
    public string secondLine { get; set; }
    public string city { get; set; }
    public int zipCode { get; set; }

    public Address()
    {
        
    }
    public Address(string firsLine, string secondLine, string city, int zipCode)
    {
        this.firsLine = firsLine;
        this.secondLine = secondLine;
        this.city = city;
        this.zipCode = zipCode;
    }
}