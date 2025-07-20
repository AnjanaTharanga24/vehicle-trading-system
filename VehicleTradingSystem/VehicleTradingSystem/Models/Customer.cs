// Models/Customer.cs
namespace VehicleTradingSystem.Models
{
    public class Customer
    {
        public string CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NIC { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string Address { get; set; }
        public string PreferredVehicleType { get; set; }
        public string BudgetRange { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}