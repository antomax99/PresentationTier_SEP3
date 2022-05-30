using System.Collections;
using System.Text;
using System.Text.Json;

using System.Text.Json.Serialization;
using Contracts;
using Entities;

namespace RESTClient;
//TODO: Change to be able to parse orders and the attached products
public class OrderHttpClientImpl : IOrderService
{
    private readonly int APPLICATION_IP=8080;
    public async Task<IList<Order>> GetAllOrdersAsync()
    {
        using HttpClient client = new ();
        HttpResponseMessage response = await client.GetAsync($"http://localhost:{APPLICATION_IP}/orders");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        IList<Order> orders = JsonSerializer.Deserialize<IList<Order>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        Console.WriteLine(orders[0].ToString());
        return orders;
    }

    public async Task<IList<Order>> GetAllOrdersFromUser(int userID)
    {
        using HttpClient client = new ();
        HttpResponseMessage response = await client.GetAsync($"http://localhost:{APPLICATION_IP}/orders/{userID}/user");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        IList<Order> orders = JsonSerializer.Deserialize<IList<Order>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        //Console.WriteLine(orders[0].ToString());
        return orders;
    }

    public async Task<Order> GetOrderById(int id)
    {
        
        using HttpClient client = new ();
        HttpResponseMessage response = await client.GetAsync($"http://localhost:{APPLICATION_IP}/orders/{id}");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        Order order = JsonSerializer.Deserialize<Order>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        Console.WriteLine(order.ToString());
        return order;
    }

    public async Task AddOrderAsync(Order order)
    {
        
        using HttpClient client = new ();
        //CamelCase for application 
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        string orderAsJson = JsonSerializer.Serialize(order,options);

        StringContent postcontent = new(orderAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync($"http://localhost:{APPLICATION_IP}/order/add",postcontent);
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }
        Order returned = JsonSerializer.Deserialize<Order>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        Console.WriteLine("AddOrderAsync returned: " + returned.ToString());
    }

    public async Task DeleteOrderByIdAsync(int id)
    {
        using HttpClient client = new ();
        HttpResponseMessage response = await client.GetAsync($"http://localhost:{APPLICATION_IP}/Order/{id}");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }
    }

    public async Task UpdateOrderAsync(Order order)
    {
        using HttpClient client = new ();
        //CamelCase for application 
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        string orderAsJson = JsonSerializer.Serialize(order,options);
        StringContent postcontent = new(orderAsJson, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await client.PatchAsync($"http://localhost:{APPLICATION_IP}/order/update",postcontent);
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }
        Order returned = JsonSerializer.Deserialize<Order>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        
        Console.WriteLine("UpdateOrderAsync returned: " + returned); //Console line
    }
    public async Task RequestPurchase(Order order)
    {
        
        using HttpClient client = new ();
        //CamelCase for application 
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        string orderAsJson = JsonSerializer.Serialize(order,options);

        StringContent postcontent = new(orderAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync($"http://localhost:{APPLICATION_IP}/order/purchase",postcontent);
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }
    }
}