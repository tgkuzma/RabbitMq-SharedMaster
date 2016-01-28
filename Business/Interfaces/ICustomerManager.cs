using System.Collections.Generic;
using Models;

namespace Business.Interfaces
{
    public interface ICustomerManager
    {
        List<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
        Customer GetCustomerByName(string customerName);
        void AddCustomer(Customer customerToAdd);
        void DeleteCustomer(Customer customerToDelete);
        void UpdateCustomer();
    }
}