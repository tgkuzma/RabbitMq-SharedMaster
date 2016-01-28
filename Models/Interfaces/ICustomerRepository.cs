using System.Collections.Generic;

namespace Models.Interfaces
{
    public interface ICustomerRepository
    {
        List<Customer> GetAll();
        Customer GetById(int id);
        void Add(Customer entity);
        void Delete(Customer entity);
        void SaveChanges();
    }
}