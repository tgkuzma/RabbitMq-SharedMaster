using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base("MasterDbContext")
        {
            Database.SetInitializer(new DataContextInitializer());
        }

        public DataContext(string connectionStringName) : base(connectionStringName)
        {
            Database.SetInitializer(new DataContextInitializer());
        }

        public DbSet<Customer> Customers { get; set; }

    }
}
