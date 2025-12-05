using JsonLearning.Models;
using JsonLearning.Seed;
using JsonLearning.Services;

await Seeds.SeedAsync();
var service = new CustomerService();

while (true)
{
    Console.WriteLine("1 = List | 2. Add | 3. Edit | 4. Delete | 5.Customer-By-City | 6. List-Students | 7. List-Grades | 8. Student-Grades | 0. Exit ");
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
        case "6":
            await StudentService.ListStudentAsync();
            break;
        case "7":
            await StudentService.ListGradesAsync();
            break;
        case "8":
            await StudentService.StudentGradeSummaryAsync();
            break;
        case "0. Exit":
            break;
        default:
            break;
    }
}





