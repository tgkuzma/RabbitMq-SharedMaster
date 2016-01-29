using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Ninject;

namespace SharedMaster
{
    public class Program
    {
        private static void Main(string[] args)
        {
            InitializeDependencies();
            Console.WriteLine("Waiting for messages...");
            Console.ReadKey();
        }

        private static void InitializeDependencies()
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            kernel.Get<ICustomerManager>().GetAllCustomers();
        }
    }
}
