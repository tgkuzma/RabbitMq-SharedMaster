using System.Collections.Generic;
using Business.Interfaces;
using Models;
using Models.Interfaces;

namespace Business
{
    public class CustomerManager : ICustomerManager
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerManager(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Customer GetCustomerByName(string customerName)
        {
            throw new System.NotImplementedException();
        }

        public void AddCustomer(Customer customerToAdd)
        {
            _customerRepository.Add(customerToAdd);
            _customerRepository.SaveChanges();
        }

        public void DeleteCustomer(Customer customerToDelete)
        {
            _customerRepository.Delete(customerToDelete);
            _customerRepository.SaveChanges();
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAll();
        }

        public Customer GetCustomerById(int id)
        {
            return _customerRepository.GetById(id);
        }

        public void UpdateCustomer()
        {
            _customerRepository.SaveChanges();
        }
    }
}
