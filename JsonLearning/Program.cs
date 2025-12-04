using JsonLearning.Models;
using JsonLearning.Services;


var service = new CustomerService();

while (true)
{
    Console.WriteLine("1 = List | 2. Add | 3. Edit | 4. Delete | 5.Customer-By-City | 0. Exit ");
    var input = Console.ReadLine();
    switch (input)
    {
        case "1":
            await CustomerService.ListCustomersAsync(service);
            break;
        case "2":
            await CustomerService.AddCustomerAsync(service);
            break;
        case "3":
            await CustomerService.UpdateCustomerAsync(service);
            break;
        case "4":
            await CustomerService.DeleteCustomerAsync(service);
            break;
        case "5":
            await CustomerService.ListCustomerCityAsync(service);
            break;
        case "0":
            break;
        default:
            break;
    }
}





