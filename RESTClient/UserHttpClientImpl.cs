using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Contracts;
using Entities;

namespace RESTClient;

public class UserHttpClientImpl :IUserService
{
    
    public async Task<IList<User>> GetAsync()
    {
        using HttpClient client = new ();
        HttpResponseMessage response = await client.GetAsync("http://localhost:8080/users");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        IList<User> users = JsonSerializer.Deserialize<IList<User>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        Console.WriteLine("GetUser returned: " + users); //Console line
        return users;
    }

    public async Task<User> GetUserByUsername(string username)
    {
        using HttpClient client = new();
        
        HttpResponseMessage response = await client.GetAsync($"http://localhost:8080/login/{username}");
        ValidateContent(response);
        string responseContent = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        { throw new Exception($"Error: {response.StatusCode}, {responseContent}"); }
        
        User user = JsonSerializer.Deserialize<User>(responseContent, new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true})!;

        return user;
    }

    public async Task<User> GetUserById(int id)
    {
        using HttpClient client = new ();
        HttpResponseMessage response = await client.GetAsync($"http://localhost:8080/users/{id}");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        User user = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        Console.WriteLine("GetUser returned: " + user); //Console line
        return user;
    }

    public async Task AddUserAsync(User user)
    {
        using HttpClient client = new ();
        //CamelCase for application 
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        string UserAsJson = JsonSerializer.Serialize(user,options);

        StringContent usercontent = new(UserAsJson, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await client.PostAsync($"http://localhost:8080/users/add",usercontent);
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }
        User returned = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        
        Console.WriteLine("AddUserAsync returned: " + returned); //Console line
        
    }

    public async Task DeleteAsync(int id)
    {
        using HttpClient client = new ();
        HttpResponseMessage response = await client.GetAsync($"http://localhost:8080/users/{id}");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }
    }

    public async Task UpdateAsync(User user)
    {
        using HttpClient client = new ();
        //CamelCase for application 
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        string UserAsJson = JsonSerializer.Serialize(user,options);
        StringContent usercontent = new(UserAsJson, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await client.PostAsync($"http://localhost:8080/users/update",usercontent);
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }
        User returned = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        
        Console.WriteLine("UpdateAsync returned: " + returned); //Console line
    }
    
    public HttpContent ValidateContent(HttpResponseMessage response)
    {
        if(string.IsNullOrEmpty(response.Content?.ReadAsStringAsync().Result))
        {
            return response.Content= new StringContent("null",Encoding.UTF8, MediaTypeNames.Application.Json);
        }
        else
        {
            return response.Content;
        }
    }
}