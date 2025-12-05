

namespace JsonLearning.Services;

public class CustomerService
{
    // Vår "datorbasfil" en jsonfil i bin/
    public static readonly string Filename = Path.Combine(AppContext.BaseDirectory, "Customer.Json");

    // Snygg formatering
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true
    };

    // READ
    public async Task<List<Customer>> LoadAllAsync()
    {
        if (!File.Exists(Filename))
        {
            return new List<Customer>();
        }

        var json = await File.ReadAllTextAsync(Filename);
        var customers = JsonSerializer.Deserialize<List<Customer>>(json, _options);
        
        // NYTT decrypt ----->
        foreach (var customer in customers)
        {
            if (!string.IsNullOrEmpty(customer.CustomerEmail))
            {
                customer.CustomerEmail = EncryptionHelper.Decrypt(customer.CustomerEmail);
            }
        }
        // Säker fallback om JSON är tom eller felaktig
        return customers ?? new List<Customer>();
    }

    // Spara listan till JSON filen
    private async Task SaveAsync(List<Customer> customers)
    {
        // Nytt kryptering
        var toSave = customers.Select(customer => new Customer
        {
            CustomerId = customer.CustomerId,
            CustomerName = customer.CustomerName,
            City = customer.City,
            CustomerEmail = string.IsNullOrEmpty(customer.CustomerEmail)
                ? customer.CustomerEmail
                : EncryptionHelper.Encrypt(customer.CustomerEmail),
            CustomerPhoneNumber = customer.CustomerPhoneNumber
        }).ToList();
        
        var json = JsonSerializer.Serialize(toSave, _options);
        await File.WriteAllTextAsync(Filename, json);
    }

    // enkel auto-inkremetring av customerId
    public async Task AddAsync(Customer customer)
    {
        if (customer is null) throw new ArgumentNullException(nameof(customer));

        var customers = await LoadAllAsync();

        customer.CustomerId = customers.Any() ? customers.Max(c => c.CustomerId) + 1 : 1;

        customers.Add(customer);
        await SaveAsync(customers);
    }
    
    public async Task UpdateAsync(int customerId, string? customerName, string? customerEmail, string? city, string phoneNumber)
    {
        var customers = await LoadAllAsync();
        
        var customer = customers.FirstOrDefault(c => c.CustomerId == customerId);
        if (customer is null)
        {
            Console.WriteLine("Customer not found");
            return;
        }

        if (!string.IsNullOrWhiteSpace(customerName))
        {
            customer.CustomerName = customerName;
        }
        
        if (!string.IsNullOrWhiteSpace(customerEmail))
        { 
            customer.CustomerEmail = customerEmail;
        }
        if (!string.IsNullOrWhiteSpace(city))
        {
            customer.City = city;
        }
        if (int.TryParse(phoneNumber, out int number))
        {
            customer.CustomerPhoneNumber = number;
        }
        await SaveAsync(customers);
    }
    
    public async Task DeleteAsync(int customerId)
    {
        var customers = await LoadAllAsync();
        var customer = customers.FirstOrDefault(c => c.CustomerId == customerId);
        if (customer is null)
        {
            Console.WriteLine("Customer not found");
        }
        else
        {
            customers.Remove(customer);
            await SaveAsync(customers);
        }
    }
    
    // Lista kunder
    public static async Task ListCustomersAsync(Services.CustomerService customerService)
    {
        var customers = await customerService.LoadAllAsync();
        if (!customers.Any())
        {
            Console.WriteLine("No Customers");
            return;
        }
    
        Console.WriteLine("Id | Name | Email | City | PhoneNumber");
        foreach (var customer in customers)
        {
            Console.WriteLine($"{customer.CustomerId} | {customer.CustomerName} | {customer.CustomerEmail} | {customer.City}  | {customer.CustomerPhoneNumber}");
        }
    }

// Lägga till ny kund

    public static async Task AddCustomerAsync(Services.CustomerService customerService)
    {
        Console.WriteLine("Enter Name: ");
        var customerName = Console.ReadLine()??"";
        Console.WriteLine("Enter Email: ");
        var customerEmail = Console.ReadLine()??"";
        Console.WriteLine("Enter City: ");
        var customerCity = Console.ReadLine() ??"";
        Console.WriteLine("Enter Phone Number: ");
        var customerPhoneNumber = Console.ReadLine()?? "";
    
        var customer = new Models.Customer()
        {
            CustomerName = customerName,
            CustomerEmail = customerEmail,
            City = customerCity,
            CustomerPhoneNumber =  int.Parse(customerPhoneNumber)
        };
        await customerService.AddAsync(customer);
        Console.WriteLine("Customer added successfully");
    }

    public static async Task UpdateCustomerAsync(Services.CustomerService customerService)
    {
        await ListCustomersAsync(customerService);
        
        Console.WriteLine("Enter ID: ");
        if (!int.TryParse(Console.ReadLine(), out int customerId))
        {
            Console.WriteLine("Invalid ID");
        }
        
        Console.WriteLine("Enter Name: ");
        var customerName = Console.ReadLine() ?? string.Empty;
        Console.WriteLine("Enter Email: ");
        var customerEmail = Console.ReadLine() ?? string.Empty;
        Console.WriteLine("Enter City: ");
        var customerCity = Console.ReadLine() ?? string.Empty;
        Console.WriteLine("Enter Phone Number: ");
        var customerPhoneNumber = Console.ReadLine() ?? string.Empty;
        
        await customerService.UpdateAsync(customerId, customerName, customerEmail, customerCity, customerPhoneNumber);
        Console.WriteLine("Customer updated successfully");
    }

    public static async Task DeleteCustomerAsync(Services.CustomerService customerService)
    {
        await ListCustomersAsync(customerService);
        Console.WriteLine("Enter ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int customerId))
        {
            Console.WriteLine("Invalid ID");
        }
      
        await customerService.DeleteAsync(customerId);
        Console.WriteLine("Customer deleted successfully");
    }
    
    // Filtrerar kunder per stad
    public static async Task ListCustomerCityAsync(CustomerService customerService)
    {
        await ListCustomersAsync(customerService);
        
        Console.WriteLine("Enter City to filter by: ");
        var city = Console.ReadLine();
    
        if (string.IsNullOrWhiteSpace(city))
        {
            Console.WriteLine("City name cannot be empty.");
            return;
        }
    
        var customers = await customerService.LoadAllAsync();
        var filteredCustomers = customers
            .Where(c => string.Equals(c.City, city, StringComparison.OrdinalIgnoreCase))
            .ToList();
    
        if (!filteredCustomers.Any())
        {
            Console.WriteLine($"No customers found in '{city}'.");
            return;
        }
    
        Console.WriteLine($"Customers in '{city}':");
        Console.WriteLine("Id | Name | Email | City | PhoneNumber");
        foreach (var customer in filteredCustomers)
        {
            Console.WriteLine($"{customer.CustomerId} | {customer.CustomerName} | {customer.CustomerEmail} | {customer.City} | {customer.CustomerPhoneNumber}");
        }
    }
}